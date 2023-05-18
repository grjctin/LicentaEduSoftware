import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { map, of, take } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = []; //salvam members aici, nu in componenta pentru a nu face cate un request de fiecare data
  memberCache = new Map();//retine kvp, retine pagini intregi pe care la gasim folosind o cheie(configuratie de filtre)
  user: User | undefined;
  userParams: UserParams | undefined;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user) {
          this.userParams = new UserParams(user);
          this.user = user;
        }
      }
    })
   }

   getUserParams() {
    return this.userParams;
   }

   setUserParams(params: UserParams) {
    this.userParams = params;
   }

   resetUserParams() {
    if(this.user) {
      this.userParams = new UserParams(this.user);
      return this.userParams;
    }
    return;
   }

  getMembers(userParams: UserParams) {
    //console.log(Object.values(userParams).join('-'));
    const response = this.memberCache.get(Object.values(userParams).join('-'));

    //Daca exista deja in cache
    if(response) return of(response);

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    //Daca nu exista in cache, adaug kvp
    return this.getPaginatedResult<Member []>(this.baseUrl + 'users',params).pipe(
      map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    )
  }

  getMember(username: string) {
    //vrem sa facem un flat array care sa contina doar membri din memberCache
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.userName === username)
    //console.log(member);

    if(member) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username/*, this.getHttpOptions()*/);
  }
  //nu mai folosim metoda pentru a trimite tokenul in header pt autentificare
  //pentru ca am creat interceptorul care face asta automat

  // getHttpOptions() {
  //   const userString = localStorage.getItem('user');
  //   if(!userString) return;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token
  //     })
  //   }
  // }

  updateMember(member:Member) {
    //trebuie sa actualizam membrul modificat in lista de members dupa update
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member}; //ia fiecare propr pe rand, spread operator
      })
    )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) { 
    return this.http.delete(this.baseUrl + 'users/delete-photo/'+ photoId);
  }

  private getPaginatedResult<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);

    return params;
  }
}

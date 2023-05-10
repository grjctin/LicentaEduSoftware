import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = []; //salvam members aici, nu in componenta pentru a nu face cate un request de fiecare data

  constructor(private http: HttpClient) { }

  getMembers() {
    if(this.members.length > 0) return of(this.members); //trebuie returnat ca observable, folosim of din rxjs
    return this.http.get<Member[]>(this.baseUrl + 'users'/*, this.getHttpOptions()*/).pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )

  }

  getMember(username: string) {
    const member = this.members.find(x => x.userName == username);
    if (member) return of(member);
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
}

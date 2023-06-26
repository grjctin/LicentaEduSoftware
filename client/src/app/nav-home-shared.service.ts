import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavHomeSharedService {

  private triggerEventSource = new Subject<void>();

  constructor() { }

  triggerEvent$ = this.triggerEventSource.asObservable();

  triggerEvent() {
    this.triggerEventSource.next();
  }
}

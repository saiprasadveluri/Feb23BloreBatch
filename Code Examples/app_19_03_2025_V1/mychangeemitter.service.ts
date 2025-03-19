import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs'
@Injectable({
  providedIn: 'root'
})
export class MychangeemitterService {
   evtEmitterSource:Subject<Boolean>=new Subject();
   EmittedEvents:Observable<Boolean>;
  constructor() { 
    this.EmittedEvents=this.evtEmitterSource.asObservable();
  }

  EmitEvent(loggedIn:boolean)
  {
    this.evtEmitterSource.next(loggedIn);
  }
}
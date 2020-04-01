import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUerl = 'http://localhost:5000/api/Auth/';
constructor(private http: HttpClient) { }
login(model: any) {
  return this.http.post(this.baseUerl + 'login', model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('Token', user.token);
        }
      })
    );

}

register(model: any) {
  return this.http.post(this.baseUerl + 'register', model);
}
}


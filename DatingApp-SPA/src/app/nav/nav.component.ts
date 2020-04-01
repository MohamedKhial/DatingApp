import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/_services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }
  login() {
    this.authservice.login(this.model).subscribe(next => {
      console.log('login sucess');

    }, error => {
      console.log('Login Error');
    });
  }

loggedIn(){
   const token = localStorage.getItem('Token');
   return !!token;
}

logout(){
  localStorage.removeItem('Token');
  console.log('logged out');
}

}

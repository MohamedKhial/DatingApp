import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({

  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {
  registermode = false;
  // values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
   // this.getValues();
  }
 registerToggle() {
    this.registermode = !this.registermode;
  }

  // getValues() {

  //   this.http.get('http://localhost:5000/api/Values').subscribe(respnse => {
  //     this.values = respnse;
  //   }, error => {
  //     console.log(error);
  //   });
  // }
  CancelRegisterMode(regrmode: boolean) {
    this.registermode = regrmode;
  }
}

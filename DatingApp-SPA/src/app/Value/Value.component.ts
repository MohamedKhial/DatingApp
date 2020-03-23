import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-Value',
  templateUrl: './Value.component.html',
  styleUrls: ['./Value.component.css']
})
export class ValueComponent implements OnInit {
values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {

    this.http.get('http://localhost:5000/api/Values').subscribe(respnse => {
      this.values = respnse;
    }, error => {
      console.log(error);
    });

    // this.http.get('http://localhost:5000/api/Values').subscribe( response=> {
    //     console.log('in success');
    //     this.values= response;
    //   },error=>{
    //     console.log(error);
    //   });
  }

}

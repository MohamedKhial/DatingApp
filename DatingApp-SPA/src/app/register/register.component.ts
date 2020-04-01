import { Component, OnInit, Input, Output , EventEmitter } from '@angular/core';
import { AuthService } from 'src/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 // @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
model: any = {};
  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }
register() {
 this.authservice.register(this.model).subscribe(() => {
   console.log('registered');
 }, error => {
   console.log(error);
 });
}
Cancel() {
  this.cancelRegister.emit(false);
  console.log('canceled');
}
}

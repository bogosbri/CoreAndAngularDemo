import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // output properties emit events
  @Output() cancelRegister = new EventEmitter();
  model: any = {};


  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
   this.authService.register(this.model).subscribe( () => {
     this.alertify.success('registration success');
   }, error => {
     console.log(error);
   });
  }

  cancel() {
    console.log('cancelled registration');
    this.cancelRegister.emit(false);
  }

}

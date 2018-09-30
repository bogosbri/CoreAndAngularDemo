import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  // store our user name and password as type any set to an empty object using the curly braces
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {

  }

  login() {
    this.authService.login(this.model).subscribe( next => {
        console.log('Log in success');
      },
       error => {
         console.log('failed to login');
       });
      }

      loggedIn() {
        const token = localStorage.getItem('token');
        return !!token;
      }

      logOut() {
        localStorage.removeItem('token');
        console.log('log out');
      }


  }

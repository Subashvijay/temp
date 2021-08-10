import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Token } from '../Models/token';
import { UserInfo } from '../Models/UserInfo';
import { TweetappService } from '../services/tweetapp.service';

@Component({
  selector: 'app-login',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  user:UserInfo;
  submitted=false;
  token:Token;
  userForm:FormGroup;
  message:string;
  constructor(private frombuilder:FormBuilder,private service:TweetappService,private route:Router) { }

  ngOnInit() {
    this.userForm = this.frombuilder.group({
        username:['',[Validators.required]],
        password:['',[Validators.required,
        Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
      });
  }

  onSubmitLogin(){
    this.submitted=true;
    if(this.userForm.invalid){
     return;
    }
      else {
      this.Validate();
      }
    }
     public Validate()
      {
        let username=this.userForm.value['username']
        let password=this.userForm.value['password']
          let token=new Token()
          this.service.Login(username,password).subscribe(res=>{this.token=res,console.log(this.token)
            if(this.token.message=="Success")
            {
              alert("welcome")
       console.log(this.token)
          localStorage.setItem("token",this.token.tokens);
          localStorage.setItem("UserId",this.token.userId.toString());
          localStorage.setItem("Username",this.token.username);
          this.route.navigate(['USER'], { skipLocationChange: true })
            }
            else{
              alert("invalid username or password")
              this.onReset();
            }
        });
      }
      get f(){return this.userForm.controls;}

    onReset()
    {
      this.submitted=false;
      this.userForm.reset();
    }
}
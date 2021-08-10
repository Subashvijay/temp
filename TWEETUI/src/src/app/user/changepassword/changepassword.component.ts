import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Token } from 'src/app/Models/token';
import { UserInfo } from 'src/app/Models/UserInfo';
import { TweetappService } from 'src/app/services/tweetapp.service';


@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent implements OnInit {
  user:UserInfo;
  submitted=false;
  token:Token;
  userForm:FormGroup;
  message:string;
  id:number;
  username:string;
  uname:string;
  prePasswordCheck: boolean = false;
  passwordValidation: boolean = false;
  form:FormGroup
  constructor(private frombuilder:FormBuilder,private service:TweetappService,private route:Router) { 
    this.id = Number(localStorage.getItem('UserId') || '{}') ;
    this.username = String(localStorage.getItem('Username') || '{}');
    this.service.GetUserProfile(this.username).subscribe(res=>
      {
        this.user=res;
        console.log(this.user);
      },
      err=>{
        console.log(err);
      }
      )
  }

  ngOnInit() {
    this.userForm = this.frombuilder.group({
        emailid: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]],
        oldpassword: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
        newpassword: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
        confirmpassword: ['', Validators.required],
      });
      }

      checkPassword(password: HTMLInputElement, confirmpassword: HTMLInputElement) {

        // console.log(password.value,rePassword.value);
        if (password.value.length != 0) {
          this.prePasswordCheck = false;
          if (password.value == confirmpassword.value || confirmpassword.value.length == 0) {
            this.passwordValidation = false;
          } else {
            this.passwordValidation = true;
          }
        } else {
          this.prePasswordCheck = true;
          this.passwordValidation = false;
        }
        if (confirmpassword.value.length == 0) {
          this.prePasswordCheck = false;
          this.passwordValidation = false;
        }
        if (this.prePasswordCheck) {
          confirmpassword.value = "";
        }
      }
    
      // Check prePassword
      checkPrePassword(password : HTMLInputElement ){
        if(password.value.length != 0){
          this.prePasswordCheck = false;
          this.passwordValidation = false;
        }
      }

    changepassword()
    {
      let emailId=this.userForm.value["emailid"];
      let oldPassword=this.userForm.value["oldpassword"];
      let newPassword=this.userForm.value["newpassword"]

      this.service.UpdatePassword(emailId, oldPassword, newPassword).subscribe(res=>{console.log(this.message),alert("updated succesfully"), this.route.navigateByUrl('USER')},err=>{
        console.log(err)
      })
    }

    onSubmitPassword(){
      this.submitted=true;
      if(this.userForm.invalid){
       return;
      }
        else {
        this.changepassword();
        }
      }
      get f(){return this.userForm.controls;}
      Search()
  {
     this.uname = this.form.value["username"]
    localStorage.setItem("uname", this.uname);
    this.route.navigateByUrl('/SEARCH TWEET');
  }
  onReset()
    {
      this.submitted=false;
      this.userForm.reset();
    }
}
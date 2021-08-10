import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserInfo } from '../Models/UserInfo';
import { TweetappService } from '../services/tweetapp.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  list: UserInfo[] = [];
  user: UserInfo;
  submitted = false;
  userForm: FormGroup;
  img: string;
  selectedFile: File;
  message: string;
  emailValid: boolean = false;
  prePasswordCheck: boolean = false;
  passwordValidation: boolean = false;
  passwordLength: boolean = false;
  showElement: boolean = false;
  constructor(private frombuilder: FormBuilder, private service: TweetappService, private route: Router) { }

  ngOnInit() {
    this.userForm = this.frombuilder.group({
      firstname: ['', [Validators.required, Validators.pattern("^[A-Za-z]{0,}$")]],
      username: ['', Validators.required],
      emailid: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]],
      password: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
      confirmpassword: ['', Validators.required],
      contactnumber: ['', [Validators.required, Validators.pattern("^[0-9]{9}$")]],
      lastname: ['', [Validators.pattern("^$|^[A-Za-z]{0,}$")]],
    });
  }

  //check if password and repassword are same

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

  onSubmitUser() {
    this.submitted = true;
    if (this.userForm.invalid) {
      return;
    }
    else {
      if (this.emailValid == false && this.passwordValidation == false) {
        this.user = new UserInfo();
        this.user.id = 0;
        this.user.firstName = this.userForm.value["firstname"];
        this.user.lastName = this.userForm.value["lastname"];
        this.user.userName = this.userForm.value["username"];
        this.user.email = this.userForm.value["emailid"];
        this.user.password = this.userForm.value["password"];
        this.user.contactNumber = parseInt(this.userForm.value["contactnumber"], 10);
        this.user.imageName = this.img;
        const payload = Object.assign({}, this.user);
        const finalPayload = { userInfo: payload };
        console.log(finalPayload)
        this.service.Register(finalPayload).subscribe(res => {
          alert("Successfully registered");
          console.log(res);
          this.route.navigateByUrl('HOME');
        },
          err => {
            alert("Failed to Register! Try again")
            console.log(err);
            this.onReset();
          }
        );
      }
    }
  }
  get f() { return this.userForm.controls; }
  onReset() {
    this.submitted = false;
    this.userForm.reset();
  }
  fileEvent(event: any) {
    this.img = event.target.files[0].name;
  }
}


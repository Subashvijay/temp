import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Tweet } from 'src/app/Models/tweet';
import { UserInfo } from 'src/app/Models/UserInfo';
import { UserTweets } from 'src/app/Models/user-tweets';
import { TweetappService } from 'src/app/services/tweetapp.service';

@Component({
  selector: 'app-posttweet',
  templateUrl: './posttweet.component.html',
  styleUrls: ['./posttweet.component.css']
})
export class PosttweetComponent implements OnInit {

  form:FormGroup;
  id:number;
  user:UserInfo;
  userlist:UserInfo[];
  tweet:Tweet;
  username:string;
  submitted = false;
  list:UserTweets[];
  public isCollapsed = true;

  constructor(private frombuilder:FormBuilder,private service:TweetappService,private route:Router) { 
    this.id = Number(localStorage.getItem('UserId') || '{}') ;
    this.username = String(localStorage.getItem('Username') || '{}');
    this.isCollapsed = true;
    this.service.GetUserProfile(this.username).subscribe(res=>
      {
        this.user=res;
        console.log(this.user);
        localStorage.setItem("lastName",this.user.lastName);
        localStorage.setItem("firstName",this.user.firstName);
      },
      err=>{
        console.log(err);
      })
  }

  ngOnInit() {
    this.form = this.frombuilder.group({
      tweets:['', Validators.required],
      like:['']
    })
  }

  onSubmitPost(){
    this.submitted=true;
    if(this.form.invalid){
     return;
    }
      else{
        this.tweet=new Tweet();
      this.tweet.userid=this.id;
      this.tweet.username=this.username;
      this.tweet.text=this.form.value["tweets"];
      this.tweet.tweetDate=new Date();
      this.tweet.lastName= String(localStorage.getItem('lastName'));
      this.tweet.firstName=String(localStorage.getItem('firstName'));
      this.tweet.likes= 0;
      const payload = Object.assign({}, this.tweet);
      const finalPayload = { tweet: payload };
      console.log(this.tweet)
     this.service.PostTweet(finalPayload).subscribe(res=>{
        alert("Tweet Posted Successfully")
        this.onReset();
    this.route.navigateByUrl('USER')
     },
      err=>{
        alert("Failed to Post")
        this.onReset();
      });
      }
    }

    onReset(){
      this.submitted=false;
      this.form.reset();
      this.isCollapsed = !this.isCollapsed;
    }

}

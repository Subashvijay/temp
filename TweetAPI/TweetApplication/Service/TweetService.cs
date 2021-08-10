// <copyright file="TweetService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TweetApplication.Models;
using TweetApplication.Repository;

namespace TweetApplication.Service
{
    /// <summary>
    /// Tweet service
    /// </summary>
    public class TweetService : ITweetService
    {
        /// <summary>
        /// Tweet repository
        /// </summary>
        private readonly ITweetRepository tweetRepository;

        /// <summary>
        /// Logger deatils
        /// </summary>
        private readonly ILogger<TweetService> logger;

        /// <summary>
        /// Tweet service
        /// </summary>
        /// <param name="tweetRepository">Tweet repository</param>
        /// <param name="logger">Logger</param>
        public TweetService(ITweetRepository tweetRepository, ILogger<TweetService> logger)
        {
            this.tweetRepository = tweetRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userInfo">User information</param>
        /// <returns>Empty string if registered successfully else error message</returns>
        public async Task<string> RegisterUser(UserInfo userInfo)
        {
            string message = $"Registeration failed for user having mail id: {userInfo.Email}";
            userInfo.Password = this.EncryptPassword(userInfo.Password);
            int result = await this.tweetRepository.RegisterUser(userInfo);
            if (result > 0)
            {
                message = string.Empty;
            }
            return message;
        }

        /// <summary>
        /// UpdatePassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="oldpassword">oldpassword.</param>
        /// <param name="newPassword">newPassword.</param>
        /// <returns>response.</returns>
        public async Task<string> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            string message = string.Empty;
                if (newPassword != null && oldpassword != null)
                {
                    newPassword = this.EncryptPassword(newPassword);
                    oldpassword = this.EncryptPassword(oldpassword);
                }

                var result = await this.tweetRepository.UpdatePassword(emailId, oldpassword, newPassword);
                if (result)
                {
                    message = "\"Updated Successfully\"";
                }
                else
                {
                    message = "Update Failed";
                }

                return message;
        }

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        public async Task<string> ForgotPassword(string emailId, string password)
        {
                string message = string.Empty;
                if (password != null)
                {
                    password = this.EncryptPassword(password);
                }

                var result = await this.tweetRepository.ForgotPassword(emailId, password);
                if (result)
                {
                    message = "\"Changed Password\"";
                }
                else
                {
                    message = "Failed";
                }

                return message;
        }

        /// <summary>
        /// Verify user credentials when user tries to login
        /// </summary>
        /// <param name="username">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>True if user verified else false</returns>
        public async Task<UserInfo> VerifyUserCredentials(string username, string password)
        {
            password = this.EncryptPassword(password);
            return await this.tweetRepository.VerifyUserCredentials(username, password);
        }

        /// <summary>
        /// Post tweet
        /// </summary>
        /// <param name="tweet">Tweet</param>
        /// <returns>Empty string if tweet posted successfully else error message</returns>
        public async Task<string> PostTweet(Tweet tweet)
        {
            string message = $"Posting tweet failed for: {tweet.Text}";
            int result = await this.tweetRepository.PostTweet(tweet);
            if (result > 0)
            {
                message = string.Empty;
            }
            return message;
        }

        /// <summary>
        /// Get tweet based on user id
        /// </summary>
        /// <param name="userId">User if</param>
        /// <returns>Tweets</returns>
        public async Task<IEnumerable<Tweet>> GetTweet(int userId)
        {
            return await this.tweetRepository.GetTweet(userId);
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<IEnumerable<UserInfo>> GetAllUsers()
        {
            return await this.tweetRepository.GetAllUsers();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<UserInfo> GetUserProfile(string userName)
        {
            return await this.tweetRepository.GetUserProfile(userName);
        }

        /// <summary>
        /// Get tweets
        /// </summary>
        /// <returns>Tweets</returns>
        public async Task<IEnumerable<UserTweets>> GetAllTweets()
        {
            return await this.tweetRepository.GetAllTweets();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<List<UserTweets>> GetTweetsByUser(string userName)
        {
            return await this.tweetRepository.GetTweetsByUser(userName);
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="userCredentials">User credentials</param>
        /// <returns>Empty string if user password updated successfully else error message</returns>
        public async Task<string> UpdateUserPassword(UserCredentials userCredentials)
        {
            string message = $"Update password failed for user having mail id: {userCredentials.EmailId}";
            userCredentials.NewPassword = this.EncryptPassword(userCredentials.NewPassword);
            userCredentials.OldPassword = this.EncryptPassword(userCredentials.OldPassword);
            int result = await this.tweetRepository.UpdateUserPassword(userCredentials);
            if (result > 0)
            {
                message = string.Empty;
            }
            return message;
        }

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>Empty string if user password reset succeeded else error message</returns>
        public async Task<string> ResetUserPassword(string emailId, string password)
        {
            string message = $"Reset password failed for user having mail id: {emailId}";
            password = this.EncryptPassword(password);
            Tuple<string, string> updateData = new Tuple<string, string>(emailId, password);
            int result = await this.tweetRepository.ResetUserPassword(updateData);
            if (result > 0)
            {
                message = string.Empty;
            }
            return message;
        }

        /// <summary>
        /// Likes.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        public async Task<int> Likes(string username, string tweet)
        {
            return await this.tweetRepository.Likes(username, tweet);
        }

        /// <summary>
        /// Comments.
        /// </summary>
        /// <param name="comment">comment.</param>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <param name="date">date.</param>
        /// <returns>response.</returns>
        public async Task<int> Comments(string comment, string username, string userName, string tweet)
        {
            DateTime date = DateTime.Now;
            return await this.tweetRepository.Comments(comment, username, userName, tweet, date);
        }

        public async Task<List<UserComments>> GetComments(string username, string tweet)
        {
                var result = await this.tweetRepository.GetComments(username, tweet);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
        }

        public async Task<string> DeleteTweet(string username, string tweet)
        {
                string message = string.Empty;
                var result = await this.tweetRepository.DeleteTweet(username, tweet);
                if (result > 0)
                {
                    return message = "\"Deleted\"";
                }
                else
                {
                    return message = "\"Failed to Delete\"";
                }
        }

        private string EncryptPassword(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }
}

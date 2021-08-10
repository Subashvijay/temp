// <copyright file="ITweetService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApplication.Models;

namespace TweetApplication.Service
{
    /// <summary>
    /// Interface for tweet service
    /// </summary>
    public interface ITweetService
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userInfo">User information</param>
        /// <returns>Empty string if registered successfully else error message</returns>
        Task<string> RegisterUser(UserInfo userInfo);

        /// <summary>
        /// Verify user credentials when user tries to login
        /// </summary>
        /// <param name="username">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>True if user verified else false</returns>
        Task<UserInfo> VerifyUserCredentials(string username, string password);

        /// <summary>
        /// Post tweet
        /// </summary>
        /// <param name="tweet">Tweet</param>
        /// <returns>Empty string if tweet posted successfully else error message</returns>
        Task<string> PostTweet(Tweet tweet);

        /// <summary>
        /// Get tweet based on user id
        /// </summary>
        /// <param name="userId">User if</param>
        /// <returns>Tweets</returns>
        Task<IEnumerable<Tweet>> GetTweet(int userId);

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        Task<IEnumerable<UserInfo>> GetAllUsers();

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        Task<UserInfo> GetUserProfile(string userName);

        /// <summary>
        /// Get tweets
        /// </summary>
        /// <returns>Tweets</returns>
        Task<IEnumerable<UserTweets>> GetAllTweets();

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        Task<List<UserTweets>> GetTweetsByUser(string userName);

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="userCredentials">User credentials</param>
        /// <returns>Empty string if user password updated successfully else error message</returns>
        Task<string> UpdateUserPassword(UserCredentials userCredentials);

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>Empty string if user password reset succeeded else error message</returns>
        Task<string> ResetUserPassword(string emailId, string password);

        /// <summary>
        /// Likes.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        Task<int> Likes(string username, string tweet);

        /// <summary>
        /// Comments.
        /// </summary>
        /// <param name="comment">comment.</param>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        Task<int> Comments(string comment, string username, string userName, string tweet);

        /// <summary>
        /// Likes.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        Task<List<UserComments>> GetComments(string username, string tweet);

        /// <summary>
        /// DeleteTweet.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        Task<string> DeleteTweet(string username, string tweet);

        /// <summary>
        /// UpdatePassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="oldpassword">oldpassword.</param>
        /// <param name="newPassword">newPassword.</param>
        /// <returns>response.</returns>
        Task<string> UpdatePassword(string emailId, string oldpassword, string newPassword);

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        Task<string> ForgotPassword(string emailId, string password);
    }
}

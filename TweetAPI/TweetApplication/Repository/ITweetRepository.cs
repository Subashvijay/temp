// <copyright file="ITweetRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApplication.Models;

namespace TweetApplication.Repository
{
    /// <summary>
    /// Interface for tweet repository
    /// </summary>
    public interface ITweetRepository
    {
        /// <summary>
        /// Validate email id
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <returns>No of rows affected</returns>
        Task<bool> ValidateEmailId(string emailId);

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userInfo">User information</param>
        /// <returns>No of rows affected</returns>
        Task<int> RegisterUser(UserInfo userInfo);

        /// <summary>
        /// Verify user credentials when user tries to login
        /// </summary>
        /// <param name="username">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>True if user found else false</returns>
        Task<UserInfo> VerifyUserCredentials(string username, string password);

        /// <summary>
        /// Post tweet
        /// </summary>
        /// <param name="tweet">Tweet</param>
        /// <returns>No of rows affected</returns>
        Task<int> PostTweet(Tweet tweet);

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
        /// <returns>No of rows affected</returns>
        Task<int> UpdateUserPassword(UserCredentials userCredentials);

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>No of rows affected</returns>
        Task<int> ResetUserPassword(Tuple<string, string> updateData);

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
        /// <param name="userid">userid.</param>
        /// <returns>response.</returns>
        Task<int> Comments(string comment, string username, string userName, string tweet, DateTime date);

        /// <summary>
        /// Likes.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        Task<List<UserComments>> GetComments(string username, string tweet);

        /// <summary>
        /// UpdatePassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="oldpassword">oldpassword.</param>
        /// <param name="newPassword">newPassword.</param>
        /// <returns>response.</returns>
        Task<bool> UpdatePassword(string emailId, string oldpassword, string newPassword);

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        Task<bool> ForgotPassword(string emailId, string password);

        /// <summary>
        /// DeleteTweet.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        Task<int> DeleteTweet(string username, string tweet);
    }
}

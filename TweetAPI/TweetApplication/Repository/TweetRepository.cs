// <copyright file="TweetRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using ProductMaintenanceService.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApplication.Models;

namespace TweetApplication.Repository
{
    /// <summary>
    /// Tweet repository
    /// </summary>
    public class TweetRepository : ITweetRepository
    {
        /// <summary>
        /// Tweet app database context
        /// </summary>
        private readonly tweetAppDatabaseContext dbcontext;

        /// <summary>
        /// Tweet repository
        /// </summary>
        /// <param name="context">Tweet app database context</param>
        public TweetRepository(tweetAppDatabaseContext context)
        {
            dbcontext = context;
        }

        /// <summary>
        /// Validate email id
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <returns>True if email id validated else false</returns>
        public async Task<bool> ValidateEmailId(string emailId)
        {
            UserInfo user = await dbcontext.UserInfos.SingleOrDefaultAsync(e => e.Email == emailId);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userInfo">User information</param>
        /// <returns>No of rows affected</returns>
        public async Task<int> RegisterUser(UserInfo userInfo)
        {
            int result = 0;
            try
            {
                dbcontext.UserInfos.Add(userInfo);
                result = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TweetApplicationException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Verify user credentials when user tries to login
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>True if user found else false</returns>
        public async Task<UserInfo> VerifyUserCredentials(string username, string password)
        {
            return await dbcontext.UserInfos.SingleOrDefaultAsync(e => e.UserName == username && e.Password == password);
        }

        /// <summary>
        /// UpdatePassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="oldpassword">oldpassword.</param>
        /// <param name="newPassword">newPassword.</param>
        /// <returns>response.</returns>
        public async Task<bool> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            var update = await this.dbcontext.UserInfos.Where(x => x.Email == emailId && x.Password == oldpassword).FirstOrDefaultAsync();
            if (update != null)
            {
                update.Password = newPassword;
                this.dbcontext.UserInfos.Update(update);
                var result = await this.dbcontext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        public async Task<bool> ForgotPassword(string emailId, string password)
        {
            var result = await this.dbcontext.UserInfos.Where(s => s.Email == emailId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Password = password;
                this.dbcontext.Update(result);
                var response = this.dbcontext.SaveChanges();
                if (response > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Post tweet
        /// </summary>
        /// <param name="tweet">Tweet</param>
        /// <returns>No of rows affected</returns>
        public async Task<int> PostTweet(Tweet tweet)
        {
            int result = 0;
            try
            {
                dbcontext.Tweets.Add(tweet);
                result = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TweetApplicationException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Get tweet based on user id
        /// </summary>
        /// <param name="userId">User if</param>
        /// <returns>Tweets</returns>
        public async Task<IEnumerable<Tweet>> GetTweet(int userId)
        {
            return await dbcontext.Tweets.Where(i => i.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<IEnumerable<UserInfo>> GetAllUsers()
        {
            return await dbcontext.UserInfos.ToListAsync();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<UserInfo> GetUserProfile(string userName)
        {
            return await this.dbcontext.UserInfos.Where(s => s.UserName == userName).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get tweets
        /// </summary>
        /// <returns>Tweets</returns>
        public async Task<IEnumerable<UserTweets>> GetAllTweets()
        {
            return await (from tweet in this.dbcontext.Tweets join user in this.dbcontext.UserInfos on tweet.UserId equals user.Id select new UserTweets { UserName = user.UserName, Tweets = tweet.Text, Imagename = user.ImageName, TweetDate = tweet.TweetDate, FirstName = user.FirstName, LastName = user.LastName, Likes = tweet.Likes }).ToListAsync();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<List<UserTweets>> GetTweetsByUser(string userName)
        {
            var users = await this.dbcontext.UserInfos.FirstOrDefaultAsync(e => e.UserName == userName);
            var result = await (from tweet in this.dbcontext.Tweets join user in this.dbcontext.UserInfos on tweet.UserId equals user.Id where tweet.UserId == users.Id select new UserTweets { UserName = user.UserName, Tweets = tweet.Text, Imagename = user.ImageName, TweetDate = tweet.TweetDate, FirstName = user.FirstName, LastName = user.LastName, Likes = tweet.Likes }).ToListAsync();
            return result;
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="userCredentials">User credentials</param>
        /// <returns>No of rows affected</returns>
        public async Task<int> UpdateUserPassword(UserCredentials userCredentials)
        {
            int result = 0;
            try
            {
                var update = await dbcontext.UserInfos.Where(x => x.Email == userCredentials.EmailId && x.Password == userCredentials.OldPassword).FirstOrDefaultAsync();
                if (update != null)
                {
                    update.Password = userCredentials.NewPassword;
                    dbcontext.Update(update);
                    result = dbcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new TweetApplicationException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>No of rows affected</returns>
        public async Task<int> ResetUserPassword(Tuple<string, string> updateData)
        {
            int result = 0;
            try
            {
                var update = await dbcontext.UserInfos.Where(x => x.Email == updateData.Item1).FirstOrDefaultAsync();
                if (update != null)
                {
                    update.Password = updateData.Item2;
                    dbcontext.Update(update);
                    result = dbcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new TweetApplicationException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Likes.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        public async Task<int> Likes(string username, string tweet)
        {
            var result = await this.dbcontext.Tweets.Where(s => s.UserName == username && s.Text == tweet).FirstOrDefaultAsync();
            result.Likes++;
            this.dbcontext.Tweets.Update(result);
            await this.dbcontext.SaveChangesAsync();
            return result.Likes;
        }

        /// <summary>
        /// Comments.
        /// </summary>
        /// <param name="comment">.</param>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <param name="date">date.</param>
        /// <returns>response.</returns>
        public async Task<int> Comments(string comment, string username, string userName, string tweet, DateTime date)
        {
            Comment comments = new Comment();
            int results = 0;
            var result = await this.dbcontext.Tweets.Where(s => s.UserName == userName && s.Text == tweet).FirstOrDefaultAsync();
            if (result != null)
            {
                comments.TweetId = result.Id;
                comments.UserName = username;
                comments.Comments = comment;
                comments.Date = date;
                this.dbcontext.Add(comments);
                results = await this.dbcontext.SaveChangesAsync();
            }

            return results;
        }

        public async Task<List<UserComments>> GetComments(string username, string tweet)
        {
            var result = await this.dbcontext.Tweets.Where(s => s.UserName == username && s.Text == tweet).FirstOrDefaultAsync();
            var result1 = await (from commentss in this.dbcontext.Comments join users in this.dbcontext.UserInfos on username equals users.UserName where commentss.TweetId == result.Id select new UserComments { Username = commentss.UserName, Comments = commentss.Comments, Imagename = users.ImageName, Date = commentss.Date }).ToListAsync();
            return result1;
        }

        public async Task<int> DeleteTweet(string username, string tweet)
        {
            var result = await this.dbcontext.Tweets.Where(s => s.UserName == username && s.Text == tweet).FirstOrDefaultAsync();
            this.dbcontext.Remove(result);
            var response = await this.dbcontext.SaveChangesAsync();
            return response;
        }
    }
}

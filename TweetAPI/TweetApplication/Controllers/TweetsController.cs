// <copyright file="TweetsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TweetApplication.Commands;
using TweetApplication.Common;
using TweetApplication.Exceptions;
using TweetApplication.Models;
using TweetApplication.Service;

namespace TweetApplication.Controller
{
    /// <summary>
    /// Tweet controller
    /// </summary>
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Tweet service
        /// </summary>
        private readonly ITweetService tweetservice;

        /// <summary>
        /// Logger details
        /// </summary>
        private readonly ILogger<TweetsController> logger;

        /// <summary>
        /// Mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetsController"/> class.
        /// </summary>
        /// <param name="tweetservice">Tweet service</param>
        /// <param name="logger">Logger</param>
        public TweetsController(ITweetService tweetservice, ILogger<TweetsController> logger, IMediator mediator, IConfiguration configuration)
        {
            this.tweetservice = tweetservice;
            this.logger = logger;
            this.mediator = mediator;
            this.configuration = configuration;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userInfo">User information</param>
        /// <returns>No content if registered successfully else bad request</returns>
        [Route("register")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserCommand request)
        {
            string validationResult = string.Empty;
            if (request != null && request.UserInfo != null)
            {
                validationResult = await this.mediator.Send(request);
                if (string.IsNullOrWhiteSpace(validationResult))
                {
                    logger.LogTrace($"User registeration succeeded for: {JsonConvert.SerializeObject(request.UserInfo)}");
                    return this.NoContent();
                }
            }
            else
            {
                validationResult = Constants.InvalidRequestError;
            }
            logger.LogError(validationResult);
            return this.BadRequest(ValidationResult.ValidationMessage(new List<string>(new[] { validationResult })));
        }

        /// <summary>
        /// Verify user credentials when user tries to login
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <param name="password">Password</param>
        /// <returns>Ok result with true or false if user found else bad request</returns>
        [HttpGet]
        [Route("login/{username},{password}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VerifyUserCredentials(string username, string password)
        {
            Token token = null;
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                UserInfo result = await this.tweetservice.VerifyUserCredentials(username, password);
                if (result != null)
                {
                    token = new Token() { UserId = result.Id, Username = result.UserName, Tokens = this.GenerateJwtToken(username), Message = "Success" };
                }
                else
                {
                    token = new Token() { Tokens = null, Message = "UnSuccess" };
                }

                return this.Ok(token);
            }
            logger.LogError(Constants.InvalidRequestError);
            return this.BadRequest(ValidationResult.ValidationMessage(new List<string>(new[] { Constants.InvalidRequestError })));
        }

        /// <summary>
        /// Post tweet
        /// </summary>
        /// <param name="request">Post tweet command object</param>
        /// <returns>No content if tweet posted successfully else bad request</returns>
        [Route("tweet")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostTweet(PostTweetCommand request)
        {
            string validationResult = string.Empty;
            if (request != null && request.Tweet != null)
            {
                string logDetail = JsonConvert.SerializeObject(request);
                validationResult = await this.mediator.Send(request);
                if (string.IsNullOrWhiteSpace(validationResult))
                {
                    logger.LogTrace($"Tweet posted successfully for: {logDetail}");
                    return this.NoContent();
                }
            }
            else
            {
                validationResult = Constants.InvalidRequestError;
            }
            logger.LogError(validationResult);
            return this.BadRequest(ValidationResult.ValidationMessage(new List<string>(new[] { validationResult })));
        }

        /// <summary>
        /// Get tweet based on user id
        /// </summary>
        /// <param name="userId">User if</param>
        /// <returns>Ok result with tweets if tweets are returned else if no tweet found no content else bad request</returns>
        [HttpGet]
        [Route("~/api/Users/{userId}/[controller]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTweet(int userId)
        {
            if (userId > 0)
            {
                IEnumerable<Tweet> result = await this.tweetservice.GetTweet(userId);
                if (result.Any())
                {
                    logger.LogTrace($"Tweets retrieved successfully for user id: {userId}");
                    return this.Ok(result);
                }
                logger.LogTrace($"No tweets found for user id: {userId}");
                return this.NoContent();
            }
            logger.LogError(Constants.InvalidRequestError);
            return this.BadRequest(ValidationResult.ValidationMessage(new List<string>(new[] { Constants.InvalidRequestError })));
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Ok result with users if user info are returned else if no user found no content else bad request</returns>
        [Route("users/all")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<UserInfo> result = await this.tweetservice.GetAllUsers();
            if (result.Any())
            {
                return this.Ok(result);
            }
            return this.NoContent();
        }

        /// <summary>
        /// GetUserProfile.
        /// </summary>
        /// <param name="username">username.</param>
        /// <returns>response.</returns>
        [HttpGet]
        [Route("user/{username}")]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            UserInfo result = await this.tweetservice.GetUserProfile(username);
            return this.Ok(result);
        }

        /// <summary>
        /// Get Tweets By Users.
        /// </summary>
        /// <param name="username">username.</param>
        /// <returns>response.</returns>
        [HttpGet]
        [Route("user/search/{username}")]
        public async Task<IActionResult> GetTweetsByUser(string username)
        {
                var result = await this.tweetservice.GetTweetsByUser(username);
                return this.Ok(result);
        }

        /// <summary>
        /// Get tweets
        /// </summary>
        /// <returns>Ok result with tweets if tweets are returned else if no tweet found no content else bad request</returns>
        [Route("all")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllTweets()
        {
            IEnumerable<UserTweets> result = await this.tweetservice.GetAllTweets();
            if (result.Any())
            {
                return this.Ok(result);
            }
            return this.NoContent();
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="request">Update user password command request</param>
        /// <returns>No content if user password updated successfully else bad request</returns>
        [Route("update/{emailId},{oldpassword},{newpassword}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserPassword(string emailId, string oldpassword, string newPassword)
        {
                var result = await this.tweetservice.UpdatePassword(emailId, oldpassword, newPassword);
                return this.Ok(result);
        }

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailId">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        [HttpPut]
        [Route("forgot/{emailId},{password}")]
        public async Task<IActionResult> ForgotPassword(string emailId, string password)
        {
                var result = await this.tweetservice.ForgotPassword(emailId, password);
                return this.Ok(result);
        }

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="request">Reset user password command request</param>
        /// <returns>No content if user password reset succeeded else bad request</returns>
        [Route("~/api/Users/{emailId}/PasswordReset")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetUserPassword([FromBody]ResetUserPasswordCommand request, string emailId)
        {
            string validationResult = string.Empty;
            if (request != null && !string.IsNullOrWhiteSpace(request.Password) && !string.IsNullOrWhiteSpace(emailId) && request.EmailId == emailId)
            {
                validationResult = await this.mediator.Send(request);
                if (string.IsNullOrWhiteSpace(validationResult))
                {
                    logger.LogTrace($"User password reset succeeded for usern having email id: {request.EmailId}");
                    return this.NoContent();
                }
            }
            else
            {
                validationResult = Constants.InvalidRequestError;
            }
            logger.LogError(validationResult);
            return this.BadRequest(ValidationResult.ValidationMessage(new List<string>(new[] { validationResult })));
        }

        /// <summary>
        /// Likes.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        [HttpGet]
        [Route("likes/{username},{tweet}")]
        public async Task<IActionResult> GetLikes(string username, string tweet)
        {
                var result = await this.tweetservice.Likes(username, tweet);
                return this.Ok(result);
        }

        /// <summary>
        /// Comments.
        /// </summary>
        /// <param name="comment">comment.</param>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <param name="date">date.</param>
        /// <returns>response.</returns>
        [HttpPost]
        [Route("reply/{comment},{username},{Name},{tweet}")]
        public async Task<IActionResult> PostComment(string comment, string username, string Name, string tweet)
        {
                var result = await this.tweetservice.Comments(comment, username, Name, tweet);
                return this.Ok(result);
        }

        /// <summary>
        /// Get All Comments.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        [HttpGet]
        [Route("allcomments/{username},{tweet}")]
        public async Task<IActionResult> GetAllComments(string username, string tweet)
        {
                var result = await this.tweetservice.GetComments(username, tweet);
                return this.Ok(result);
        }

        /// <summary>
        /// Delete Tweet.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="tweet">tweet.</param>
        /// <returns>response.</returns>
        [HttpDelete]
        [Route("tweetdelete/{username},{tweet}")]
        public async Task<IActionResult> DeleteTweet(string username, string tweet)
        {
            try
            {
                var result = await this.tweetservice.DeleteTweet(username, tweet);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while Deleteing user tweet");
                throw;
            }
        }

        private string GenerateJwtToken(string emailId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, emailId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, emailId),
                new Claim(ClaimTypes.Role, emailId),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //recommended is 5 min
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                this.configuration["JwtIssuer"],
                this.configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

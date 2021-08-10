// <copyright file="RegisterUserCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using System.Runtime.Serialization;
using TweetApplication.Models;

namespace TweetApplication.Commands
{
    /// <summary>
    /// Register user command
    /// </summary> 
    public class RegisterUserCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommand"/> class.
        /// </summary>
        /// <param name="userInfo">User information</param>
        public RegisterUserCommand(UserInfo userInfo)
        {
            this.UserInfo = userInfo;
        }

        /// <summary>
        /// Gets user information
        /// </summary>
        [DataMember]
        public UserInfo UserInfo { get; private set; }
    }
}

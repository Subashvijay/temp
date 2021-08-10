// <copyright file="ResetUserPasswordCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using System.Runtime.Serialization;

namespace TweetApplication.Commands
{
    /// <summary>
    /// Register user command
    /// </summary> 
    public class ResetUserPasswordCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetUserPasswordCommand"/> class.
        /// </summary>
        /// <param name="userInfo">User information</param>
        public ResetUserPasswordCommand(string emailId, string password)
        {
            this.EmailId = emailId;
            this.Password = password;
        }

        /// <summary>
        /// Gets email id
        /// </summary>
        [DataMember]
        public string EmailId { get; private set; }

        /// <summary>
        /// Gets password
        /// </summary>
        [DataMember]
        public string Password { get; private set; }
    }
}

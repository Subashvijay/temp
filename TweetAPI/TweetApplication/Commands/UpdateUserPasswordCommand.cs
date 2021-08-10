// <copyright file="UpdateUserPasswordCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using System.Runtime.Serialization;
using TweetApplication.Models;

namespace TweetApplication.Commands
{
    /// <summary>
    /// Update user password command
    /// </summary> 
    public class UpdateUserPasswordCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserPasswordCommand"/> class.
        /// </summary>
        /// <param name="userCredentials">User credentials</param>
        public UpdateUserPasswordCommand(UserCredentials userCredentials)
        {
            this.UserCredentials = userCredentials;
        }

        /// <summary>
        /// Gets user credentials to be updated
        /// </summary>
        [DataMember]
        public UserCredentials UserCredentials { get; private set; }
    }
}

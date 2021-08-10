// <copyright file="PostTweetCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using System.Runtime.Serialization;
using TweetApplication.Models;

namespace TweetApplication.Commands
{
    /// <summary>
    /// Post tweet command
    /// </summary> 
    public class PostTweetCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTweetCommand"/> class.
        /// </summary>
        /// <param name="tweet">Tweet information</param>
        public PostTweetCommand(Tweet tweet)
        {
            this.Tweet = tweet;
        }

        /// <summary>
        /// Gets tweet information
        /// </summary>
        [DataMember]
        public Tweet Tweet { get; private set; }
    }
}

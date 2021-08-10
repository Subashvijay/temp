// <copyright file="PostTweetCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TweetApplication.Commands;
using TweetApplication.Service;

namespace TweetApplication.CommandHandlers
{
    /// <summary>
    /// Post tweet command
    /// </summary>
    public class PostTweetCommandHandler : IRequestHandler<PostTweetCommand, string>
    {
        /// <summary>
        /// Tweet service
        /// </summary>
        private readonly ITweetService tweetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostTweetCommandHandler"/> class.
        /// </summary>
        /// <param name="tweetService">Tweet service</param>
        public PostTweetCommandHandler(ITweetService tweetService)
        {
            this.tweetService = tweetService;
        }

        /// <summary>
        /// Handler which processes the command when user tries to post tweet
        /// </summary>
        /// <param name="request">Post tweet command request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Empty string if succeed else error message</returns>
        public async Task<string> Handle(PostTweetCommand request, CancellationToken cancellationToken)
        {
            return await this.tweetService.PostTweet(request.Tweet);
        }
    }
}

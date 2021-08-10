// <copyright file="ResetUserPasswordCommandHandler.cs" company="PlaceholderCompany">
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
    /// Reset user password command
    /// </summary>
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, string>
    {
        /// <summary>
        /// Tweet service
        /// </summary>
        private readonly ITweetService tweetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetUserPasswordCommandHandler"/> class.
        /// </summary>
        /// <param name="tweetService">Tweet service</param>
        public ResetUserPasswordCommandHandler(ITweetService tweetService)
        {
            this.tweetService = tweetService;
        }

        /// <summary>
        /// Handler which processes the command when user tries to reset password
        /// </summary>
        /// <param name="request">Post tweet command request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Empty string if succeed else error message</returns>
        public async Task<string> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await this.tweetService.ResetUserPassword(request.EmailId, request.Password);
        }
    }
}

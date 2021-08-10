// <copyright file="UpdateUserPasswordCommandHandlers.cs" company="PlaceholderCompany">
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
    /// Update user password command handler
    /// </summary>
    public class UpdateUserPasswordCommandHandlers : IRequestHandler<UpdateUserPasswordCommand, string>
    {
        /// <summary>
        /// Tweet service
        /// </summary>
        private readonly ITweetService tweetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserPasswordCommandHandlers"/> class.
        /// </summary>
        /// <param name="tweetService">Tweet service</param>
        public UpdateUserPasswordCommandHandlers(ITweetService tweetService)
        {
            this.tweetService = tweetService;
        }

        /// <summary>
        /// Handler which processes the command when user tries to update his/her password
        /// </summary>
        /// <param name="request">Update user password command request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Empty string if succeed else error message</returns>
        public async Task<string> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await this.tweetService.UpdateUserPassword(request.UserCredentials);
        }
    }
}

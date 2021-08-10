// <copyright file="RegisterUserCommandHandler.cs" company="PlaceholderCompany">
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
    /// Register user command
    /// </summary>
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        /// <summary>
        /// Tweet service
        /// </summary>
        private readonly ITweetService tweetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommandHandler"/> class.
        /// </summary>
        /// <param name="tweetService">Tweet service</param>
        public RegisterUserCommandHandler(ITweetService tweetService)
        {
            this.tweetService = tweetService;
        }

        /// <summary>
        /// Handler which processes the command when user tries to register
        /// </summary>
        /// <param name="request">Register user command request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Empty string if succeed else error message</returns>
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await this.tweetService.RegisterUser(request.UserInfo);
        }
    }
}

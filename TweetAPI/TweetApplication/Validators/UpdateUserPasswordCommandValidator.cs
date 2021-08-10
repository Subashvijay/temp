// <copyright file="UpdateUserPasswordCommandValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using TweetApplication.Commands;
using TweetApplication.Repository;

namespace TweetApplication.Validators
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        /// <summary>
        /// Tweet repository
        /// </summary>
        private readonly ITweetRepository tweetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserPasswordCommandValidator"/> class.
        /// </summary>
        public UpdateUserPasswordCommandValidator(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
            this.RuleFor(command => command.UserCredentials.EmailId).NotEmpty().WithMessage("Email id is required to update password.");
            this.RuleFor(command => command.UserCredentials.OldPassword).NotEmpty().WithMessage("Old password is required to update password.");
            this.RuleFor(command => command.UserCredentials.NewPassword).NotEmpty().WithMessage("New password should not be empty.");
        }
    }
}

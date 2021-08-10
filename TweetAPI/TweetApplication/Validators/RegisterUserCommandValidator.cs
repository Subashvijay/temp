// <copyright file="RegisterUserCommandValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using TweetApplication.Commands;
using TweetApplication.Repository;

namespace TweetApplication.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        /// <summary>
        /// Tweet repository
        /// </summary>
        private readonly ITweetRepository tweetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommandValidator"/> class.
        /// </summary>
        public RegisterUserCommandValidator(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
            this.RuleFor(command => command.UserInfo.FirstName).NotEmpty().WithMessage("First name should not be empty.");
            this.RuleFor(command => command.UserInfo.Email).NotEmpty().WithMessage("Email id is required to register.");
            this.RuleFor(command => command.UserInfo.Password).NotEmpty().WithMessage("Password is required to register.");
            this.RuleFor(command => command.UserInfo).NotEmpty().MustAsync(async (userInfo, ct) => await this.tweetRepository.ValidateEmailId(userInfo.Email)).WithMessage("Email id already used. Enter a different email id");
        }
    }
}

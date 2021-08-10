// <copyright file="PostTweetCommandValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using TweetApplication.Commands;
using TweetApplication.Repository;

namespace TweetApplication.Validators
{
    public class PostTweetCommandValidator : AbstractValidator<PostTweetCommand>
    {
        /// <summary>
        /// Tweet repository
        /// </summary>
        private readonly ITweetRepository tweetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostTweetCommandValidator"/> class.
        /// </summary>
        public PostTweetCommandValidator(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
            this.RuleFor(command => command.Tweet.UserId).GreaterThan(0).WithMessage("User id shpuld be greater than 0.");
            this.RuleFor(command => command.Tweet.Text).NotEmpty().WithMessage("Tweet should not be empty.");
        }
    }
}

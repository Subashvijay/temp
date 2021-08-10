// <copyright file="JsonErrorResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace TweetApplication.Models
{
    /// <summary>
    /// The json error response.
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// Gets or sets the developer message.
        /// </summary>
        public dynamic DeveloperMessage { get; set; }
    }
}

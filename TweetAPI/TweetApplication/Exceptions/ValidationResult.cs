// <copyright file="ValidationResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using TweetApplication.Models;

namespace TweetApplication.Exceptions
{
    /// <summary>
    /// Validation result
    /// </summary>
    public static class ValidationResult
    {
        /// <summary>
        /// To assign the error messages in json response format
        /// </summary>
        /// <param name="errorMessages">Error message</param>
        /// <returns>Error messages in json response</returns>
        public static JsonErrorResponse ValidationMessage(IEnumerable<string> errorMessages)
        {
            var response = new JsonErrorResponse
            {
                Messages = errorMessages,
                DeveloperMessage = string.Empty
            };
            return response;
        }
    }
}

// <copyright file="CustomException.cs" company="Trane Company">
// Copyright (c) Trane Company. All rights reserved.
// </copyright>

namespace ProductMaintenanceService.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Product Maintenance Service Domain Exception
    /// </summary>
    [Serializable]
    public class TweetApplicationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TweetApplicationException"/> class.
        /// </summary>
        public TweetApplicationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetApplicationException"/> class.
        /// </summary>
        /// <param name="message">message value</param>
        public TweetApplicationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetApplicationException"/> class.
        /// </summary>
        /// <param name="message">message value</param>
        /// <param name="innerException">inner Exception</param>
        public TweetApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMaintenanceServiceDomainException"/> class.
        /// </summary>
        /// <param name="info">the serialization info</param>
        /// <param name="context">the streaming context</param>
        protected TweetApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

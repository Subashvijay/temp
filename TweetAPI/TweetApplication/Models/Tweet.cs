// <copyright file="Tweet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApplication.Models
{
    public partial class Tweet
    {
        public Tweet()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime TweetDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Likes { get; set; }

        public virtual UserInfo User { get; set; }
        public virtual UserInfo UserNameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

// <copyright file="Comment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApplication.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int TweetId { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }

        public virtual Tweet Tweet { get; set; }
        public virtual UserInfo UserNameNavigation { get; set; }
    }
}

// <copyright file="UserInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;

#nullable disable

namespace TweetApplication.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Comments = new HashSet<Comment>();
            TweetUserNameNavigations = new HashSet<Tweet>();
            TweetUsers = new HashSet<Tweet>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ContactNumber { get; set; }
        public string UserName { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tweet> TweetUserNameNavigations { get; set; }
        public virtual ICollection<Tweet> TweetUsers { get; set; }
    }
}

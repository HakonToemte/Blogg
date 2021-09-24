using System;
using System.Collections.Generic;

namespace Blogg
{
    public class Blog
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Post> Posts {get;set;}
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogg
{
    public class Blog : IdentityUser
    {
        public virtual ICollection<Post> Posts {get;set;}
    }
}
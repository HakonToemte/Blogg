using System;

namespace Blogg
{
    public class BlogPost
    {
        public int Id { get; set; }
        public virtual User User {get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
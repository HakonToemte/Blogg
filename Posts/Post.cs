using System;

namespace Blogg
{
    public class Post
    {
        public int Id { get; set; }
        public virtual Blog Blog {get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
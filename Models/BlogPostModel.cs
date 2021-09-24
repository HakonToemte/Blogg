using System;

namespace Blogg
{
    public class BlogPostModel
    {
        public int Id { get; set; }
        public virtual User User {get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}

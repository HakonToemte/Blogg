using System.Collections.Generic;

namespace Blogg
{
    public interface IBlogPostValidator
    {
        string[] IsValid(BlogPost blogPost);
    }
}
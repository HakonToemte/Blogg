using System.Collections.Generic;

namespace Blogg
{
    public interface IPostValidator
    {
        string[] IsValid(Post post);
    }
}
using System.Collections.Generic;

namespace Blogg
{
    public interface IBlogValidator
    {
        string[] IsValid(Blog blog);
    }
}
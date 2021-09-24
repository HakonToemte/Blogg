using System.Collections.Generic;

namespace Blogg
{
    public interface IUserValidator
    {
        string[] IsValid(User user);
    }
}
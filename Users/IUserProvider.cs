using System.Threading.Tasks;
using System.Collections.Generic;

namespace Blogg
{
    public interface IUserProvider
    {
        Task<User[]> GetUsers();
        Task AddUser(User user);
        Task<User> GetUser(string name);
        Task UpdateUser(int id, User user);
        Task RemoveUser(int id);
    }
}

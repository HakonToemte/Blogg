using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blogg
{
    public class UserProvider : IUserProvider
    {
        private readonly BlogContext _blogContext;

        public UserProvider(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<User[]> GetUsers()
        {
            var list = await _blogContext.Users.ToListAsync();
            User[] new_list = list.ToArray();
            return new_list;
        }

        public Task AddUser(User user){
            _blogContext.Users.Add(user);
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<User> GetUser(string name){
            var user = _blogContext.Users.Where(b => b.Name == name)
                    .FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return await Task.Run(() => user);
        }

        public Task UpdateUser(int id, User user)
        {
            _blogContext.Users.Update(user);
            _blogContext.SaveChanges();
            
            return Task.CompletedTask;
        }

        public Task RemoveUser(int id)
        {
            _blogContext.Users.Remove(_blogContext.Users.Find(id));
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        
    }
}

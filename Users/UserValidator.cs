using System.Collections.Generic;
using System;

namespace Blogg
{
    public class UserValidator : IUserValidator
    {
        public string[] IsValid(User user){
            List<string> list= new List<string>();
            if (user.Name == null){
                list.Add("Name can not be empty");
            }
            if (user.PasswordHash == null){
                list.Add("Password can not be empty");
            }
            string[] array = list.ToArray();
            return array;
        }
    }
}
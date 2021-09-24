using System.Collections.Generic;
using System;

namespace Blogg
{
    public class BlogValidator : IBlogValidator
    {
        public string[] IsValid(Blog blog){
            List<string> list= new List<string>();
            if (blog.UserName == null){
                list.Add("Name can not be empty");
            }
            if (blog.PasswordHash == null){
                list.Add("Password can not be empty");
            }
            string[] array = list.ToArray();
            return array;
        }
    }
}
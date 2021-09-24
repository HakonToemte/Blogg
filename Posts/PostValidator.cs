using System.Collections.Generic;
using System;

namespace Blogg
{
    public class PostValidator : IPostValidator
    {
        public string[] IsValid(Post post){
            List<string> list= new List<string>();
            if (post.Title == null){
                list.Add("Title can not be empty");
            }
            if (post.Text == null){
                list.Add("Text can not be empty");
            }
            string[] array = list.ToArray();
            return array;
        }
    }
}
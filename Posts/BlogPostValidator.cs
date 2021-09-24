using System.Collections.Generic;
using System;

namespace Blogg
{
    public class BlogPostValidator : IBlogPostValidator
    {
        public string[] IsValid(BlogPost blogPost){
            List<string> list= new List<string>();
            if (blogPost.Title == null){
                list.Add("Title can not be empty");
            }
            if (blogPost.Text == null){
                list.Add("Text can not be empty");
            }
            string[] array = list.ToArray();
            return array;
        }
    }
}
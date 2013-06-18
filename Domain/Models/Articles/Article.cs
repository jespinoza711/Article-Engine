using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Articles
{
    public class Article : IArticle
    {
        public Article()
        {
            Tags = new List<Tag>();
        }

        public string Summary { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}

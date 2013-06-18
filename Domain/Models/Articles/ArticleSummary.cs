using System;
using System.Collections.Generic;

namespace Models.Articles
{
    public class ArticleSummary : IArticle
    {

        public ArticleSummary()
        {
            Tags = new List<Tag>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public IList<Tag> Tags { get; set; }

        public bool IsPublished { get; set; }
    }
}

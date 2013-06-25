using System.Collections.Generic;
using Models.Tags;

namespace Models.Articles
{
    public interface IArticle
    {
        string Summary { get; set; }
        IList<Tag> Tags { get; set; }
    }
}
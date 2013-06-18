using System.Collections.Generic;

namespace Models.Articles
{
    public interface IArticle
    {
        string Summary { get; set; }
        IList<Tag> Tags { get; set; }
    }
}
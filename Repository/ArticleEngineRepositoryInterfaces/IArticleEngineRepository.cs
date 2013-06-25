using System;
using System.Collections.Generic;
using Models;
using Models.Articles;
using Models.Tags;

namespace ArticleEngineRepositoryInterfaces
{
    public interface IArticleEngineRepository
    {
        IList<ArticleSummary> GetAllArticles();
        ArticleOld GetArticle(Guid id);
        IList<ArticleSummary> GetArticles(params Tag[] tags);
        void SaveArticle(ArticleOld articleOld);
        void DeleteArticle(ArticleOld articleOld);
        IList<Tag> GetTags();
        Dictionary<Tag, int> CountTags();
    }
}

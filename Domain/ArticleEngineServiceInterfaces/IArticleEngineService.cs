using System;
using System.Collections.Generic;
using Models;
using Models.Articles;
using Models.Tags;

namespace ArticleEngineServiceInterfaces
{
    public interface IArticleEngineService
    {
        IList<ArticleSummary> GetArticles();
        IList<ArticleSummary> GetPublishedArticles();

        ArticleOld GetArticle(Guid id);

        IList<ArticleSummary> GetArticles(params Tag[] tags);

        void SaveArticle(ArticleOld articleOld);

        void DeleteArticle(ArticleOld articleOld);

        IList<Tag> GetTags();

        Dictionary<Tag, int> CountTags();
    }
}

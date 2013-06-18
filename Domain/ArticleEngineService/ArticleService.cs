using System;
using System.Collections.Generic;
using ArticleEngineRepositoryInterfaces;
using ArticleEngineServiceInterfaces;
using Models;
using Models.Articles;

namespace ArticleEngineService
{
    public class ArticleService : IArticleEngineService
    {
        private IArticleEngineRepository _articleEngineRepository;

        public ArticleService(IArticleEngineRepository articleEngineRepository)
        {
            _articleEngineRepository = articleEngineRepository;
        }

        public IList<ArticleSummary> GetArticles()
        {
            return _articleEngineRepository.GetAllArticles();
        }

        public IList<ArticleSummary> GetPublishedArticles()
        {
            throw new NotImplementedException();
        }

        public ArticleOld GetArticle(Guid id)
        {
            return _articleEngineRepository.GetArticle(id);
        }

        public IList<ArticleSummary> GetArticles(params Tag[] tags)
        {
            return _articleEngineRepository.GetArticles(tags);
        }

        public void SaveArticle(ArticleOld articleOld)
        {
            if (articleOld != null)
            {
                _articleEngineRepository.SaveArticle(articleOld);
            }
        }

        public void DeleteArticle(ArticleOld articleOld)
        {
            if (articleOld != null)
            {
                _articleEngineRepository.DeleteArticle(articleOld);
            }
        }

        public IList<Tag> GetTags()
        {
            return _articleEngineRepository.GetTags();
        }

        public Dictionary<Tag, int> CountTags()
        {
            return _articleEngineRepository.CountTags();
        }
        
    }
}

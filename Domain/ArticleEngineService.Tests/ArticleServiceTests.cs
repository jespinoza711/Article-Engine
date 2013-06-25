using System;
using System.Collections.Generic;
using System.Linq;
using ArticleEngineRepositoryInterfaces;
using Models;
using Models.ArticleValidation;
using Models.Articles;
using Models.PublishingRules;
using Models.Tags;
using Moq;
using NUnit.Framework;

namespace ArticleEngineService.Tests
{
    [TestFixture]
    public class ArticleServiceTests
    {
        private Mock<IArticleEngineRepository> _articleRepository;

        private Tag _cSharp;
        private Tag _vb;
        private Tag _javaScript;

        private IList<ArticleOld> _articles;
        const string ArticleTitle = "My First Article";

        private ArticleSummary ConvertToSummary(ArticleOld articleOld)
        {
            return new ArticleSummary
            {
                Id = articleOld.Id,
                ArticleType =  articleOld.ArticleType,
                Description = articleOld.Description,                
                SubTitle = articleOld.SubTitle,
                Tags = articleOld.Tags,
                Title = articleOld.Title
            };
        }

        [SetUp]
        public void Init()
        {
            _articleRepository = new Mock<IArticleEngineRepository>();

            _cSharp = new Tag(Guid.NewGuid(), "C#");
            _vb = new Tag(Guid.NewGuid(), "VB");
            _javaScript = new Tag(Guid.NewGuid(), "JavaScript");

            _articles = new List<ArticleOld>
                {
                    new ArticleOld(ArticleType.Full, new FullArticleValidator(), new ArticlePublishingRules())
                    {
                        Id = Guid.NewGuid(),
                        Tags = new List<Tag> {_cSharp, _vb},
                        Title = ArticleTitle
                    },
                    new ArticleOld(ArticleType.Full, new FullArticleValidator(), new ArticlePublishingRules())
                    {
                        Id = Guid.NewGuid(),
                        Tags = new List<Tag> {_cSharp}
                    },
                    new ArticleOld(ArticleType.Full, new FullArticleValidator(), new ArticlePublishingRules())
                    {
                        Id = Guid.NewGuid(),
                    }
                };

            _articleRepository.Setup(x => x.GetAllArticles()).Returns(_articles.Select(ConvertToSummary).ToList);
            _articleRepository.Setup(x => x.GetArticle(It.IsAny<Guid>())).Returns((Guid id) => _articles.FirstOrDefault(x => x.Id.Equals(id)));

            _articleRepository.Setup(x => x.GetArticles(It.IsAny<Tag[]>())).Returns((Tag[] t) =>
                {
                    IList<ArticleSummary> returnArticles = new List<ArticleSummary>();
                    foreach (var tag in t)
                    {
                        foreach (ArticleOld article in _articles)
                        {
                            if (article.Tags.Any(ta => ta.Id.Equals(tag.Id)) && !returnArticles.Contains(article))
                            {
                                returnArticles.Add(ConvertToSummary(article));
                            }
                        }
                    }

                    return returnArticles;
                });

            _articleRepository.Setup(x => x.SaveArticle(It.IsAny<ArticleOld>())).Callback((ArticleOld a) => _articles.Add(a));
            _articleRepository.Setup(x => x.DeleteArticle(It.IsAny<ArticleOld>())).Callback((ArticleOld a) => _articles.Remove(a));
            _articleRepository.Setup(x => x.GetTags()).Returns(new List<Tag> {_cSharp, _vb, _javaScript});

            _articleRepository.Setup(x => x.CountTags()).Returns(() =>
                {
                    var tagCount = new Dictionary<Tag, int>();

                    foreach (ArticleOld article in _articles)
                    {
                        foreach (var tag in article.Tags)
                        {
                            if (tagCount.ContainsKey(tag))
                            {
                                tagCount[tag] = tagCount[tag] + 1;
                            }
                            else
                            {
                                tagCount.Add(tag, 1);
                            }
                        }
                    }

                    return tagCount;
                });
        }

        [Test]
        public void TestGetArticles()
        {
            int repoArticleCount = _articleRepository.Object.GetAllArticles().Count;

            var articleService = new ArticleService(_articleRepository.Object);

            Assert.AreEqual(repoArticleCount, articleService.GetArticles().Count);
        }

        [Test]
        public void TestThatValuesAreSetCorrectlyWhenArticleValid()
        {
            throw new NotImplementedException();            
        }

        [Test]
        public void TestThatValuesAreSetCorrectlyWhenArticleNotValid()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void TestGetPublishedArticles()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void TestAnArticleIsInCorrectStateToBePublished()
        {
            //what are the rules for an article to be published?

            //can not be a draft

            //must be valid

            //published from date must be <= datetime.now

            //published to must be null or > datetime.now

            //needs a title

            //needs a subtitle

            //needs a description

            //needs created by

            //needs created date time

            //has rendered body and is not empty

            


            throw new NotImplementedException();
        }

        [Test]
        public void TestFullArticleValidMethod()
        {
            //








            throw new NotImplementedException();
        }


        [Test]
        public void TestIsDescriptionFromTitleOrBody()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void TestLinkArticleValidMethod()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void TestGetArticle()
        {
            //test will not return an article for random GUID
            Guid unkonwnId = Guid.NewGuid();
            while (_articleRepository.Object.GetAllArticles().FirstOrDefault(a => a.Id.Equals(unkonwnId)) != null)
            {
                unkonwnId = Guid.NewGuid();
            }

            var articleService = new ArticleService(_articleRepository.Object);
            Assert.IsNull(articleService.GetArticle(unkonwnId));

            //test will return an article for known GUID
            Guid knownId = _articleRepository.Object.GetAllArticles().First().Id;
            ArticleOld articleOld = articleService.GetArticle(knownId);
            Assert.IsNotNull(articleOld);
            Assert.AreEqual(knownId, articleOld.Id);
        }

        [Test]
        public void TestGetArticleByTags()
        {
            //Test getting CSharp will return 2 articles
            var articleService = new ArticleService(_articleRepository.Object);

            IList<ArticleSummary> cSharpArticles = articleService.GetArticles(_cSharp);
            Assert.AreEqual(2, cSharpArticles.Count);
            
            //Test getting VB will return 1
            IList<ArticleSummary> vbArticles = articleService.GetArticles(_vb);
            Assert.AreEqual(1, vbArticles.Count);

            //Test getting JavaScript returns none
            IList<ArticleSummary> javaScriptArticles = articleService.GetArticles(_javaScript);
            Assert.AreEqual(0, javaScriptArticles.Count);
        }

        [Test]
        public void TestAddArticle()
        {
            var articleService = new ArticleService(_articleRepository.Object);

            var article = new ArticleOld(ArticleType.Full, new FullArticleValidator(), new ArticlePublishingRules())
                {
                    Id = Guid.NewGuid()
                };

            Assert.IsNull(articleService.GetArticle(article.Id));

            articleService.SaveArticle(article);

            ArticleOld returnedArticleOld = articleService.GetArticle(article.Id);
            Assert.IsNotNull(returnedArticleOld);
            Assert.AreEqual(article.Id, returnedArticleOld.Id);
        }

        [Test]
        public void TestEditArticle()
        {
            ArticleOld testArticleOld = _articles.FirstOrDefault(a => a.Title.Equals(ArticleTitle));
            Assert.IsNotNull(testArticleOld); //this gets the article with the given title for us to edit.. i.e. we now have the ID.

            var articleService = new ArticleService(_articleRepository.Object);

            var article = articleService.GetArticle(testArticleOld.Id);
            Assert.IsNotNull(article);

            string newTitle = "My New Title";
            Assert.AreNotEqual(testArticleOld.Title, newTitle);
            article.Title = newTitle;
            articleService.SaveArticle(article);

            var newArticle = articleService.GetArticle(testArticleOld.Id);
            Assert.IsNotNull(newArticle);
            Assert.AreEqual(newTitle, newArticle.Title);
        }

        [Test]
        public void TestDeleteArticle()
        {
            var articleService = new ArticleService(_articleRepository.Object);

            var article = new ArticleOld(ArticleType.Full, new FullArticleValidator(), new ArticlePublishingRules()) { Id = Guid.NewGuid(), Title = "my new article to be deleted" };
            int articleCount = articleService.GetArticles().Count;

            articleService.SaveArticle(article);
            Assert.AreEqual(articleCount + 1, articleService.GetArticles().Count);

            var testArticle = articleService.GetArticle(article.Id);
            Assert.IsNotNull(testArticle);

            articleService.DeleteArticle(article);
            Assert.AreEqual(articleCount, articleService.GetArticles().Count);
            Assert.IsNull(articleService.GetArticle(article.Id));
        }

        [Test]
        public void TestGetAllTags()
        {
            var articleService = new ArticleService(_articleRepository.Object);
            IList<Tag> tags = articleService.GetTags();

            Assert.IsNotNull(tags);
            Assert.AreEqual(3, tags.Count);
            Assert.Contains(_cSharp, tags.ToArray());
            Assert.Contains(_vb, tags.ToArray());
            Assert.Contains(_javaScript, tags.ToArray());
        }

        [Test]
        public void TestCountTags()
        {
            var articleService = new ArticleService(_articleRepository.Object);
            var countTags = articleService.CountTags();

            Assert.IsNotNull(countTags);
            Assert.AreEqual(2, countTags.Count);

            Assert.AreEqual(2, countTags[_cSharp]);
            Assert.AreEqual(1, countTags[_vb]);
        }

    }
}

using System;
using Models.ArticleValidation;
using Models.Articles;
using Models.PublishingRules;

namespace Models
{
    public class ArticleOld : ArticleSummary
    {
        private readonly IArticleValidator _validator;
        private readonly ArticlePublishingRules _publishingRules;

        public ArticleOld(ArticleType articleType, IArticleValidator validator, ArticlePublishingRules publishingRules)
        {
            ArticleType = articleType;
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }
            _validator = validator;

            if (publishingRules == null)
            {
                throw new ArgumentNullException("publishingRules");
            }

            _publishingRules = publishingRules;
        }

        public DateTime? DisplayFrom { get; set; }
        public DateTime? DisplayTo { get; set; }
        public DateTime LastUpdated { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public bool IsDraft { get; set; }
        public bool IsValid { get { return _validator.IsArticleValid(this); } protected set { } } // setter there for ORM to save to db so queries work more effectively


        public string Body { get; set; }
        public string RenderedBody { get; set; }

        public override bool IsPublished
        {
            get { return _publishingRules.IsInStateToPublish(this); }
            protected set { }
        }
    }
}

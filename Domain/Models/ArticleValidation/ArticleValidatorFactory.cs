using System;

namespace Models.ArticleValidation
{
    public class ArticleValidatorFactory
    {
        public IArticleValidator Create(ArticleOld articleOld)
        {
            if (articleOld == null)
            {
                throw new ArgumentNullException();
            }
                
            switch (articleOld.ArticleType)
            {
                case ArticleType.Link:
                    return new LinkArticleValidator();                    
                case ArticleType.Full:
                    return new FullArticleValidator();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
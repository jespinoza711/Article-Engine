using Models.Articles;

namespace Models.ArticleValidation
{
    public interface IArticleValidator
    {
        bool IsArticleValid(ArticleOld articleOld);

        ValidationResult Validate(Article article);
    }
}
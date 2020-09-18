using Core.Application;
using Library.Domain;
using Library.Domain.AuthorAggregate.Model;

namespace Library.Application
{
    public class UpdateAuthorUseCase : UseCase<Author, LibraryUnitOfWork>
    {
        private readonly Author _author;

        public UpdateAuthorUseCase(Author author,
                                   LibraryUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _author = author;
        }

        protected override Author Action()
        {
            var authors = _unitOfWork.GetAuthorRepository();

            var author = authors.GetById(_author.Id);

            author.UpdateName(_author.AuthorName.Name);

            author.UpdatePicture(_author.AuthorPicture.ImageUrl);

            authors.AddOrUpdate(author);

            return author;
        }
    }
}
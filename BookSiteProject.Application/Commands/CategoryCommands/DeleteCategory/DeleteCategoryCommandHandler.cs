using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserContext _userContext;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, 
            IUserContext userContext,
            IBookRepository bookRepository)
        {
            _categoryRepository = categoryRepository;
            _userContext = userContext;
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            var isDeletable = user != null && user.IsInRole("Admin");
            if (!isDeletable) return Unit.Value;

            var category = await _categoryRepository.GetCategoryByName(request.CategoryName);
            if(category == null) return Unit.Value;

            var books = await _bookRepository.GetBooksByCategory(category);
            foreach(var x in books)
            {
                x.Category = null;
            }
            await _categoryRepository.Remove(category);

            return Unit.Value;

        }
    }
}

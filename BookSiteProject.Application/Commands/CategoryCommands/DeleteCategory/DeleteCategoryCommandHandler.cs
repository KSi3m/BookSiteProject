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
        private readonly IUserContext _userContext;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUserContext userContext)
        {
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            var isDeletable = user != null && user.IsInRole("Admin");
            if (!isDeletable) return Unit.Value;

            var category = await _categoryRepository.GetCategoryByName(request.CategoryName);
            if(category == null) return Unit.Value;

            await _categoryRepository.Remove(category);

            return Unit.Value;

        }
    }
}

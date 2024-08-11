using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserContext _userContext;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IUserContext userContext)
        {
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByName(request.OldName);

            if(category == null) return Unit.Value;
            var user = _userContext.GetCurrentUser();
            bool isEditable = user != null && user.IsInRole("Admin");
            if (!isEditable)
            {
                return Unit.Value;
            }

            category.Name = request.NewName;
         
            await _categoryRepository.Commit();
            return Unit.Value;

        }
    }
}

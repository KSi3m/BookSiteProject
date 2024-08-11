using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.ChangeStatusOfCategory
{
    public class ChangeStatusOfCategoryCommandHandler : IRequestHandler<ChangeStatusOfCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserContext _userContext;

        public ChangeStatusOfCategoryCommandHandler(ICategoryRepository categoryRepository,
                  IUserContext userContext)
        {
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(ChangeStatusOfCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            bool isEditable = user != null && (user.IsInRole("Moderator") || user.IsInRole("Admin"));
            if (!isEditable)
            {
                return Unit.Value;
            }

            var category =  await _categoryRepository.GetCategoryByName(request.CategoryName);

           if(category == null)
           {
                return Unit.Value;
           }



            category.Active = request.Status;
            await _categoryRepository.Commit();
            return Unit.Value;
        }
    }
}

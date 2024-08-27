using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Application.Queries.BookOfferQueries.GetAllBookOffersOfUser;
using BookSiteProject.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSiteProject.MVC.Controllers.MVC
{
    public class AccountController: Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;
  

        public AccountController(IMediator mediator, IUserContext userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }

        [Route("Account/BookOffers")]
        [Authorize]
        public async Task<IActionResult> MyOffers()
        {
            var userId = _userContext.GetCurrentUser()!.Id;
            var bookOffers = await _mediator.Send(new GetAllBookOffersOfUserQuery() { UserId = userId });
    
            return View(bookOffers);
        }
    }
}

using AutoMapper;
using BookSiteProject.Application.Commands.BookOfferCommands.CreateBookOffer;
using BookSiteProject.Application.Commands.BookOfferCommands.DeleteBookOffer;
using BookSiteProject.Application.Commands.BookOfferCommands.EditBookOffer;
using BookSiteProject.Application.Queries.BookOfferQueries.GetBookOfferById;
using BookSiteProject.Application.Queries.BookOfferQueries.GetBookOffersOfBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSiteProject.MVC.Controllers.Api
{
    [ApiController]
    [Route("api/")]
    public class BookController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost]
        [Authorize]
        [Route("books/bookoffers")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> CreateBookOffer([FromForm] CreateBookOfferCommand command)
        {
            if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Created();
        }

        [HttpGet]
        //[Authorize(Roles = "Owner,Admin,Moderator")]
        [Route("books/{encodedName}/bookoffers")]
        public async Task<IActionResult> GetBookOffers(string encodedName)
        {
            var data = await _mediator.Send(new GetBookOffersOfBookQuery() { EncodedName = encodedName });
            return Ok(data);
        }

        [HttpDelete]
        [Authorize]
        [Route("bookoffers/{offerId}")]
        public async Task<IActionResult> DeleteBookOffer(int offerId)
        {
            await _mediator.Send(new DeleteBookOfferCommand { OfferId = offerId });
            return NoContent();

        }

        [HttpGet]
        [Authorize]
        [Route("bookoffers/{offerId}")]
        public async Task<IActionResult> GetBookOffer(int offerId)
        {
            var offer = await _mediator.Send(new GetBookOfferByIdQuery { OfferId = offerId });
            var model = _mapper.Map<EditBookOfferCommand>(offer);
            return Ok(model);

        }
        [HttpPut]
        [Authorize]
        [Route("bookoffers/{offerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> EditBookOffer([FromForm] EditBookOfferCommand command, int offerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }
    }
}

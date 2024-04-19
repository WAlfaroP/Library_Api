using Library_WebApi.Dtos;
using Library_WebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            try
            {
                var books = await _mediator.Send(new GetAllBooksQuery());
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}
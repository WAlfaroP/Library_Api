﻿using Library_WebApi.Commands;
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

        [HttpPost]
        [Route("AddBook")]
        public async Task<ActionResult<NewBookDto>> AddBook([FromBody] AddBookCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public async Task<ActionResult<DeleteBookResultDto>> DeleteBook(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteBookCommand { BookId = id });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BorrowBook/{id}")]
        public async Task<ActionResult<BorrowBookResultDto>> BorrowBook(int id)
        {
            try
            {
                var result = await _mediator.Send(new BorrowBookCommand { BookId = id });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ReturnBook/{id}")]
        public async Task<ActionResult<ReturnBookResultDto>> ReturnBook(int id)
        {
            try
            {
                var result = await _mediator.Send(new ReturnBookCommand { BookId = id });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
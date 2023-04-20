using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WalletApp_Backend.Common
{
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [NonAction]
        public IActionResult Response<T>(Response<T> response) =>
            response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response.Value),
                HttpStatusCode.Created => Created("", response.Value),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(response.Message),
                HttpStatusCode.BadRequest => BadRequest(response.Message),
                HttpStatusCode.Unauthorized => Unauthorized(response.Message),
                _ => StatusCode((int)response.StatusCode,response.Message)
            };
    }
}
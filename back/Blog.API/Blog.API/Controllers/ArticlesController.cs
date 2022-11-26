using Blog.Domain.Commands.Articles;
using Blog.Domain.Queries.Articles;
using Blog.SharedKernel.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken) 
        {
            var result = await _mediator.Send(new GetArticleQuery(id), cancellationToken);

            return this.ParseToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAnArticleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return this.ParseToActionResult(result);
        }
    }
}

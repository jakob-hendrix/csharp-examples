using MediatR;
using MediatRDemo.Data;
using MediatRDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediatRDemo.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IMediator mediator;

        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<Contact> GetContact([FromRoute] Query query) => await mediator.Send(query);

        #region Nested Classes

        // input message. Expecting a response of contact
        public class Query : IRequest<Contact>
        {
            public int Id { get; set; }
        }

        // biz logic happens here
        public class ContactHandler : IRequestHandler<Query, Contact>
        {
            private readonly ContactsContext context;

            public ContactHandler(ContactsContext context) => this.context = context;

            public Task<Contact> Handle(Query request, CancellationToken cancellationToken)
            {
                return context.Contacts.Where(c => c.Id == request.Id).SingleOrDefaultAsync();
            }
        }

        #endregion
    }
}

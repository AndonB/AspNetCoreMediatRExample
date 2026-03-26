using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IRepo<AddressBookEntry> _repo;

        public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
        {
            _repo = repo; 
            _mediator = mediator;
        }

        public AddressBookEntry Entry { get; set; }

        public void OnGet(Guid id)
        {
            // Finds the entry to delete with the specified ID and retrives it (or null if not found)
            var spec = new EntryByIdSpecification(id);
            Entry = _repo.Find(spec).FirstOrDefault();
        }

        public async Task<ActionResult> OnPost(Guid id) 
        {
            _ = await _mediator.Send(new DeleteAddressRequest { Id = id });
            return RedirectToPage("Index");
        }
    }
}

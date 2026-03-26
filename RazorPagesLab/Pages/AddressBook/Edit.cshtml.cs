using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
        // Finds the entry to with the specified ID and retrives it (or null if not found)
        var spec = new EntryByIdSpecification(id);
        var entry = _repo.Find(spec).FirstOrDefault();

		// If the entry exists, populate the form with the existing data
		if (entry != null) {
			UpdateAddressRequest = new UpdateAddressRequest
			{
				Id = entry.Id,
				Line1 = entry.Line1,
				Line2 = entry.Line2,
				City = entry.City,
				State = entry.State,
				PostalCode = entry.PostalCode
            };
		}

    }

    public async Task<ActionResult> OnPost()
	{
        // If the form data is valid, send the update request to the mediator and redirect to the index page
        if (ModelState.IsValid)
        {
            _ = await _mediator.Send(UpdateAddressRequest);
            return RedirectToPage("Index");
        }

        return Page();
	}
}
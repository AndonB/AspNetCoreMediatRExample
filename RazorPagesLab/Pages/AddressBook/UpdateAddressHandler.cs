using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
    : IRequestHandler<UpdateAddressRequest>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        // Finds the entry to with the specified ID and retrives it (or null if not found)
        var spec = new EntryByIdSpecification(request.Id);
        var entry = _repo.Find(spec).FirstOrDefault();

        // If the entry exists update it with the new values and save the changes to the repo
        if (entry != null) {
            entry.Update(request.Line1, request.Line2, request.City, request.State, request.PostalCode);
            _repo.Update(entry);
        }

        return await Task.FromResult(Unit.Value);
    }
}
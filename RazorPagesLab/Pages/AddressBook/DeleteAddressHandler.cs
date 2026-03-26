using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler
    : IRequestHandler<DeleteAddressRequest>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        // Finds the entry to delete with the specified ID and retrives it (or null if not found)
        var spec = new EntryByIdSpecification(request.Id);
        var entry = _repo.Find(spec).FirstOrDefault();

        // If the entry exists remove it from the repo
        if (entry != null)
        {
            _repo.Remove(entry);
        }

        return await Task.FromResult(Unit.Value);
    }
}
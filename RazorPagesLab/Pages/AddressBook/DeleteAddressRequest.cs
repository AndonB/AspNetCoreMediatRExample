using System;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressRequest
    : IRequest
{
    // The ID of the address book entry to delete
    public Guid Id { get; set; }
}
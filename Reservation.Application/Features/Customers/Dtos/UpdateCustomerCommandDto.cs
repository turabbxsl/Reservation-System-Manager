namespace Reservation.Application.Features.Customers.Dtos
{
    public class UpdateCustomerCommandDto
    {
        public EditableField FullName { get; set; }
        public EditableField PhoneNumber { get; set; }
        public EditableField Email { get; set; }
        public EditableField Note { get; set; }
    }

    public class EditableField
    {
        public string? Value { get; set; }
        public bool IsEdit { get; set; }
    }
}

﻿namespace Reservation.Application.Features.Customers.Dtos
{
    public class RegisterCustomerDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

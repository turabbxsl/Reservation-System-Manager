﻿namespace Reservation.Application.Features.Auth.ViewModels
{
    public class LoginVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}

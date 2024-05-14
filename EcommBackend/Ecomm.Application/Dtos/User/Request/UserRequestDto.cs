﻿namespace Ecomm.Application.Dtos.User.Request
{
    public class UserRequestDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public int? State { get; set; } = 1;
    }
}

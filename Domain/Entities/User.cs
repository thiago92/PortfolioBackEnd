﻿namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required string Role { get; set; }
    }
}

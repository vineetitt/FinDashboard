﻿namespace FinDashboard.API.Models.Domain
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public virtual Portfolio Portfolio { get; set; }

    }
}

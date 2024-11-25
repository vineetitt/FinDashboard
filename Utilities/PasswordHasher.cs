using System;
using BCrypt.Net;

namespace FinDashboard.API.Utilities
{
    public class PasswordHasher
    {
            public string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }
            public bool VerifyPassword(string password, string hashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
        
    }
}

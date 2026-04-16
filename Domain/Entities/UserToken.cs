using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}

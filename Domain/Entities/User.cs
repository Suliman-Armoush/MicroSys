using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
 
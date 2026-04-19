using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Request
{
    public class LoginRequestDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}

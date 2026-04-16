using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class AuthResponseDto
    {
        public string Message { get; set; } = "Logged in successfully";
        public string Token { get; set; } = string.Empty;
        
    }
}

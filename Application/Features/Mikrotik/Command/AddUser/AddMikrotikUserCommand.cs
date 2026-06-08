using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.AddUser
{
    public class AddMikrotikUserCommand : IRequest<MikrotikUserInformationResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public string UserDetails { get; set; }
        public double? LimitGB { get; set; }

        // 👇 أضف هذه الخاصية (اختيارية)
        public string? MacAddress { get; set; }
    }
}

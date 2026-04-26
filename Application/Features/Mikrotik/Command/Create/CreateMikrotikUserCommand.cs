using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.Create
{
    public class CreateMikrotikUserCommand : IRequest<MikrotikUserInformationResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public string Server { get; set; }
        public int DepartmentId { get; set; } 
        public string UserDetails { get; set; }
        public double? LimitGB { get; set; }
    }
}

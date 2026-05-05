using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Features.Mikrotik.Command.Update
{
    public class UpdateMikrotikUserCommand : IRequest<MikrotikUserInformationResponse>
    {
        [JsonIgnore]
        public string? CurrentUsername { get; set; }

        public string? NewUsername { get; set; }
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string? Server { get; set; }
        public int? DepartmentId { get; set; }
        public string? UserDetails { get; set; }
        public double? LimitGB { get; set; }
    }
}

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
        [JsonIgnore] // عشان يختفي نهائياً من الـ Body في Swagger
        public string? CurrentUsername { get; set; }

        // هذه الحقول فقط هي التي ستظهر في الـ Body
        public string? NewUsername { get; set; }
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string? Server { get; set; }
        public int? DepartmentId { get; set; }
        public string? UserDetails { get; set; }
        public double? LimitGB { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DTOs.Response
{
    public class MikrotikUserResponse
    {
        public string Comment { get; set; } = null!;
        public string Username { get; set; } = null!;

        [JsonIgnore]
        public long BytesInRaw { get; set; }
        [JsonIgnore]
        public long BytesOutRaw { get; set; }

        public double BytesIn => Math.Round(BytesInRaw / Math.Pow(1024, 3), 2);
        public double BytesOut => Math.Round(BytesOutRaw / Math.Pow(1024, 3), 2);

            public double Total => Math.Round((BytesInRaw + BytesOutRaw) / Math.Pow(1024, 3), 2);
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using tik4net;

namespace Infrastructure.MikroTik.Client
{
    public class MikrotikClient
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ITikConnection Connect()
        {
            var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api);
            connection.Open(Host, Username, Password);
            return connection;
        }
    }
}

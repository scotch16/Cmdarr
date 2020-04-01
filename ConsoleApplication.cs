using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cmdarr
{
    class ConsoleApplication
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public ConsoleApplication(ILogger<ConsoleApplication> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void Run()
        {
            string token = _configuration.GetValue<string>("slack:bot-user-oauth-access-token");
            string message = _configuration.GetValue<string>("message-to-write");
            var writer = new SlackMessageWriter(token);
            Task.WaitAll(writer.WriteMessage(message));
        }
    }
}
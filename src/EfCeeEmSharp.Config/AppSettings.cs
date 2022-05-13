namespace EfCeeEmSharp.Config;

public class AppSettings
{
    public string? BoardsToRun { get; set; }

    public RabbitMqSettings RabbitMq { get; set; }

    public class RabbitMqSettings
    {
        public string Hostname { get; set; }
        public string Vhost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
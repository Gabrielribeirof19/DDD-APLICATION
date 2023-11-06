namespace authentication.Domain.Configuration
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();

        public static DatabaseConfiguration Database { get; set; } = new();
        public class DatabaseConfiguration
        {
            public static string ConnectionString { get; set; } = string.Empty;
        }

        public class SecretsConfiguration
        {
            public static string ApiKey { get; set; } = string.Empty;
            public static string JwtPrivateKey { get; set; } = string.Empty;
            public static string PasswordSaltKey { get; set; } = string.Empty;
        }
    }
}
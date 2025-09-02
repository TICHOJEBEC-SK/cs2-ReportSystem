using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace ReportSystem.Config;

public class DatabaseConfig
{
    [JsonPropertyName("Server")] public string Server { get; set; } = "127.0.0.1";
    [JsonPropertyName("Port")] public int Port { get; set; } = 3306;
    [JsonPropertyName("Database")] public string Database { get; set; } = "database_name";
    [JsonPropertyName("User")] public string User { get; set; } = "database_user";
    [JsonPropertyName("Password")] public string Password { get; set; } = "database_password";
    [JsonPropertyName("SslMode")] public string SslMode { get; set; } = "None";

    public string ToConnectionString()
    {
        return $"Server={Server};Port={Port};Database={Database};User ID={User};Password={Password};SslMode={SslMode};";
    }
}

public class ReportConfig : BasePluginConfig
{
    [JsonPropertyName("Language")] public string Language { get; set; } = "sk";
    [JsonPropertyName("ChatPrefix")] public string ChatPrefix { get; set; } = " {lightred}[REPORT]";
    [JsonPropertyName("ReportCommand")] public string ReportCommand { get; set; } = "css_report";
    [JsonPropertyName("Database")] public DatabaseConfig Database { get; set; } = new();
    [JsonPropertyName("ServerName")] public string ServerName { get; set; } = "MIRAGE";
    [JsonPropertyName("ServerAddress")] public string ServerAddress { get; set; } = "127.0.0.1:27015";

    [JsonPropertyName("NotifyAdminsPermission")]
    public string NotifyAdminsPermission { get; set; } = "@css/kick";

    [JsonPropertyName("Reasons")]
    public List<string> Reasons { get; set; } = new()
    {
        "Cheating",
        "Toxic",
        "Voice spam"
    };

    [JsonPropertyName("CooldownSeconds")] public int CooldownSeconds { get; set; } = 30;

    [JsonPropertyName("BlockSelfReport")] public bool BlockSelfReport { get; set; } = true;
}
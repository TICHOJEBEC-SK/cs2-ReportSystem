namespace ReportSystem.Data;

public class ReportRecord
{
    public int Id { get; set; }

    public string ServerName { get; set; } = "";

    public ulong CallerSteamId64 { get; set; }
    public string CallerNickname { get; set; } = "";

    public ulong TargetSteamId64 { get; set; }
    public string TargetNickname { get; set; } = "";

    public string Reason { get; set; } = "";

    public string Status { get; set; } = "new"; // new | claimed | resolved | invalid

    public string? ClaimedByDiscordId { get; set; }
    public string? ClaimedByDiscordTag { get; set; }
    public DateTime? ClaimedAt { get; set; }

    public string? ResolvedByDiscordId { get; set; }
    public string? ResolvedByDiscordTag { get; set; }
    public string? ResolutionNote { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public string? DiscordMessageId { get; set; }
    public string? DiscordChannelId { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
namespace ReportSystem.Data;

public class ReportRecord
{
    public int Id { get; set; }

    public string ServerName { get; set; } = "";

    public string ServerAddress { get; set; } = "";

    public ulong CallerSteamId64 { get; set; }
    public string CallerNickname { get; set; } = "";

    public ulong TargetSteamId64 { get; set; }
    public string TargetNickname { get; set; } = "";

    public string Reason { get; set; } = "";

    public string Status { get; set; } = "new";

    public string? ClaimedByDiscordId { get; set; }
    public string? ClaimedByDiscordTag { get; set; }
    public DateTime? ClaimedAt { get; set; }

    public string? ResolvedByDiscordId { get; set; }
    public string? ResolvedByDiscordTag { get; set; }
    public string? ResolutionNote { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public string? DiscordMessageId { get; set; }
    public string? DiscordChannelId { get; set; }

    public ulong? ClaimedByAdminSteamId64 { get; set; }
    public string? ClaimedByAdminNickname { get; set; }
    public ulong? ResolvedByAdminSteamId64 { get; set; }
    public string? ResolvedByAdminNickname { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
using Dapper;

namespace ReportSystem.Data;

public class ReportRepository
{
    private readonly Database _db;

    public ReportRepository(Database db)
    {
        _db = db;
    }

    public async Task<int> InsertAsync(ReportRecord rec)
    {
        await using var con = _db.Open();
        const string q = @"
INSERT INTO report_system_reports
(server_name, server_addres,
 caller_steamid64, caller_nickname, target_steamid64, target_nickname, reason, status,
 claimed_by_discord_id, claimed_by_discord_tag, claimed_at,
 resolved_by_discord_id, resolved_by_discord_tag, resolution_note, resolved_at,
 discord_message_id, discord_channel_id,
 claimed_by_admin_steamid64, claimed_by_admin_nickname,
 resolved_by_admin_steamid64, resolved_by_admin_nickname,
 created, updated)
VALUES
(@ServerName, @ServerAddress,
 @CallerSteamId64, @CallerNickname, @TargetSteamId64, @TargetNickname, @Reason, @Status,
 @ClaimedByDiscordId, @ClaimedByDiscordTag, @ClaimedAt,
 @ResolvedByDiscordId, @ResolvedByDiscordTag, @ResolutionNote, @ResolvedAt,
 @DiscordMessageId, @DiscordChannelId,
 @ClaimedByAdminSteamId64, @ClaimedByAdminNickname,
 @ResolvedByAdminSteamId64, @ResolvedByAdminNickname,
 @Created, @Updated);
SELECT LAST_INSERT_ID();";
        var id = await con.ExecuteScalarAsync<int>(q, rec);
        return id;
    }
}
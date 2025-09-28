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
    
    public async Task<bool> HasUnresolvedByCallerAndTargetAsync(ulong callerSteamId64, ulong targetSteamId64)
    {
        await using var con = _db.Open();
        const string q = @"
SELECT EXISTS(
  SELECT 1
  FROM report_system_reports
  WHERE caller_steamid64 = @CallerSteamId64
    AND target_steamid64 = @TargetSteamId64
    AND status IN ('new','claimed')
) AS has_open;";
        var has = await con.ExecuteScalarAsync<bool>(q, new
        {
            CallerSteamId64 = callerSteamId64,
            TargetSteamId64 = targetSteamId64
        });
        return has;
    }
}
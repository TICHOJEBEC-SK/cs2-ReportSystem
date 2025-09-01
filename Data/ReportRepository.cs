using Dapper;

namespace ReportSystem.Data;

public class ReportRepository
{
    private readonly Database _db;

    public ReportRepository(Database db)
    {
        _db = db;
    }

    public async Task InsertAsync(ReportRecord rec)
    {
        await using var con = _db.Open();
        const string q = @"
INSERT INTO report_system_reports
(caller_steamid64, caller_nickname, target_steamid64, target_nickname, reason, created)
VALUES
(@CallerSteamId64, @CallerNickname, @TargetSteamId64, @TargetNickname, @Reason, @Created);";
        await con.ExecuteAsync(q, rec);
    }
}
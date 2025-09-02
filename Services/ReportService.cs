using CounterStrikeSharp.API.Core;
using ReportSystem.Config;
using ReportSystem.Data;

namespace ReportSystem.Services;

public class ReportService
{
    private readonly ReportRepository _repo;
    private readonly ReportConfig _cfg;

    private readonly Dictionary<ulong, DateTime> _cooldowns = new();

    public ReportService(ReportRepository repo, ReportConfig cfg)
    {
        _repo = repo;
        _cfg = cfg;
    }

    public bool CanReport(ulong callerSteamId64)
    {
        if (_cfg.CooldownSeconds <= 0) return true;

        var now = DateTime.UtcNow; // UTC kvôli konzistencii s botom
        if (_cooldowns.TryGetValue(callerSteamId64, out var last))
        {
            var next = last.AddSeconds(_cfg.CooldownSeconds);
            if (now < next) return false;
        }

        _cooldowns[callerSteamId64] = now;
        return true;
    }

    public async Task CreateAsync(CCSPlayerController caller, CCSPlayerController target, string reason)
    {
        var now = DateTime.UtcNow;

        var rec = new ReportRecord
        {
            ServerName = _cfg.ServerName,
            CallerSteamId64 = caller.SteamID,
            CallerNickname = Chat.Name(caller),
            TargetSteamId64 = target.SteamID,
            TargetNickname = Chat.Name(target),
            Reason = reason,
            Status = "new",
            Created = now,
            Updated = now
        };

        await _repo.InsertAsync(rec);
    }
}
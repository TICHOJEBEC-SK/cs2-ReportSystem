using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using MenuManager;
using ReportSystem.Config;
using ReportSystem.Data;
using ReportSystem.Menu;
using ReportSystem.Services;

namespace ReportSystem;

public class ReportSystem : BasePlugin, IPluginConfig<ReportConfig>
{
    public override string ModuleName => "ReportSystem";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "TICHOJEBEC";
    public override string ModuleDescription => "https://github.com/TICHOJEBEC-SK/cs2-ReportSystem";

    private readonly PluginCapability<IMenuApi?> _menuCapability = new("menu:nfcore");
    private IMenuApi? _menuApi;

    private Localization _l = null!;
    private Database _db = null!;
    private ReportRepository _repo = null!;
    private ReportService _service = null!;

    public ReportConfig Config { get; set; } = new();

    public void OnConfigParsed(ReportConfig config)
    {
        if (string.IsNullOrWhiteSpace(config.Language)) config.Language = "sk";
        if (string.IsNullOrWhiteSpace(config.ChatPrefix)) config.ChatPrefix = " [REPORT]";
        if (string.IsNullOrWhiteSpace(config.ReportCommand)) config.ReportCommand = "css_report";
        if (string.IsNullOrWhiteSpace(config.ServerName)) config.ServerName = "CS2";
        if (string.IsNullOrWhiteSpace(config.ServerAddress)) config.ServerAddress = "127.0.0.1:27015";
        if (config.CooldownSeconds < 0) config.CooldownSeconds = 0;
        if (config.Reasons.Count == 0)
            config.Reasons = new() { "Cheating", "Toxic", "Voice spam", "Exploit" };

        Config = config;
    }

    public override void Load(bool hotReload)
    {
        var langDir = Path.Combine(ModuleDirectory, "lang");
        _l = new Localization(langDir, Config.Language);

        _db = new Database(Config.Database.ToConnectionString());
        _db.InitAsync().GetAwaiter().GetResult();

        _repo = new ReportRepository(_db);
        _service = new ReportService(_repo, Config);

        AddCommand(Config.ReportCommand, "Open report menu", OnReportCommand);
    }

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        _menuApi = _menuCapability.Get();
        if (_menuApi == null)
            Console.WriteLine("[ReportSystem] MenuManager Core not found (menu:nfcore)");
    }

    private string Pref(string s) => $"{Config.ChatPrefix.TrimEnd()} {s}";

    private void OnReportCommand(CCSPlayerController? caller, CommandInfo info)
    {
        if (!Chat.ValidateCaller(caller)) return;
        var player = caller!;

        if (_menuApi == null)
        {
            Chat.ToPlayer(player, Pref(_l["MenuNotReady"]));
            return;
        }

        if (!_service.CanReport(player.SteamID))
        {
            Chat.ToPlayer(player, Pref(_l["CooldownActive"]), Config.CooldownSeconds);
            return;
        }

        var players = Utilities.GetPlayers()
            .Where(p => p is { IsValid: true } && !p.IsBot && !p.IsHLTV)
            .OrderBy(p => p.PlayerName)
            .ToList();

        ReportMenu.OpenPlayers(
            _menuApi,
            player,
            players,
            _l["MenuTitleSelectPlayer"],
            target =>
            {
                if (Config.BlockSelfReport && target.SteamID == player.SteamID)
                {
                    Chat.ToPlayer(player, Pref(_l["SelfReportBlocked"]));
                    return;
                }

                ReportMenu.OpenReasons(
                    _menuApi,
                    player,
                    _l["MenuTitleSelectReason"],
                    Config.Reasons,
                    reason =>
                    {
                        _service.CreateAsync(player, target, reason).GetAwaiter().GetResult();

                        Chat.ToPlayer(player, Pref(_l["ReportSent"]), Chat.Name(target), reason);

                        if (!string.IsNullOrWhiteSpace(Config.NotifyAdminsPermission))
                        {
                            foreach (var p in Utilities.GetPlayers())
                            {
                                if (p is { IsValid: true } && !p.IsBot && !p.IsHLTV)
                                {
                                    if (AdminManager.PlayerHasPermissions(p, Config.NotifyAdminsPermission))
                                    {
                                        Chat.ToPlayer(p, Pref(_l["ReportNotifyAdmins"]),
                                            Chat.Name(player), Chat.Name(target), reason);
                                    }
                                }
                            }
                        }
                    }
                );
            },
            _ => Chat.ToPlayer(player, Pref(_l["MenuNoPlayers"]))
        );
    }
}

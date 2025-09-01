using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using MenuManager;
using ReportSystem.Services;

namespace ReportSystem.Menu;

public static class ReportMenu
{
    public static void OpenPlayers(
        IMenuApi api,
        CCSPlayerController caller,
        IEnumerable<CCSPlayerController> targets,
        string title,
        Action<CCSPlayerController> onSelect,
        Action<string>? onEmpty = null,
        Localization? loc = null)
    {
        var list = targets.Where(p => p is { IsValid: true } && !p.IsBot && !p.IsHLTV)
            .OrderBy(p => p.PlayerName)
            .ToList();

        if (list.Count == 0)
        {
            if (loc != null)
                Chat.ToPlayer(caller, $" {loc["MenuNoPlayers"]}");
            else
                onEmpty?.Invoke("MenuNoPlayers");
            return;
        }

        var menu = api.GetMenu(title);
        
        menu.PostSelectAction = PostSelectAction.Reset;

        foreach (var p in list)
        {
            var target = p;
            menu.AddMenuOption(Chat.Name(target), (_, __) =>
            {
                if (target is { IsValid: true } && !target.IsBot && !target.IsHLTV)
                    onSelect(target);
                else
                    Chat.ToPlayer(caller, loc?["MenuPlayerInvalid"] ?? " {lightred}Hráč nie je online.");
            });
        }

        menu.Open(caller);
    }

    public static void OpenReasons(
        IMenuApi api,
        CCSPlayerController caller,
        string title,
        IReadOnlyList<string> reasons,
        Action<string> onSelect)
    {
        var menu = api.GetMenu(title);
        
        menu.PostSelectAction = PostSelectAction.Close;
        menu.ExitButton = true;

        foreach (var r in reasons)
        {
            var reason = r;
            menu.AddMenuOption(reason, (_, __) =>
            {
                onSelect(reason);
                
                CounterStrikeSharp.API.Modules.Menu.MenuManager.CloseActiveMenu(caller);
            });
        }

        menu.Open(caller);
    }
}

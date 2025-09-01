namespace ReportSystem.Data;

public class ReportRecord
{
    public int Id { get; set; }
    public ulong CallerSteamId64 { get; set; }
    public string CallerNickname { get; set; } = "";
    public ulong TargetSteamId64 { get; set; }
    public string TargetNickname { get; set; } = "";
    public string Reason { get; set; } = "";
    public DateTime Created { get; set; }
}
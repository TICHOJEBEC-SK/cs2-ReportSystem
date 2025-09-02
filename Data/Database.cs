using Dapper;
using MySqlConnector;

namespace ReportSystem.Data;

public class Database
{
    private readonly string _connStr;

    public Database(string connStr)
    {
        _connStr = connStr;
    }

    public MySqlConnection Open()
    {
        return new MySqlConnection(_connStr);
    }

    public async Task InitAsync()
    {
        await using var con = Open();
        await con.OpenAsync();
        
        var sql = @"
CREATE TABLE IF NOT EXISTS report_system_reports (
  id                         INT NOT NULL AUTO_INCREMENT,
  server_name                VARCHAR(64)     NOT NULL,
  caller_steamid64           BIGINT UNSIGNED NOT NULL,
  caller_nickname            VARCHAR(64)     NOT NULL,
  target_steamid64           BIGINT UNSIGNED NOT NULL,
  target_nickname            VARCHAR(64)     NOT NULL,
  reason                     VARCHAR(128)    NOT NULL,
  status                     VARCHAR(16)     NOT NULL DEFAULT 'new',
  claimed_by_discord_id      VARCHAR(32)     NULL,
  claimed_by_discord_tag     VARCHAR(64)     NULL,
  claimed_at                 DATETIME        NULL,
  resolved_by_discord_id     VARCHAR(32)     NULL,
  resolved_by_discord_tag    VARCHAR(64)     NULL,
  resolution_note            VARCHAR(255)    NULL,
  resolved_at                DATETIME        NULL,
  discord_message_id         VARCHAR(32)     NULL,
  discord_channel_id         VARCHAR(32)     NULL,
  created                    DATETIME        NOT NULL,
  updated                    DATETIME        NOT NULL,
  PRIMARY KEY (id),
  KEY idx_status (status),
  KEY idx_created (created),
  KEY idx_status_created (status, created),
  KEY idx_target_steamid64 (target_steamid64),
  KEY idx_caller_steamid64 (caller_steamid64)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
        await con.ExecuteAsync(sql);
    }
}
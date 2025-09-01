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
  id                 INT NOT NULL AUTO_INCREMENT,
  caller_steamid64   BIGINT UNSIGNED NOT NULL,
  caller_nickname    VARCHAR(64)     NOT NULL,
  target_steamid64   BIGINT UNSIGNED NOT NULL,
  target_nickname    VARCHAR(64)     NOT NULL,
  reason             VARCHAR(64)     NOT NULL,
  created            DATETIME        NOT NULL,
  PRIMARY KEY (id),
  KEY idx_caller_steamid64 (caller_steamid64),
  KEY idx_target_steamid64 (target_steamid64),
  KEY idx_created (created)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
        await con.ExecuteAsync(sql);
    }
}
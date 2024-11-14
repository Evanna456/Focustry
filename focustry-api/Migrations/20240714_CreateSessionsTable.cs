using FluentMigrator;

namespace focustry_api.Migrations
{
    [Migration(2)]
    public class _20240714_CreateSessionsTable : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("sessions").Exists())
            {
                var sql = @"CREATE TABLE sessions (uid VARCHAR(255), token VARCHAR(255), created_at TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP) ENGINE = MEMORY;";
                Execute.Sql(sql);
            }
        }
        public override void Down()
        {
            Delete.Table("sessions");
        }
    }
}

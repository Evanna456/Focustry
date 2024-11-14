using FluentMigrator;

namespace focustry_api.Migrations
{
    [Migration(1)]
    public class _20240714_CreateUsersTable : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("users").Exists())
            {
                Create.Table("users")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("role_id").AsInt32()
                .WithColumn("uid").AsString()
                .WithColumn("firstname").AsString()
                .WithColumn("lastname").AsString()
                .WithColumn("email").AsString()
                .WithColumn("username").AsString()
                .WithColumn("password").AsString()
                .WithColumn("remember_token").AsString()
                .WithColumn("remtoken_created_at").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("created_at").AsDateTime().WithDefault(SystemMethods.CurrentDateTime);
            }
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}

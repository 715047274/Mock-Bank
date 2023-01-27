using System;
using FluentMigrator;
namespace MockBank.Data.Migrations
{
    [Migration(2022_05_30_003)]    
    public class CreateAddressTableMigration : Migration
    {
        private string TName = "berkeley_address";
        
        public override void Up()
        {
            Create.Table(TName)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("address1").AsString().Nullable()
                .WithColumn("address2").AsString().Nullable()
                .WithColumn("city").AsString().Nullable()
                .WithColumn("state").AsString().Nullable()
                .WithColumn("postal_code").AsString().Nullable()
                .WithColumn("country").AsString().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(TName);
        }
    }
}
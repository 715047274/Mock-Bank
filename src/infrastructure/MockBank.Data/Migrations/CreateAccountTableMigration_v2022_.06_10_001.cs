using System;
using FluentMigrator;

namespace MockBank.Data.Migrations
{
    [Migration(2021_06_10_001)]
    public class CreateAccountTableMigration : Migration
    {
        
        private string TName = "berkeley_account";
        public override void Up()
        {
            Create.Table(TName)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("account_number").AsString().Nullable()
                .WithColumn("cardholder_id").AsInt32().NotNullable()
                .WithColumn("program_id").AsInt32().NotNullable()
                .WithColumn("bank_id").AsInt32().NotNullable()
                .WithColumn("status_code").AsString().NotNullable().WithDefaultValue("not_active")
                .WithColumn("balance").AsDecimal(5,2).Nullable().WithDefaultValue(0.00)
                .WithColumn("processor_reference_id").AsString().Nullable()
                .WithColumn("start_date").AsDate().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("end_date").AsDate().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            
        }

        public override void Down()
        {
            Delete.Table(TName);
        }
    }
}
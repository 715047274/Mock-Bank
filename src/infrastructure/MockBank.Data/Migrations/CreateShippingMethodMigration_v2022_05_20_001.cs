using System;
using FluentMigrator;

namespace MockBank.Data.Migrations
{
    [Migration(2021_01_01_001)]
    public class CreateShippingMethodTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("shipping_method")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("method_name").AsString(100).NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            
            // Seed default data 
            
            SeedShippingMethod(3, "USPS Mail", SystemMethods.CurrentDateTime);
            SeedShippingMethod(4, "USPS International", SystemMethods.CurrentDateTime);
            SeedShippingMethod(8, "UPS Worldwide Saver", SystemMethods.CurrentDateTime);
            SeedShippingMethod(9, "UPS Standard", SystemMethods.CurrentDateTime);
        }

        private void SeedShippingMethod(int id, string methodName, SystemMethods updatedAt)
        {
            Insert.IntoTable("shipping_method").Row(new
            {
                id = id, 
                method_name= methodName,
                updated_at = updatedAt
            });
        }

        public override void Down()
        {
            Delete.Table("shipping_method");
        }
    }
}
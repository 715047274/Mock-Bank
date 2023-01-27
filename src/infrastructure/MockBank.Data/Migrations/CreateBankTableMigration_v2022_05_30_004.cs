using System;
using System.Collections.Generic;
using FluentMigrator;
namespace MockBank.Data.Migrations
{
    [Migration(2022_05_30_004)]    
    public class CreateBankTableMigration : Migration
    {
        private string TName = "berkeley_bank";
        public override void Up()
        {
            Create.Table(TName)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().Nullable()
                .WithColumn("transit_number").AsString().Nullable()
                .WithColumn("institution_number").AsString().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            SeedBankData();
        }

        public override void Down()
        {
            Delete.Table(TName);
        }
        private void SeedBankData()
        {
            foreach (var bank in SeedBanks)
            {
                Insert.IntoTable(TName).Row(bank);
            }
             
        }
        private class Bank
        {
            public int id { get; set; }
            public string name { get; set; }
            public string transit_number { get; set; } // branch number
            public string institution_number { get; set; }

            public SystemMethods updated_at { get; set; } = SystemMethods.CurrentDateTime;
        }



        private List<Bank> SeedBanks = new List<Bank>()
        {
            new Bank
            {
                id=1,
                name = "Bank Of Montreal (BMO)", 
                institution_number= "001",
                transit_number = "01372"
            },
            new Bank
            {
                id=2,
                name = "The Bank of Nova Scotia (Scotiabank)", 
                institution_number= "002",
                transit_number = "0123"
            },
            new Bank
            {
                id=3,
                name = "Royal Bank of Canada (RBC)", 
                institution_number= "003",
                transit_number = "0123"
            },
            new Bank
            {
                id=4,
                name = "Toronto-Dominion Canada Trust (TD)", 
                institution_number= "004",
                transit_number = "0123"
            },
            new Bank
            {
                id=5,
                name = "National Bank of Canada (NBC)", 
                institution_number= "006",
                transit_number = "0123"
            },
            new Bank
            {
                id=6,
                name = "Canadian Imperial Bank of Commerce (CIBC)", 
                institution_number= "010",
                transit_number = "0123"
            },
            new Bank
            {
                id=7,
                name = "HSBC Bank Canada (HSBC)", 
                institution_number= "016",
                transit_number = "0123"
            },
        };
    }
}
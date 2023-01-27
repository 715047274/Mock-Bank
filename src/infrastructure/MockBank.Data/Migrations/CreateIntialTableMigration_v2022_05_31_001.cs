using System;
using System.Collections.Generic;
using FluentMigrator;

namespace MockBank.Data.Migrations
{
    [Migration(2022_05_31_001)]
    public class CreateIntialTableMigration :Migration
    {
        private string cardHolderTable = "berkeley_cardholder";
        private string programTable = "berkeley_program";
        private string merchantTable = "berkeley_merchant";
        private string cardTable = "berkeley_card";
        private string authorization = "berkeley_authorization";
        private string transaction = "berkeley_transaction";
        private string processor = "berkeley_processor_event";
        public override void Up()
        {
            
            #region Create Table  cardholder program merchant card authorization transaction processor_event
            // card_holder table
            Create.Table(cardHolderTable)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("first_name").AsString().Nullable()
                .WithColumn("middle_name").AsString().Nullable()
                .WithColumn("last_name").AsString().Nullable()
                .WithColumn("date_of_birth").AsString().Nullable()
                .WithColumn("emboss_line").AsString().Nullable()
                .WithColumn("phone").AsString().Nullable()
                .WithColumn("email").AsString().Nullable()
                .WithColumn("sin").AsString().Nullable()
                .WithColumn("shipping_method_id").AsInt32()
                .WithColumn("address_id").AsInt32()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            // program table --> 1 : N account
            Create.Table(programTable)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().Nullable()
                .WithColumn("master_funding_account_number").AsString().Nullable()
                .WithColumn("program_type").AsString().Nullable() // virtual_reloadable , 
                .WithColumn("currency").AsString().Nullable()
                .WithColumn("status").AsString().Nullable().WithDefaultValue("ACTIVE")
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            // card table 1:1 shipping_method refs order_shipping_method_id, 1:1 account 
            Create.Table(cardTable)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("account_id").AsInt32().Nullable()
                .WithColumn("card_number").AsString().Nullable()
                .WithColumn("expiry_year").AsString().Nullable()
                .WithColumn("expiry_month").AsString().Nullable()
                .WithColumn("cvv").AsString().Nullable()
                .WithColumn("activation_date").AsDate().Nullable()
                .WithColumn("order_shipping_method_id").AsInt32().Nullable()
                .WithColumn("order_status").AsString().Nullable()
                .WithColumn("order_tracking_number").AsString().Nullable()
                .WithColumn("registration_date").AsDate().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("shipping_date").AsDate().Nullable()
                .WithColumn("status_code").AsString().NotNullable().WithDefaultValue("not_active")
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            // merchant table
            Create.Table(merchantTable)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("mcc").AsString().Nullable()   // required  Merchant Category Code
                .WithColumn("mcc_description").AsString().Nullable() // required Merchant Category Code Description
                .WithColumn("name").AsString().Nullable()
                .WithColumn("city").AsString().Nullable()
                .WithColumn("country").AsString().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            // authorization 
            Create.Table(authorization)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("account_id").AsInt32().Nullable()
                .WithColumn("external_tag").AsString().Nullable()
                .WithColumn("timestamp").AsDateTime()
                .WithColumn("transaction_amount").AsString()
                .WithColumn("transaction_currency").AsString()
                .WithColumn("type_id").AsInt32()
                .WithColumn("merchant_id").AsInt32()
                .WithColumn("processor_reference_id").AsString()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            //transaction
            Create.Table(transaction)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("account_id").AsInt32().Nullable()
                .WithColumn("external_tag").AsString().Nullable()
                .WithColumn("idempotency_key").AsString().Nullable()
                .WithColumn("timestamp").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("transaction_amount").AsString()
                .WithColumn("transaction_currency").AsString()
                .WithColumn("type_id").AsInt32()
                .WithColumn("merchant_id").AsInt32()
                .WithColumn("processor_reference_id").AsString()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            // processor
            Create.Table(processor)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("message").AsString().Nullable()
                .WithColumn("status").AsString().Nullable()
                .WithColumn("action_name").AsString().Nullable()
                .WithColumn("delay_millisecond").AsInt32().Nullable()
                .WithColumn("reference_id").AsString().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
            #endregion
            #region Seed Data
            Insert.IntoTable(programTable).Row(
                new
                {
                id="124",
                name="Ceridian Dayforce Physical Rel",
                master_funding_account_number="1930000010",
                program_type = "physical_reloadable",
                currency = "124",
                status="inactive",
                updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(programTable).Row(
                new
                {
                    id="211",
                    name="Ceridian Shared Bin Test",
                    master_funding_account_number="1930001414",
                    program_type = "physical_reloadable",
                    currency = "124",
                    status="active",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(programTable).Row(
                new
                {
                    id="199",
                    name="Dayforce GPR BIN",
                    master_funding_account_number="1990000033",
                    program_type = "physical_reloadable",
                    currency = "124",
                    status="active",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(programTable).Row(
                new
                {
                    id="200",
                    name="Dayforce GPR BIN Virtual",
                    master_funding_account_number="2000000017",
                    program_type = "physical_reloadable",
                    currency = "124",
                    status="active",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(programTable).Row(
                new
                {
                    id="67",
                    name="Dayforce Reloadable KYC",
                    master_funding_account_number="129201017800",
                    program_type = "physical_reloadable",
                    currency = "840",
                    status="inactive",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            /*
                MCCs 0001–1499: Agricultural Services
                MCCs 1500–2999: Contracted Services
                MCCs 4000–4799: Transportation Services
                MCCs 4800–4999: Utility Services
                MCCs 5000–5599: Retail Outlet Services
                MCCs 5600–5699: Clothing Stores
                MCCs 5700–7299: Miscellaneous Stores
                MCCs 7300–7999: Business Services
                MCCs 8000–8999: Professional Services and Membership Organizations
                MCCs 9000–9999: Government Services
             */
            Insert.IntoTable(merchantTable).Row(
                new
                {
                    id=1,
                    mcc=5999,
                    mcc_description= "Miscellaneous and Special",
                    name="Value Load",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(merchantTable).Row(
                new
                {
                    id=2,
                    mcc=5999,
                    mcc_description= "Miscellaneous and Special",
                    name="Direct Deposit",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(merchantTable).Row(
                new
                {
                    id=3,
                    mcc=5999,
                    mcc_description= "Miscellaneous and Special",
                    name="Biller Account Load",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(merchantTable).Row(
                new
                {
                    id=4,
                    mcc=5999,
                    mcc_description= "Miscellaneous and Special",
                    name="Visa Direct Load (Berkeley)",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            Insert.IntoTable(merchantTable).Row(
                new
                {
                    id=5,
                    mcc=5999,
                    mcc_description= "Miscellaneous and Special",
                    name="Visa Direct P2P Load (Berkeley)",
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            );
            #endregion




        }

        public override void Down()
        {
            Delete.Table(cardHolderTable);
            Delete.Table(programTable);
            Delete.Table(merchantTable);
            Delete.Table(cardTable);
            Delete.Table(transaction);
            Delete.Table(authorization);
            Delete.Table(processor);
           
        }
        
    }
}
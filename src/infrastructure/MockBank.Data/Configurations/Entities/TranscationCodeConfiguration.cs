using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data.Configurations.Entities
{
    public class TranscationCodeConfiguration : IEntityTypeConfiguration<TransactionCode>
    {
        public void Configure(EntityTypeBuilder<TransactionCode> builder)
        {
            // builder.HasData(
            //     // Transaction Codes defined for Canadian Program [https://berkeleypayments.atlassian.net/wiki/spaces/CS/pages/978026497/Authorization+Transaction+Codes]
            //     new TransactionCode
            //     {   id = 1,
            //         code_type = "01",
            //         description = "Purchase",
            //         transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 2,
            //         code_type = "03",
            //         description = "Cash Advance",
            //         transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 3,
            //        code_type = "04",
            //        description = "Withdrawal",
            //        transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 4,
            //         code_type = "05",
            //         description = "Unique Transaction",
            //         transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 5,
            //         code_type = "09",
            //         description = "Credit Voucher",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 6,
            //         code_type = "20",
            //         description = "Refund",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 7,
            //         code_type = "89",
            //         description = "Internal Transfer",
            //         transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 8,
            //         code_type = "90",
            //         description = "Internal Transfer",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 9,
            //         code_type = "B4",
            //         description = "Authorisation Fees",
            //         transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 10,
            //         code_type = "CC",
            //         description = "Card To Card Credit",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 11,
            //         code_type = "CT",
            //         description = "Card To Card Debit",
            //         transaction_sign = "Debit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 12,
            //         code_type = "DP",
            //         description = "Prepaid Deposit",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 13,
            //         code_type = "IT",
            //         description = "Initial Topup",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {
            //         id = 14,
            //         code_type = "PP1",
            //         description = "Value Load",
            //         transaction_sign = "Credit"
            //     },
            //     new TransactionCode
            //     {   
            //         id = 15,
            //         code_type = "MS",
            //         description = "Money Send Payment",
            //         transaction_sign = "Credit"
            //     }
            // );
        }
    }
}
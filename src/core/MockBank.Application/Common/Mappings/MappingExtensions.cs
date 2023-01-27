 using System.Collections.Generic;
 using System.Security.Cryptography.Xml;
 using AutoMapper;
 using MockBank.Application.Dto.Berkeley;
 using MockBank.Application.Features.Berkeleys.CardIssuing.CardHolders.Commands.CreateCardholder;
 using MockBank.Domain.Entities.Berkeleys;
 using Account = MockBank.Domain.Entities.Berkeleys.Account;
 using Address = MockBank.Domain.Entities.Berkeleys.Address;

 namespace MockBank.Application.Configurations.Common.Mappings
{
    public class MappingExtensions: Profile
    {

        public MappingExtensions()
        {
            CreateMap<CreateCardholderCommand, Address>().ReverseMap();
            CreateMap<Account, BKCreateAccountData>().ReverseMap();
            CreateMap<CardHolder, AccountHolderData>().ReverseMap();
            CreateMap<Account, BKCardholderAccount>().ReverseMap();
            CreateMap<BankInfo, BKBankDetails>().ReverseMap();
            CreateMap<Card, BKCard>().ReverseMap();
            CreateMap<Account, BKAccountBalance>().ReverseMap();
            CreateMap<Transaction, BKLoadFundsData>().ReverseMap();
            CreateMap<Transaction, BKTransactionHistoryData>().ReverseMap();
            CreateMap<Merchant,BKMerchant>().ReverseMap();
            CreateMap<Account, BKAccountBalance>().ReverseMap();
            CreateMap<Transaction, BKListValueLoadTransaction>().ReverseMap();
            CreateMap<Account, BKAccountData>().ReverseMap();
            CreateMap<Transaction, BKTransaction>().ReverseMap();
        }
    }
}
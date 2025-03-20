using Transactions.Domain.DTOs;
using Transactions.Domain.Enums;
using AutoMapper;
using Transactions.Domain.Models;

namespace Transactions.Mappings
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<TransactionDto, TransactionModel>()
                .ConstructUsing(dto => new TransactionModel(
                    Enum.Parse<TransactionType>(dto.TransactionType, true),
                    dto.Amount,
                    dto.BankAccountId,
                    dto.CounterpartyBankCode,
                    dto.CounterpartyBankName,
                    dto.CounterpartyBranch,
                    dto.CounterpartyAccountNumber,
                    dto.CounterpartyHolderName,
                    dto.CounterpartyHolderDocument,
                    Enum.Parse<CounterpartyAccountType>(dto.CounterpartyAccountType, true),
                    Enum.Parse<CounterpartyHolderType>(dto.CounterpartyHolderType, true)
                ))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<TransactionModel, TransactionDto>();
        }
    }
}

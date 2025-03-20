using AutoMapper;
using Transactions.Domain.DTOs;
using Transactions.Domain.Enums;
using Transactions.Domain.Models;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        // De DTO para Model
        CreateMap<TransactionDto, TransactionModel>()
            .ConstructUsing(dto => new TransactionModel(
                Enum.Parse<TransactionType>(dto.TransactionType.ToString(), true),
                dto.Amount,
                dto.BankAccountId,
                dto.CounterpartyBankCode,
                dto.CounterpartyBankName,
                dto.CounterpartyBranch,
                dto.CounterpartyAccountNumber,
                dto.CounterpartyHolderName,
                dto.CounterpartyHolderDocument,
                Enum.Parse<CounterpartyAccountType>(dto.CounterpartyAccountType.ToString(), true),
                Enum.Parse<CounterpartyHolderType>(dto.CounterpartyHolderType.ToString(), true)
            ))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));

        // De Model para DTO
        CreateMap<TransactionModel, TransactionDto>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ForMember(dest => dest.CounterpartyAccountType, opt => opt.MapFrom(src => src.CounterpartyAccountType.ToString()))
            .ForMember(dest => dest.CounterpartyHolderType, opt => opt.MapFrom(src => src.CounterpartyHolderType.ToString()));
    }
}

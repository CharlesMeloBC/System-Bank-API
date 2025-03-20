using AccountBank.Domain.DTOs;
using AccountBank.Domain.Enums;
using AccountBank.Domain.Models;
using AutoMapper;

namespace AccountBank.Mappings
{
    public class AccountBankMappingProfile : Profile
    {
        public AccountBankMappingProfile()
        {
            CreateMap<AccountBankDto, AccountBankModel>()
                .ConstructUsing(dto => new AccountBankModel(
                    dto.HolderName,
                    Enum.Parse<AccountType>(dto.TypeAccount, true),
                    dto.HolderEmail,
                    dto.HolderDocuments,
                    Enum.Parse<HolderType>(dto.HolderType, true)
                ))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AccountStatus.ACTIVE));

            CreateMap<AccountBankModel, AccountBankDto>()
                .ForMember(dest => dest.TypeAccount, opt => opt.MapFrom(src => src.TypeAccount.ToString()))
                .ForMember(dest => dest.HolderType, opt => opt.MapFrom(src => src.HolderType.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}

using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionsDTO>().IncludeMembers(x => x.Item);
            // .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Item.Make))
            // .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Item.Model))
            // .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item.Year))
            // .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Item.Color))
            // .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Item.Mileage))
            // .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Item.ImageUrl))
            // .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Item, AuctionsDTO>();
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src));
            // {
            //     Make = src.Make,
            //     Model = src.Model,
            //     Year = src.Year,
            //     Color = src.Color,
            //     Mileage = src.Mileage,
            //     ImageUrl = src.ImageUrl
            // }));
        CreateMap<CreateAuctionDto, Item>();
        CreateMap<UpdateAuctionDto, Item>();
    }
}
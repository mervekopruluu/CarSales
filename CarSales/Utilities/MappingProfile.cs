using AutoMapper;
using Entities.Models;
using Public.Dtos.Car;
using Public.Dtos.Customer;

namespace CarSales.Utilities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, CarDto>()
            .ForMember(src => src.CustomerFullName,
                opt => opt.MapFrom(target => target.Customer.FirstName + " " + target.Customer.LastName))
            .ForMember(src => src.SalerFullName,
                opt => opt.MapFrom(target => target.Saler.FirstName + " " + target.Saler.LastName));
        CreateMap<CarDtoForCreate, Car>()
            .ForMember(m => m.Status, opt => opt.MapFrom(target => InternalStatusEnum.ReadyForSale))
            .ForMember(m => m.RemainingPayment, opt => opt.MapFrom(target => target.Price));
        CreateMap<CarDtoForUpdate, Car>();

        CreateMap<User, CustomerDto>()
            .ForMember(src => src.Name,
                opt => opt.MapFrom(target => target.FirstName))
            .ForMember(src => src.Surname,
                opt => opt.MapFrom(target => target.LastName))
            .ForMember(src => src.FullName,
                opt => opt.MapFrom(target => target.FirstName + " " + target.LastName));
    }
}
using AutoMapper;
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Data;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		// Example of map
		// CreateMap<sourceModel, destinationModel>();
		CreateMap<Customer, CustomerReadDto>();
		CreateMap<CustomerUpdateDto, Customer>()
		.ForMember(m => m.Id, opt => opt.Ignore());
		CreateMap<CustomerCreateDto, Customer>();

		// Example with condition
		// CreateMap<EditFlightModel, Flight>()
		//   .ForAllMembers(opts => opts.Condition((src, //dest, srcMember) => srcMember != null));

		//   .ForMember(dest => dest.PaymentMethodNonce, opt => opt.MapFrom(src => src.Nonce));
	}
}
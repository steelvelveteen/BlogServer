using AutoMapper;
using BlogServer.Models;

namespace BlogAPI.Data;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		// Example of map
		// CreateMap<sourceModel, destinationModel>();
		CreateMap<Customer, Customer>()
		.ForMember(m => m.Id, opt => opt.Ignore());

		// Example with condition
		// CreateMap<EditFlightModel, Flight>()
		//   .ForAllMembers(opts => opts.Condition((src, //dest, srcMember) => srcMember != null));

		// CreateMap<PaymentSourceRequest, Braintree.PaymentMethodRequest>()
		// ForMember(d => d.Id, opt => opt.Ignore())
		//   .ForMember(dest => dest.PaymentMethodNonce, opt => opt.MapFrom(src => src.Nonce));
	}
}
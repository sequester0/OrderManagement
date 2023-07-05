using AutoMapper;
using OrderManagement.Common.DTO.Order;

namespace OrderManagement.Common.Profiles.Order
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderPartialUpdateDto, Data.Models.Order>();
            CreateMap<Data.Models.Order, OrderPartialUpdateDto>();
        }
    }
}

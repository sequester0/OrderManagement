using OrderManagement.Common.DTO.Basket;
using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;

namespace OrderManagement.Common.Contracts
{
    public interface IBasketBusinessEngine
    {
        Task<Result<List<BrandDto>>> Get(int userid);
        Task<Result<Basket>> GetBasketById(int basketid);
        Task<Result<Basket>> Add(BasketCreateDto basketCreateDto, int userid);
        //Result<BasketUpdateDto> Update(int userId, BasketUpdateDto basketUpdateDto);
        Task<Result<Basket>> Remove(int productid, int userid);
    }
}

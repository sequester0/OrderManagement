using OrderManagement.Common.DTO.Basket;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Common.Contracts
{
    public interface IBasketBusinessEngine
    {
        List<Basket> Get();
        Result<Basket> GetBasketById(int basketid);
        Result<Basket> Add(BasketCreateDto basketCreateDto);
        //Result<BasketUpdateDto> Update(int userId, BasketUpdateDto basketUpdateDto);
        Result<Basket> Remove(int productid);
    }
}

using OrderManagement.Common.DTO;
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
        Basket GetBasketById(int basketid);
        List<Basket> Get();
        void Add(BasketCreateDto basketCreateDto);
    }
}

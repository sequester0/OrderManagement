using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO;
using OrderManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BusinessEngine
{
    public class BasketBusinessEngine : IBasketBusinessEngine
    {
        EGITIM_TESTContext _context;

        public BasketBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public List<Basket> Get()
        {
            return _context.Baskets.ToList();
        }

        public void Add(BasketCreateDto basketCreateDto)
        {
            var finalBasketItem = new Basket() { 
                ProductId = basketCreateDto.ProductId
            };
            _context.Baskets.Add(finalBasketItem);
            _context.SaveChanges();
        }

        public Basket GetBasketById(int basketid)
        {
            var basket = _context.Baskets.FirstOrDefault(b => b.BasketId == basketid);
            return basket;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Basket;
using OrderManagement.Common.Result;
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

        public Result<Basket> Add(BasketCreateDto basketCreateDto)
        {
            try
            {
                var finalBasketItem = new Basket()
                {
                    ProductId = basketCreateDto.ProductId,
                    UserId = 1
                };

                _context.Baskets.Add(finalBasketItem);
                _context.SaveChanges();
                return new Result<Basket> { Data = finalBasketItem, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Basket> { Message = ex.Message, Status = false };
            }
        }

        //public Result<BasketUpdateDto> Update(int userid, BasketUpdateDto basketUpdateDto)
        //{
        //    try
        //    {
        //        var basketItemFromStore = _context.Baskets.FirstOrDefault(c => c.UserId == userid);

        //        //basketItemFromStore.ProductId = string.Join(',', basketItemFromStore.ProductId, basketUpdateDto.ProductId);

        //        _context.Baskets.Update(basketItemFromStore);
        //        _context.SaveChanges();
        //        return new Result<BasketUpdateDto> { Message = "Operation successful", Status = true, Data = basketUpdateDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result<BasketUpdateDto> { Message = ex.Message, Status = false };
        //    }
        //}

        public Result<Basket> GetBasketById(int basketid)
        {
            try
            {
                var basket = _context.Baskets.First(b => b.BasketId == basketid);
                return new Result<Basket> { Data = basket, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Basket> { Message = ex.Message, Status = false };
            }
        }

        public Result<Basket> Remove(int productid)
        {
            try
            {
                var basketItemToRemove = _context.Baskets.First(b => b.ProductId == productid);

                _context.Baskets.Remove(basketItemToRemove);
                _context.SaveChanges();
                return new Result<Basket> { Data = basketItemToRemove, Message = "Operation successful", Status = true };
            }
            catch (Exception ex)
            {
                return new Result<Basket> { Message = ex.Message, Status = false };
            }
        }
    }
}

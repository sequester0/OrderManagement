using Microsoft.EntityFrameworkCore;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Basket;
using OrderManagement.Common.DTO.Brand;
using OrderManagement.Common.DTO.Product;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using Serilog;

namespace OrderManagement.BusinessEngine
{
    public class BasketBusinessEngine : IBasketBusinessEngine
    {
        EGITIM_TESTContext _context;

        public BasketBusinessEngine(EGITIM_TESTContext context)
        {
            _context = context;
        }

        public async Task<Result<List<BrandDto>>> Get(int userid)
        {
            Log.Information($"The basket of the user id {userid} is getting.");
            // revise
            var query = (from basket in _context.Baskets
                         join product in _context.Products on basket.ProductId equals product.ProductId
                         where basket.UserId == userid
                         select new
                         {
                             UserId = basket.UserId,
                             BasketId = basket.BasketId,
                             //ProductId = product.ProductId,
                             //ProductName = product.ProductName,
                             BrandId = product.BrandId,
                             Products = product
                         });

            var brands = await (from brandsq in _context.Brands
                          join prodbask in query on brandsq.BrandId equals prodbask.BrandId
                          select new
                          {
                              BrandId = brandsq.BrandId,
                              BrandName = brandsq.BrandName,
                              //ProductId = prodbask.ProductId,
                              //ProductName = prodbask.ProductName
                              Products = prodbask.Products
                          }).ToListAsync();

            var baskets = brands.GroupBy(x => new { x.BrandId, x.BrandName });

            var userBasket = GetBrandList(baskets);

            return new Result<List<BrandDto>> { Data = userBasket, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Basket>> Add(BasketCreateDto basketCreateDto, int userid)
        {
            Log.Information($"Adding product to basket.");
            var finalBasketItem = new Basket()
            {
                ProductId = basketCreateDto.ProductId,
                UserId = userid
            };
            
            await _context.Baskets.AddAsync(finalBasketItem);
            await _context.SaveChangesAsync();
            return new Result<Basket> { Data = finalBasketItem, Message = "Operation successful", Status = true };
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

        public async Task<Result<Basket>> GetBasketById(int basketid)
        {
            Log.Information($"The basket with id number {basketid} is getting.");
            var basket = await _context.Baskets.FirstAsync(b => b.BasketId == basketid);
            return new Result<Basket> { Data = basket, Message = "Operation successful", Status = true };
        }

        public async Task<Result<Basket>> Remove(int productid, int userid)
        {
            Log.Information($"The product with id number {productid} is removing from user basket with user id {userid}.");

            var basketItemToRemove = _context.Baskets.First(b => b.ProductId == productid && b.UserId == userid);

            _context.Baskets.Remove(basketItemToRemove);
            await _context.SaveChangesAsync();
            return new Result<Basket> { Data = basketItemToRemove, Message = "Operation successful", Status = true };
        }

        private List<BrandDto> GetBrandList(IEnumerable<IGrouping<dynamic, dynamic>> values)
        {
            List<BrandDto> brandsList = new List<BrandDto>();
            foreach (var x in values)
            {
                var brand = new BrandDto
                {
                    BrandId = x.Key.BrandId,
                    BrandName = x.Key.BrandName,
                    Products = new List<ProductGetDto>()
                };

                foreach (var item in x)
                {
                    if (item.Products != null)
                    {
                        ProductGetDto product = new ProductGetDto
                        {
                            ProductId = item.Products.ProductId,
                            ProductName = item.Products.ProductName
                        };

                        brand.Products.Add(product);
                    }
                }

                brandsList.Add(brand);
            }
            return brandsList;
        }

    }
}

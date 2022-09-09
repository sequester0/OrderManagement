using OrderManagement.Common.DTO.Authenticate;
using OrderManagement.Common.DTO.User;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;

namespace OrderManagement.Common.Contracts
{
    public interface IUserBusinessEngine
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<List<User>> GetAll();
        Task<Result<User>> GetById(int id);
        Task<Result<User>> CreateUser(UserCreateDto userCreateDto);
    }
}

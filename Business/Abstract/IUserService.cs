using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult AddUser(User user);
        IResult Update(User user);
        IResult Delete(User user);  
        IDataResult<List<User>> GetAll();
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(int userId);
        IDataResult<List<UserDto>> GetUsersDetails();
    }
}

using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess;
using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EntityRepositoryBase<User, KodlamaioContext>, IUserDal
    {

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new KodlamaioContext())
            {
                var claims = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                return claims.ToList();
            }
        }

    }
}

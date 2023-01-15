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

    public class EfUserOperationClaimDal : EntityRepositoryBase<UserOperationClaim, KodlamaioContext>, IUserOperationClaimDal
    {
        public List<UserOperationClaimsDto> GetDetails()
        {
            using (var context = new KodlamaioContext())
            {
                var claims = from userClaim in context.UserOperationClaims
                             join user in context.Users
                             on userClaim.UserId equals user.Id
                             join operationClaim in context.OperationClaims
                             on userClaim.OperationClaimId equals operationClaim.Id
                             select new UserOperationClaimsDto { ClaimName = operationClaim.Name, Username = $"{user.FirstName} {user.LastName}", Email = user.Email};
                return claims.ToList();
            }
        }
    }
}

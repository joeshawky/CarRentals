using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete.Dtos;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim);
        IResult Delete(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<List<UserOperationClaimsDto>> GetDetails();
    }
}

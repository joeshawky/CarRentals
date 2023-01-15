using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EntityRepositoryBase<OperationClaim, KodlamaioContext>, IOperationClaimDal
    {
    }
}

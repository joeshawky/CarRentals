using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using Entities.Concrete.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("admin")]
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IUserService _userService;
        private readonly IOperationClaimService _operationClaimService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IUserService userService, IOperationClaimService operationClaimService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _userService = userService;
            _operationClaimService = operationClaimService;
        }


        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var result = Validator.Run(
                UserIdExists(userOperationClaim.UserId),
                OperationClaimIdExists(userOperationClaim.OperationClaimId));

            if (result.Success == false)
                return result;

            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            var result = Validator.Run(UserOperationClaimExists(userOperationClaim));
            if (result.Success == false)
                return result;

            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        public IResult Update(UserOperationClaim userOperationClaim)
        {
            var result = Validator.Run(UserOperationClaimIdExists(userOperationClaim.Id));
            if (result.Success == false)
                return result;
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }
        public IDataResult<List<UserOperationClaimsDto>> GetDetails()
        {
            var claims = _userOperationClaimDal.GetDetails();
            return new SuccessDataResult<List<UserOperationClaimsDto>>(claims);
        }

        private IResult UserIdExists(int userId)
        {
            var userResult = _userService.GetById(userId);
            if (userResult.Success)
                return new SuccessResult(userResult.Message);

            return new ErrorResult(userResult.Message);

        }

        private IResult OperationClaimIdExists(int operationClaimId)
        {
            var operationClaimResult = _operationClaimService.GetById(operationClaimId);
            if (operationClaimResult.Success)
                return new SuccessResult(operationClaimResult.Message);

            return new ErrorResult(operationClaimResult.Message);
        }

        private IResult UserOperationClaimExists(UserOperationClaim userOperationClaim)
        {
            var userOperationClaimSrc = _userOperationClaimDal.Get(u => u.Id == userOperationClaim.Id);

            if (userOperationClaimSrc == null)
                return new ErrorResult(Messages.UserOperationClaimNotFound);


            return ObjectsComparer.CompareByValues(userOperationClaim, userOperationClaimSrc)
                ? new SuccessResult()
                : new ErrorResult(Messages.UserOperationClaimInaccurate);
        }

        private IResult UserOperationClaimIdExists(int userOperationClaimId)
        {
            var userOperationClaimSrc = _userOperationClaimDal.Get(u => u.Id == userOperationClaimId);

            return userOperationClaimSrc == null ? new ErrorResult(Messages.UserOperationClaimNotFound) : new SuccessResult();
            //userOperationClaimSrc == null ? return new ErrorResult() : return new SuccessResult();
        }

        
    }
}

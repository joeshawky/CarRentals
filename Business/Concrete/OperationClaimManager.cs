using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    [SecuredOperation("admin")]
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }


        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        public IResult Update(OperationClaim operationClaim)
        {
            var result = Validator.Run(OperationClaimIdExists(operationClaim.Id));
            if (result.Success == false)
                return result;
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);

        }
        public IResult Delete(OperationClaim operationClaim)
        {
            var result = Validator.Run(OperationClaimExists(operationClaim));
            if (result.Success == false)
                return result;

            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IDataResult<OperationClaim> GetById(int operationClaimId)
        {
            var result = Validator.Run(OperationClaimIdExists(operationClaimId));

            if (result.Success == false)
                return new ErrorDataResult<OperationClaim>(result.Message);

            var operationClaim = _operationClaimDal.Get(oc => oc.Id == operationClaimId);
            return new SuccessDataResult<OperationClaim>(operationClaim);
        }

        private IResult OperationClaimIdExists(int operationClaimId)
        {
            var operationClaim = _operationClaimDal.Get(oc => oc.Id == operationClaimId);

            if (operationClaim == null)
                return new ErrorResult(Messages.OperationClaimNotFound);

            return new SuccessResult();

        }

        private IResult OperationClaimExists(OperationClaim operationClaim)
        {
            var operationClaimSrc = _operationClaimDal.Get(u => u.Id == operationClaim.Id);

            if (operationClaimSrc == null)
                return new ErrorResult(Messages.OperationClaimNotFound);

            return ObjectsComparer.CompareByValues(operationClaim, operationClaimSrc)
                ? new SuccessResult()
                : new ErrorResult(Messages.OperationClaimInaccurate);
        }

    }
}

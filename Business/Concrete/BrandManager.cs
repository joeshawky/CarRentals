using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            var result = Validator.Run(BrandNameExists(brand.Name));

            if (result.Success)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExists);
            }


            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult Delete(Brand brand)
        {
            var result = Validator.Run(BrandExists(brand));

            
            if (result.Success == false)
                return result;

            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<List<Brand>> GetAll(Func<Brand, bool>? filter = null)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(filter));

        }

        public IDataResult<Brand> GetById(int brandId)
        {
            var brand = _brandDal.Get(c => c.Id == brandId);
            if (brand is null)
                return new ErrorDataResult<Brand>(Messages.BrandNotFound);

            return new SuccessDataResult<Brand>(brand);
        }

        public IResult Update(Brand brand)
        {
            var result = Validator.Run(BrandExists(brand));

            if (result.Success == false)
                return result;

            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        private IResult BrandNameExists(string brandName)
        {
            var brandResult = _brandDal.GetAll(b => b.Name.ToLower() == brandName.ToLower()).Any();
            if (brandResult)
            {
                return new SuccessResult(Messages.BrandNameAlreadyExists);
            }

            return new ErrorResult();
        }
        
        private IResult BrandExists(Brand brand)
        {
            var brandResult = _brandDal.GetAll(b => b.Id == brand.Id && b.Name == brand.Name).Any();
            if (brandResult)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.BrandNotFound);
        }
    }
}

using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult AddCustomer(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            var result = Validator.Run(CustomerExists(customer));

            if (result.Success == false)
                return result;

            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IResult Update(Customer customer)
        {
            var result = Validator.Run(CustomerExistsById(customer.Id));

            if (result.Success == false)
                return result;

            _customerDal.Update(customer);

            return new SuccessResult(Messages.CustomerUpdated);
        }

        private IResult CustomerExistsById(int customerId)
        {
            var customer = _customerDal.Get(c => c.Id == customerId);

            return customer == null ? new ErrorResult(Messages.CustomerNotFound) : new SuccessResult();
        }
        private IResult CustomerExists(Customer customer)
        {
            var customerSrc = _customerDal.Get(c => c.Id == customer.Id);

            if (customerSrc == null)
                return new ErrorResult(Messages.CustomerNotFound);

            var result = ObjectsComparer.CompareByValues(customer, customerSrc);

            return result ? new SuccessResult() : new ErrorResult(Messages.CustomerNotFound);
        }
    }
}

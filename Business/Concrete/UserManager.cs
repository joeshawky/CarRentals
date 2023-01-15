using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Resource;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private readonly IMapper _mapper;
        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult AddUser(User user)
        {
            var result = Validator.Run(EmailExists(user.Email));

            if (result.Success)
                return result;
            
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }


        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            var result = Validator.Run(UserExistsById(user.Id));

            if (result.Success == false)
                return result;

            _userDal.Update(user);

            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult Delete(User user)
        {
            var result = Validator.Run(UserExists(user));

            if (result.Success == false)
                return result;

            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var claims = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }

        public IDataResult<User> GetById(int userId)
        {
            var result = Validator.Run(UserExistsById(userId));
            if (result.Success == false)
                return new ErrorDataResult<User>(Messages.UserNotFound);
            
            
            var user = _userDal.Get(u => u.Id == userId);
            return new SuccessDataResult<User>(user);
        }
        public IDataResult<List<UserDto>> GetUsersDetails()
        {
            var usersDto = _mapper.Map<List<User>,List<UserDto>>(_userDal.GetAll());
            return new SuccessDataResult<List<UserDto>>(usersDto);


        }
        private IResult UserExistsById(int userId)
        {
            var user = _userDal.Get(c => c.Id == userId);

            return user == null ? new ErrorResult(Messages.UserNotFound) : new SuccessResult();
        }
        private IResult UserExists(User user)
        {
            var userSrc = _userDal.Get(c => c.Id == user.Id);

            if (userSrc == null)
                return new ErrorResult(Messages.UserNotFound);

            var result = ObjectsComparer.CompareByValues(user, userSrc);

            return result ? new SuccessResult() : new ErrorResult(Messages.UserNotFound);
        }


        private IResult EmailExists(string email)
        {
            var result = _userDal.GetAll(u => u.Email == email).Any();
            if (result)
                return new SuccessResult();

            return new ErrorResult(Messages.EmailAlreadyExists);
        }

        
    }
}

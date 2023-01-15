using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            }

        [ValidationAspect(typeof(UserRegisterDtoValidator))]
        public IDataResult<User> Register(UserRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            var addResult = _userService.AddUser(user);
            if (addResult.Success)
            {
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }

            return new ErrorDataResult<User>(Messages.UserNotAdded);
        }

        public IDataResult<User> Login(UserLoginDto userForLoginDto)
        {
            var userToCheckResult = _userService.GetByMail(userForLoginDto.Email);

            if (userToCheckResult.Success == false)
            {
                return userToCheckResult;
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheckResult.Data.PasswordHash, userToCheckResult.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheckResult.Data, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Success)
            {
                return new SuccessResult(Messages.UserAlreadyExists);
            }

            return new ErrorResult();

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            if (claims.Success == false)
                return new ErrorDataResult<AccessToken>();
            
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}

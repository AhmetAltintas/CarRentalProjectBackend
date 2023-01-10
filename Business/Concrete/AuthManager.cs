using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            var addUserResult = _userService.Add(user);
            if(!addUserResult.Success) return new ErrorDataResult<User>(addUserResult.Message);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }


        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            var result = BusinessRules.Run(CheckIfPasswordsMatch(updatePasswordDTO.NewPassword, updatePasswordDTO.NewPasswordAgain));
            if (!result.Success) return result;

            var userResult = _userService.GetById(updatePasswordDTO.Id);

            var passwordVerificationResult = HashingHelper.VerifyPasswordHash(updatePasswordDTO.Password, userResult.Data.PasswordHash, userResult.Data.PasswordSalt);
            if (!passwordVerificationResult) return new ErrorResult(Messages.PasswordIsIncorrect);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updatePasswordDTO.NewPassword,out passwordHash,out passwordSalt);

            userResult.Data.PasswordHash = passwordHash;
            userResult.Data.PasswordSalt = passwordSalt;

            var updateResult = _userService.Update(userResult.Data);
            if (!updateResult.Success) return updateResult;

            return new SuccessResult(Messages.PasswordUpdated);
            
        }





        private IResult CheckIfPasswordsMatch(string newPassword, string newPasswordAgain)
        {
            if (newPassword != newPasswordAgain)
            {
                return new ErrorResult(Messages.PasswordsDoNotMatch);
            }
            return new SuccessResult();
        }
    }
}

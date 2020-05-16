using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Domain.Services.Communication;
using PostalCodeApi.Entities;
using PostalCodeApi.Extensions;
using PostalCodeApi.Helpers;

namespace PostalCodeApi.Services
{
    public class UserService : IUserService
    {
        private readonly Regex _passwordRegex =
            new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedAndSortedList<User>> GetAllAsync(int pageNumber, int pageSize, string sort)
        {
            return await _userRepository.ListAllAsync(pageNumber, pageSize, sort);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.FindByIdAsync(id);
        }

        public async Task<UserResponse> UpdatePasswordAsync(int id, string oldPassword, string newPassword)
        {
            var user = await _userRepository.FindByIdAsync(id);

            if (user == null)
                return new UserResponse(
                    "An error occurred when authenticating the user: user not found",
                    StatusCodes.Status404NotFound);

            if (!PasswordHelper.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
                return new UserResponse("An error occurred when authenticating the user: password is incorrect",
                    StatusCodes.Status400BadRequest);

            if (!_passwordRegex.IsMatch(newPassword))
                return new UserResponse(
                    "An error occurred when saving the user: password must contain a lower and upper case letter, a number and a special character ",
                    StatusCodes.Status400BadRequest);

            try
            {
                PasswordHelper.CreatePasswordHash(newPassword, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _userRepository.Update(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateRoleAsync(int id, string role)
        {
            var user = await _userRepository.FindByIdAsync(id);

            if (user == null)
                return new UserResponse("An error occurred when updating the user: user not found",
                    StatusCodes.Status404NotFound);

            user.Role = role == Role.Admin ? Role.Admin : Role.User;

            try
            {
                _userRepository.Update(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when updating the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> SaveAsync(User user, string password)
        {
            var existingUser = await _userRepository.FindByUsernameAsync(user.Username);
            if (existingUser != null)
                return new UserResponse(
                    $"An error occurred when saving the user: username {user.Username} is already taken",
                    StatusCodes.Status400BadRequest);

            if (!_passwordRegex.IsMatch(password))
                return new UserResponse(
                    "An error occurred when saving the user: password must contain a lower and upper case letter, a number and a special character ",
                    StatusCodes.Status400BadRequest);

            try
            {
                PasswordHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);

            if (user == null)
                return new UserResponse("An error occurred when deleting the user: user not found",
                    StatusCodes.Status404NotFound);

            try
            {
                _userRepository.Remove(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when deleting the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return new UserResponse("An error occurred when authenticating the user: user or password is empty",
                    StatusCodes.Status400BadRequest);

            var user = await _userRepository.FindByUsernameAsync(username);

            if (user == null)
                return new UserResponse(
                    "An error occurred when authenticating the user: username or password is incorrect",
                    StatusCodes.Status400BadRequest);
            try
            {
                return !PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)
                    ? new UserResponse(
                        "An error occurred when authenticating the user: username or password is incorrect",
                        StatusCodes.Status400BadRequest)
                    : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when authenticating the user: {ex.Message}");
            }
        }
    }
}
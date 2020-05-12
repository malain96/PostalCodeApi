using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Domain.Services.Communication;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Services
{
    public class UserService : IUserService
    {
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

        public Task<UserResponse> UpdatePasswordAsync(int id, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> UpdateIsAdminAsync(int id, bool isAdmin)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse($"An error occurred when updating the user: user not found", false);

            existingUser.IsAdmin = isAdmin;
            
            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when updating the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateTokenAsync(int id, string token)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse($"An error occurred when updating the user: user not found", false);

            existingUser.Token = token;
            
            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
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
                    $"An error occurred when saving the user: username {user.Username} is already taken", false);

            var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!regex.IsMatch(password))
                return new UserResponse(
                    "An error occurred when saving the user: password must contain a lower and upper case letter, a number and a special character ",
                    false);

            try
            {
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse($"An error occurred when deleting the user: user not found", false);

            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when deleting the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return new UserResponse($"An error occurred when authenticating the user: user or password is empty",
                    false);

            var user = await _userRepository.FindByUsernameAsync(username);

            if (user == null)
                return new UserResponse(
                    $"An error occurred when authenticating the user: username or password is incorrect",
                    false);
            try
            {
                return !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)
                    ? new UserResponse($"An error occurred when authenticating the user: username or password is incorrect",
                        false)
                    : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when authenticating the user: {ex.Message}");
            }
            
        }

        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).",
                    nameof(storedSalt));

            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != storedHash[i]).Any();
        }
    }
}
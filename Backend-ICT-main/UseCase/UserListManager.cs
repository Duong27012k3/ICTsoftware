using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace UseCase
{
    public class UserListManager
    {
        private readonly IUserRepository _userRepository;

        public UserListManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUserByID(int id)
        {
            return _userRepository.GetByID(id);
        }

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");

            var user = _userRepository.GetByUsername(username);
            if (user == null)
                throw new Exception("Tên đăng nhập không tồn tại.");
            // Fix: Use BCrypt.Net.BCrypt.Verify instead of BCrypt.Verify
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Mật khẩu không đúng.");
            if (user.Status != "active")
                throw new Exception("Tài khoản đã bị vô hiệu hóa.");

            return user;
        }

        public void AddUser(User user, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("Mật khẩu không được để trống.");
            if (_userRepository.UsernameExists(user.Username))
                throw new Exception($"Tên đăng nhập '{user.Username}' đã tồn tại.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            _userRepository.Add(user);
        }

        public void UpdateUser(User user, string? newPlainPassword = null)
        {
            var existing = _userRepository.GetByID(user.UserId);
            if (existing == null)
                throw new Exception($"Không tìm thấy user với ID = {user.UserId}");

            existing.Role = user.Role;
            existing.Status = user.Status;

            if (!string.IsNullOrWhiteSpace(newPlainPassword))
                existing.Password = BCrypt.Net.BCrypt.HashPassword(newPlainPassword);

            _userRepository.Update(existing);
        }

        public void DeleteUser(int id, int currentUserId)
        {
            if (id == currentUserId)
                throw new Exception("Không thể xóa tài khoản đang đăng nhập.");

            var user = _userRepository.GetByID(id);
            if (user == null)
                throw new Exception($"Không tìm thấy user với ID = {id}");

            _userRepository.Delete(id);
        }
    }
}

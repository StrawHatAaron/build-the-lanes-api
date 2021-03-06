﻿using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;


namespace BuildTheLanesAPI.Services
{
    public interface IUserService
    {
        Users Authenticate(string email, string password);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        Users Create(Users user, string password);
        void Update(Users user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private WebappContext _context;

        public UserService(WebappContext context)
        {
            _context = context;
        }

        public Users Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;
                
            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            // return null if user not found
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            Console.WriteLine("getting here 1?");
            var users = _context.Users; 
            Console.WriteLine(users);
            Console.WriteLine("getting here 2?");
            return users;
        }

        public Users GetById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id==id);
        }

        public Users Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Users userParam, string password = null)
        {
            // var user = _context.Users.Find(userParam.Email);
            
            _context.Users.Attach(userParam);
            
            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.Token))
                _context.Entry(userParam).Property(x => x.Token).IsModified = true;

            if (!string.IsNullOrWhiteSpace(userParam.FName))
                _context.Entry(userParam).Property(x => x.FName).IsModified = true;

            if (!string.IsNullOrWhiteSpace(userParam.LName))
                _context.Entry(userParam).Property(x => x.LName).IsModified = true;

            if (!string.IsNullOrWhiteSpace(userParam.Roles))
                _context.Entry(userParam).Property(x => x.Roles).IsModified = true;

            if(userParam.AmountDonated.HasValue)
                _context.Entry(userParam).Property(x => x.AmountDonated).IsModified = true;
                        
            if (!string.IsNullOrWhiteSpace(userParam.Title))
                _context.Entry(userParam).Property(x => x.Title).IsModified = true;

            if (!string.IsNullOrWhiteSpace(userParam.Type))
                _context.Entry(userParam).Property(x => x.Type).IsModified = true;

            if(userParam.Created.HasValue)
                _context.Entry(userParam).Property(x => x.Created).IsModified = true;
                
            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

            }

            // _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
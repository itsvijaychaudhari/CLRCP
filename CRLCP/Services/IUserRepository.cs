
using CRLCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Services
{
   public  interface IUserRepository
   {
        LoginDetails Authenticate(LoginModel details);
        IEnumerable<LoginDetails> GetAll();
        LoginDetails GetByEmailId(string EmailId);
        //public LoginDetails GetById(string EmailId)
        LoginDetails Create(LoginDetails user);
        void Update(LoginDetails user, string password = null);
        void Delete(int id);
   }

    public class UserService : IUserRepository
    {
        private CLRCP_MASTERContext _masterContext;
        public UserService(CLRCP_MASTERContext context)
        {
            _masterContext = context;
        }


        public LoginDetails Authenticate(LoginModel details)
        {
            if (string.IsNullOrEmpty(details.EmailId) || string.IsNullOrEmpty(details.Password))
                return null;

            var user = _masterContext.LoginDetails.SingleOrDefault(validUser => validUser.EmailId == details.EmailId);
            // check if username exists
            if (user == null)
                return null;
            else
            {
                if (user.Password == details.Password)
                    return user;
                else
                    return null;
            }
        }

        public LoginDetails Create(LoginDetails user)
        {
            try
            {
                _masterContext.LoginDetails.Add(user);
                _masterContext.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoginDetails> GetAll()
        {
            throw new NotImplementedException();
        }

        public LoginDetails GetByEmailId(string EmailId)
        {
            return _masterContext.LoginDetails.FirstOrDefault(item => item.EmailId == EmailId);
        }

        //public LoginDetails GetById(int Id)
        //{
        //    return _masterContext.LoginDetails.FirstOrDefault(item => item.UserId == Id);
        //}

        public void Update(LoginDetails user, string password = null)
        {
            throw new NotImplementedException();
        }


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

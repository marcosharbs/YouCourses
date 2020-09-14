using System.Collections.Generic;
using Core.Domain;
using System.Security.Cryptography;
using System.Text;

namespace User.Domain.UserAggregate.Model
{
    public class UserPassword : ValueObject<UserPassword>
    {
        public string Password { get; }

        protected UserPassword() { }

        public UserPassword(string password)
        {
            Password = CreateMD5(password);
        }

        private string CreateMD5(string input)
        {
            if(input == null)
                return "";
                
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Password;
        }
    }
}
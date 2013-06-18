using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Password.Models;

namespace PasswordPolicyController
{
    public class PasswordPolicyPBKDF2
    {
        public PasswordHashResult HashPassword(string password)
        {
            // specify that we want to randomly generate a 20-byte salt
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 64))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] key = deriveBytes.GetBytes(20); // derive a 20-byte key

                return new PasswordHashResult { Password = key, Salt = salt };
            }
        }


        //TODO get the password complexity settings model and methods into this area (proejcts)
    }
}

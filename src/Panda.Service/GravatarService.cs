using System;
using System.Collections.Generic;
using System.Text;
using Panda.Core.Contracts;

namespace Panda.Service
{
    public class GravatarService : IGravatarService
    {
        public string GetGravatarHash(string email)
        {
            using (System.Security.Cryptography.MD5 md5 =
                System.Security.Cryptography.MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(email.Trim().ToLower()));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}

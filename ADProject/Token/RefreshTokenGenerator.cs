using ADProject.Data;
using ADProject.Interface;
using ADProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ADProject.Token
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly EmployeesDBContext context;

        public RefreshTokenGenerator(EmployeesDBContext dbContext)
        {
            context = dbContext;
        }
        public string GenerateToken(int userId)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string RefreshToken = Convert.ToBase64String(randomnumber);

                var _user = context.refreshtokens.FirstOrDefault(o => o.UserId == userId);
                if (_user != null)
                {
                    _user.RefreshToken = RefreshToken;
                    context.SaveChanges();
                }
                else
                {
                    Refreshtoken tblRefreshtoken = new Refreshtoken()
                    {
                        UserId = userId,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = RefreshToken,
                        IsActive = true
                    };
                    context.refreshtokens.Add(tblRefreshtoken);
                    context.SaveChanges();
                }
               
                return RefreshToken;
            }
        }
    }
}

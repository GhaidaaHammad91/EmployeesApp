using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProject.Interface
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken(int userId);
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.CommonInterface
{
    public interface SystemInterface
    {
        Task<User?> LoginAsync(string email, string password);
    }
}

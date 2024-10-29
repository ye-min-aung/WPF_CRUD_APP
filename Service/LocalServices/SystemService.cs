using DataAccess.Repositories;
using Model;
using Service.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LocalServices
{
    
    public class SystemService : SystemInterface
    {
        public SystemService()
        {
            this.userRepository = new UserRepository();
        }

        public UserRepository userRepository { get; }

        public async Task<User?> LoginAsync(string email, string password)
        {
            User userModel= new User();
            userModel.Email = email;
            userModel.Password = password;
            bool reult = await userRepository.LoginAsync(userModel);
            if (reult)
            {
                return userModel;
            }
            else
            {
                return new User();
            }
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}

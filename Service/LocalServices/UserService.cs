using CGMWPF.Common;
using DataAccess.Repositories;
using Model;
using Service.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.LocalServices
{
    public class UserService : UserInterface,IDisposable
    {
        public UserService() 
        {
            this.userRepository = new UserRepository();
        }
        private readonly UserRepository userRepository;
        public async Task<ObservableCollection<User>> GetAllAsync(string searchName)
        {
            var result=new ObservableCollection<User>();
            var  userlist = await userRepository.GetUserList(searchName);
            if (userlist == null)
            {
               return result;
            }
            else
            {
                result = new ObservableCollection<User>(userlist);
                return result;
            }
        }

        public async Task<ObservableCollection<Role>> GetRoleAsync()
        {
            var result=new ObservableCollection<Role>();
            var rolelist = await userRepository.GetRoleList();
            if(rolelist ==null) 
            { return result; }
            else
            {
                result = new ObservableCollection<Role>(rolelist);
                return result;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        public async Task<int> UpdateAsync(User obj, string methodName)
        {
            int result=0;
            if (methodName == "SaveUser")
            {
                result = await userRepository.SaveUser(obj);
                return iConvert.ToInt(result);
            }
            else if(methodName == "UpdateUser")
            {
                result= await userRepository.UpdateUser(obj);
                return iConvert.ToInt(result);
            }
            else
            {
                result=await userRepository.DeleteUser(obj);
                return iConvert.ToInt(result);
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}

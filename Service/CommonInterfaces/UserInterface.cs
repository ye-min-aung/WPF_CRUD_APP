using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CommonInterfaces
{
    public interface UserInterface
    {
       Task<ObservableCollection<User>> GetAllAsync(string searchName);
       Task<ObservableCollection<Role>> GetRoleAsync();
       Task<int> UpdateAsync(User obj, string methodName);
       Task<User> GetUserById(int id);
    }
}

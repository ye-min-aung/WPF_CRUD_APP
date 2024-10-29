using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CommonInterfaces
{
    public interface PostInterface
    {
        Task<ObservableCollection<Post>> GetPostsAsync(string? searchString);
        Task<int> UpdateAsync(Post obj, string methodName);
        Task<int> DeletePost(Post obj);
        Task<int> UploadPost(List<Post> postData);
        Task<ObservableCollection<Post>> DownloadPost(User user);
        Task<Post> GetPostById(int id);
    }
}

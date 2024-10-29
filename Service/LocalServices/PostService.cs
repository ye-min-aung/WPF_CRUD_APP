using CGMWPF.Common;
using DataAccess.Repositories;
using Model;
using Service.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LocalServices
{
    public class PostService : PostInterface,IDisposable

    {
        private readonly PostRepository postRepository;
        public PostService() 
        {
            this.postRepository = new PostRepository();

        }
        public async Task<ObservableCollection<Post>> GetPostsAsync(string? searchString)
        {
            var result = new ObservableCollection<Post>();
            var postlist = await postRepository.GetPostList(searchString);
            if (postlist == null)
            {
                return result;
            }
            else
            {
                result = new ObservableCollection<Post>(postlist);
                return result;
            }
        }
        public async Task<Post> GetPostById(int id)
        {
            return await postRepository.GetPostById(id);
        }
        public async Task<int> UpdateAsync(Post obj, string methodName)
        {
            int result = 0;
            if (methodName == "SavePost")
            {
                result = await postRepository.SavePost(obj);
                return iConvert.ToInt(result);
            }
            else if (methodName == "UpdatePost")
            {
                result = await postRepository.UpdatePost(obj);
                return iConvert.ToInt(result);
            }
            else
            {
                result = await postRepository.DeletePost(obj);
                return iConvert.ToInt(result);
            }
        }
        public async Task<int> DeletePost(Post obj)
        {
            int result = 0;
            result = await postRepository.DeletePost(obj);
            return iConvert.ToInt(result);
        }
        public async Task<ObservableCollection<Post>> DownloadPost(User user)
        {
            var result = new ObservableCollection<Post>();
            var postlist =await postRepository.DownloadPost(user);
            if(postlist == null)
            {
                return result;
            }
            else
            {
                result = new ObservableCollection<Post>(postlist);
                return result;
            }
        }
       
        public async Task<int> UploadPost(List<Post> postData)
        {
            int result = 0;
            result = await postRepository.UploadPost(postData);
            return iConvert.ToInt(result);
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}


using CGMWPF.Common;
using CGMWPF.Front.AppControls;
using CGMWPF.Model;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using CGMWPF.Service;
using DataAccess.Repositories;
using Service.LocalServices;

namespace Client.Post
{
    public class PostViewModel:iViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PostViewModel()
        {
            PostList = new ObservableCollection<PostModel>();
            Post = new PostModel();
            Initilized();
            GetPostList();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public PostViewModel(int id)
        {
            Post = new PostModel();
            PostList = new ObservableCollection<PostModel>();
            Initilized();
            GetPostById(id);
        }

        /// <summary>
        /// Define post list
        /// </summary>
        public ObservableCollection<PostModel> PostList { get; set; }

        /// <summary>
        /// Define post
        /// </summary>
        public PostModel Post { get; set; }

        /// <summary>
        /// Define search command
        /// </summary>
        public iDelegateCommand? Search { get; set; }

        /// <summary>
        /// Define delete command
        /// </summary>
        public iDelegateCommand? Delete { get; set; }

        /// <summary>
        /// Define create command
        /// </summary>
        public iDelegateCommand? Create { get; set; }

        /// <summary>
        /// Define cancel command
        /// </summary>
        public iDelegateCommand? Cancel { get; set; }

        /// <summary>
        /// Define save command
        /// </summary>
        public iDelegateCommand?     Save { get; set; }

        /// <summary>
        /// Define upload command
        /// </summary>
        public iDelegateCommand? Upload { get; set; }

        /// <summary>
        /// Define download command
        /// </summary>
        public iDelegateCommand? Download { get; set; }
        /// <summary>
        /// Define Api
        /// </summary>
        private PostService commonApi;
        /// <summary>
        /// Define repo
        /// </summary>
        private PostRepository userRepo;
        /// <summary>
        /// Initialize
        /// </summary>
        private void Initilized()
        {
            this.Create = new iDelegateCommand(
                (object? arg) =>
                {
                    MainViewModel.Instance.PagePath = iNavigation.POST_CREATE;
                }, () => true);
            this.Search = new iDelegateCommand(
                (object? arg) =>
                {
                    this.GetPostList();
                }, () => true);
            this.Delete = new iDelegateCommand(
                (object? arg) =>
                {
                    this.DeletePost(iConvert.ToInt(arg));
                }, () => true);
            this.Save = new iDelegateCommand(
                (object? arg) =>
                {
                    this.UpdateAsync();
                }, () => true);
            this.Upload = new iDelegateCommand(
                (object? arg) =>
                {
                    this.UploadPost();
                }, () => true);
            this.Download = new iDelegateCommand(
                (object? arg) =>
                {
                    this.DownloadPost();
                }, () => true);
            this.Cancel = new iDelegateCommand(
                (object? arg) =>
                {
                    MainViewModel.Instance.PagePath = iNavigation.POST_LIST;
                },
                () => true);

        }

        /// <summary>
        /// Get all posts
        /// </summary>
        private async void GetPostList()
        {
            PostList.Clear();
            var commonApi = new PostService();
            
                var getPosts = await commonApi.GetPostsAsync(Post.Keyword);
                if (getPosts.Count > 0)
                {
                    foreach (var data in getPosts)
                    {
                        PostModel post = new();
                        iConvert.CopyClassProperty<Model.IPost>(data, post);
                        if (iAppSettings.LoginUser.Role != 1)
                        {
                            if (post.CreatedUserId == iConvert.ToString(iAppSettings.LoginUser.Id))
                            {
                                PostList.Add(post);
                            }
                        }
                        else
                        {
                            PostList.Add(post);
                        }

                    }
                }
        }

        /// <summary>
        /// Save,Update post data by checking id
        /// </summary>
        private async void UpdateAsync()
        {
            var commonApi = new PostService();
            
                if (CheckInput())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    Model.Post data = new();
                    iConvert.CopyClassProperty<Model.IPost>(Post, data);
                    if (Post.Id > 0)
                    {
                        data.UpdatedUserId = iConvert.ToString(iAppSettings.LoginUser.Id);
                        int result = await commonApi.UpdateAsync(data, "UpdatePost");
                        Mouse.OverrideCursor = null;
                        if (result == Constant.APIRESULT_SUCCESS)
                        {
                            iMessage.ShowInfomation(iMessage.MT_0190, iMessage.IMSG_TRAN_0100);
                            MainViewModel.Instance.PagePath = iNavigation.POST_LIST;
                        }
                        else
                        {
                            iMessage.ShowError(iMessage.MT_0190, iMessage.EMSG_TRAN_0150);
                        }
                    }
                    else
                    {
                        data.CreatedUserId = iConvert.ToString(iAppSettings.LoginUser.Id);
                        int result = await commonApi.UpdateAsync(data, "SavePost");
                        Mouse.OverrideCursor = null;
                        if (result == Constant.APIRESULT_SUCCESS)
                        {
                            iMessage.ShowInfomation(iMessage.MT_0180, iMessage.IMSG_TRAN_0090);
                            MainViewModel.Instance.PagePath = iNavigation.POST_LIST;
                        }
                        else
                        {
                            iMessage.ShowError(iMessage.MT_0180, iMessage.EMSG_TRAN_0140);
                        }
                    }
                }
            
        }

        /// <summary>
        /// Get post data by id
        /// </summary>
        /// <param name="id"></param>
        private async void GetPostById(int id)
        {
                var commonApi = new PostService();
                var getData = await commonApi.GetPostById(id);
                iConvert.CopyClassProperty<Model.IPost>(getData, Post);
        }

        /// <summary>
        /// Delete post data by id
        /// </summary>
        /// <param name="id"></param>
        private async void DeletePost(int id)
        {
            MessageBoxResult resultData = iMessage.ShowQuestion(iMessage.MT_0200,iMessage.QMSG_TRAN_POST_3020,MessageBoxResult.Cancel);
            if (resultData == MessageBoxResult.Yes)
            {
                var commonApi = new PostService();
                
                    Model.Post deletePost = new()
                    {
                        Id = id,
                        IsDeleted = true,
                        DeletedUserId = iConvert.ToString(iAppSettings.LoginUser.Id)
                    };
                    int result = await commonApi.DeletePost(deletePost);
                    if (result == Constant.APIRESULT_SUCCESS)
                    {
                        iMessage.ShowInfomation(iMessage.MT_0200, iMessage.IMSG_TRAN_0110);
                        this.GetPostList();
                    }
                    else
                    {
                        iMessage.ShowError(iMessage.MT_0200, iMessage.EMSG_TRAN_0160);
                    }

                }
        }

        /// <summary>
        /// Upload post data
        /// </summary>
        private async void UploadPost()
        {
            OpenFileDialog ofd = new();
            ofd.Multiselect = false;
            ofd.Filter = "Excel files (*.xlsx,*.xls)|*.xlsx;*.xls";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (ofd.ShowDialog() == true)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new(new FileInfo(ofd.FileName)))
                {
                    ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                    List<Model.Post> postList = new();
                    for (int r = 1; r <= excelWorksheet.Dimension.Rows; r++)
                    {
                        Model.Post post = new()
                        {
                            Title = iConvert.ToString(excelWorksheet.Cells[r, 1].Value),
                            Description = iConvert.ToString(excelWorksheet.Cells[r, 2].Value),
                            IsPublished = iConvert.ToString(excelWorksheet.Cells[r, 3].Value) == "Published",
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            CreatedUserId = iConvert.ToString(iAppSettings.LoginUser.Id)
                        };
                        postList.Add(post);
                    }
                    Mouse.OverrideCursor = Cursors.Wait;
                    var commonApi = new PostService();
                    int result = await commonApi.UploadPost(postList);
                    Mouse.OverrideCursor = null;
                    if (result == Constant.APIRESULT_SUCCESS)
                    {
                        iMessage.ShowInfomation(iMessage.MT_0210, iMessage.IMSG_TRAN_0120);
                        this.GetPostList();
                    }
                    else
                    {
                        iMessage.ShowWarning(iMessage.MT_0210, iMessage.EMSG_TRAN_0170);
                    }
                }

            }
        }

        /// <summary>
        /// Download post data
        /// </summary>
        private async void DownloadPost()
        {
            var currentTime = DateTime.Now;
            var fileName = "posts_" + currentTime.ToString("yyyyMMddHHmmss") + ".xlsx";
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Excel Worksheets (*.xlsx)|*.xlsx",
                FileName = fileName,
            };
            if (saveFileDialog.ShowDialog() == true)
            {

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new())
                {
                    ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Posts");
                    excelWorksheet.Cells[1, 1].Value = "Title";
                    excelWorksheet.Cells[1, 2].Value = "Description";
                    excelWorksheet.Cells[1, 3].Value = "Status";
                    var commonApi = new PostService();
                    var getPosts = await commonApi.DownloadPost(iAppSettings.LoginUser);
                    int index = 0;
                    if (getPosts.Count > 0)
                    {
                        foreach (var post in getPosts)
                        {
                            excelWorksheet.Cells[index + 2, 1].Value = post.Title;
                            excelWorksheet.Cells[index + 2, 2].Value = post.Description;
                            excelWorksheet.Cells[index + 2, 3].Value = post.IsPublished ? "Published" : "Unpublished";
                            index++;
                        }
                        excelPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                        iMessage.ShowInfomation(iMessage.MT_0220, iMessage.IMSG_TRAN_0130);
                    }
                    else
                    {
                        iMessage.ShowError(iMessage.MT_0220, iMessage.EMSG_TRAN_0180);
                    }
                }
            }
        }

        /// <summary>
        /// Validate input
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool checkResult = true;
            string messageTtl = Post.Id > 0 ? iMessage.MT_0190 : iMessage.MT_0180;
            if(string.IsNullOrEmpty(Post.Title))
            {
                iMessage.ShowInfomation(messageTtl,iMessage.WMSG_TRAN_P_2180);
                checkResult = true;
            }
            else if(string.IsNullOrEmpty(Post.Description))
            {
                iMessage.ShowInfomation(messageTtl,iMessage.WMSG_TRAN_P_2190);
                checkResult = false;
            }
            return checkResult;
        }
    }
}

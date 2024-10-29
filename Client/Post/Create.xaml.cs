﻿using Client.Post;
using System.Windows.Controls;

namespace Client.Post
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Page
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Create()
        {
            InitializeComponent();
            vm = new PostViewModel();
            this.DataContext = vm;
        }

        /// <summary>
        /// Define vm
        /// </summary>
        private PostViewModel vm;
    }
}

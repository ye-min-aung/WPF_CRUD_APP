using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Product
{
    public class ProductModel : IProduct,INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private bool isActive;
        private bool isDeleted;
        private string keyword;
        private string createdBy;
        private string createdAt;
        private string active;
        private string createdUserId;

        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string Description { get => description; set { description = value; OnPropertyChanged(nameof(Description)); } }
        public bool IsActive { get => isActive; set { isActive = value; OnPropertyChanged(nameof(IsActive)); } }
        public bool IsDeleted { get => isDeleted; set { isDeleted = value; OnPropertyChanged(nameof(IsDeleted)); } }
        public string Keyword { get => keyword; set { keyword = value; OnPropertyChanged(nameof(Keyword)); } }
        public string CreatedBy { get => createdBy; set { createdBy = value; OnPropertyChanged(nameof(CreatedBy)); } }
        public string CreatedAt { get => createdAt; set { createdAt = value; OnPropertyChanged(nameof(createdAt)); } }
        public string Active { get => active; set { active = value; OnPropertyChanged(nameof(Active)); } }
        public string CreatedUserId { get => createdUserId; set { createdUserId = value; OnPropertyChanged(nameof(CreatedUserId)); } }
        public int DataStatus { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Invoke when property change
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

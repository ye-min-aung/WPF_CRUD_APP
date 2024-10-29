using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGMWPF.Model;
namespace Model
{
    public class Product : IProduct,ICommonProperty
    {
        public Product() 
        {
            this.Id = 0;
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.IsActive = true;
            this.IsDeleted = false;
            this.CreatedDate = DateTime.Now;
            this.CreatedUserId = string.Empty;
            this.UpdatedDate = null;
            this.UpdatedUserId = null;
            this.DeletedDate = null;
            this.DeletedUserId = null;
            this.CreatedAt = null;
            this.CreatedBy = null;
            this.Active = string.Empty;
            this.Keyword = string.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set ; }
        public string Description { get; set; }
        public string Active { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedUserId { get; set; }
        public string? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? Keyword { get; set; }
    }
}

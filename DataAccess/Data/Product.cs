using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Product : CommonData
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
       
    }   
}

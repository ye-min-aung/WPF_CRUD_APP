using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class CommonData
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string CreatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? UpdatedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? DeletedUserId { get; set; }
    }
}

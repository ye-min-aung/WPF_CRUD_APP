using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IProduct
    {
        /// <summary>
        /// Define id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Define name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Define description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Define isactive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Define isdeleted
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}

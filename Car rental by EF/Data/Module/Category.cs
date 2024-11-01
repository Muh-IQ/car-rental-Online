using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Module
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }

        //public Vehicle Vehicle { get; set; }
    }

}

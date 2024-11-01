using AddChainServiceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Module
{
    public class Customer:IEntity
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(17)]
        public string Phone { get; set; }

        [Required, MaxLength(20)]
        public string DriverLicenseNumber { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }
        [Required]
        public byte Age { get; set; }

        public int _ID => CustomerID;


    }
}

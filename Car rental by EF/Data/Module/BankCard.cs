using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddChainServiceLibrary;

namespace Data.Module
{
    public class BankCard : IEntity
    {
        [Key]
        public int ID { get; set; }

        // Foreign key for Customer
        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }
        public Customer customer { get; set; }

        [Required, MaxLength(20)]
        public string Number { get; set; }
        [Required]
        public byte Cvv { get; set; }
        [Required]
        public DateTime ExpierDate { get; set; }

        [Required, MaxLength(50)]
        public string CardOwnerName { get; set; }
        public int _ID => ID;
    }
}

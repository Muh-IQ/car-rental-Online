using AddChainServiceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Module
{
    public class Company:IEntity
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        //public Vehicle Vehicle { get; set; }

        [Required, MaxLength(100)]
        public string LogoName { get; set; }
        public int _ID => ID;

    }

}

using Data.Module;
using System.ComponentModel.DataAnnotations;

namespace Car_rental_by_EF.DTOs
{
    public class BookingDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Address { get; set; }
        public byte Age { get; set; }

        public string PaymentCardNumber { get; set; }
        public byte Cvv { get; set; }
        public DateTime ExpierDate { get; set; }
        public string CardOwnerName { get; set; }


        public int VehicleID { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }

    }
}

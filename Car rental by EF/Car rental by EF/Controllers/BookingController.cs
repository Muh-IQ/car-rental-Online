using AddChainServiceLibrary;
using Business;
using Car_rental_by_EF.DTOs;
using Data;
using Data.ImplementaionServices;
using Data.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car_rental_by_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(GeneralRepositoryBusiness<Vehicle> GeneralReposASBusiness) : ControllerBase
    {
        [HttpPost]
        [Route("Booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetBooking(BookingDTO dTO)
        {
            DateTime RentalEndDate = Convert.ToDateTime(dTO.RentalEndDate.ToString().Trim());
            DateTime RentalStartDate = Convert.ToDateTime(dTO.RentalStartDate.ToString().Trim());
            // حساب الفرق بين التاريخين
            TimeSpan difference = RentalEndDate - RentalStartDate;

            // تحويل الفرق إلى عدد الأيام
            int InitialRentalDays = difference.Days;


            //update status of availability
            Vehicle vehicle = await GeneralReposASBusiness.GetById(dTO.VehicleID);
            vehicle.IsAvailableForRent = false;
            var res = await GeneralReposASBusiness.Update(vehicle, dTO.VehicleID);
            decimal RentalPricePerDay = vehicle.RentalPricePerDay;
            decimal InitialTotalDueAmount = RentalPricePerDay * InitialRentalDays;
            decimal PaidInitialTotalDueAmount = InitialTotalDueAmount;
            Customer customer = new Customer()
            {
                Address = dTO.Address.ToString().Trim(),
                Age = (byte)dTO.Age,
                Email = dTO.Email.ToString().Trim(),
                DriverLicenseNumber = dTO.DriverLicenseNumber.ToString().Trim(),
                Name = dTO.Name.ToString().Trim(),
                Phone = dTO.Phone.ToString().Trim()
            };
            BankCard bankCard = new BankCard()
            {
                CardOwnerName = dTO.CardOwnerName.ToString().Trim(),
                Cvv = (byte)dTO.Cvv,
                ExpierDate = Convert.ToDateTime(dTO.ExpierDate.ToString().Trim()),
                Number = dTO.PaymentCardNumber.ToString().Trim()
            };

            RentalBooking booking = new RentalBooking()
            {
                InitialRentalDays = InitialRentalDays,
                InitialTotalDueAmount = InitialTotalDueAmount,
                Notes = "",
                RentalEndDate = Convert.ToDateTime(dTO.RentalEndDate.ToString().Trim()),
                RentalStartDate = Convert.ToDateTime(dTO.RentalStartDate.ToString().Trim()),
                RentalPricePerDay = RentalPricePerDay,
                VehicleID = (int)dTO.VehicleID
            };

            RentalTransaction transaction = new RentalTransaction()
            {
                PaymentCardNumber = dTO.PaymentCardNumber.ToString().Trim(),
                PaidInitialTotalDueAmount = PaidInitialTotalDueAmount,
                TransactionDate = DateTime.Now,
                UpdatedTransactionDate = DateTime.Now,

            };

            List<object> data = new List<object>()  
                { customer, bankCard,booking , transaction};


            new AddChainService()
                .SetDbContext(new AppDbContext("Server=.;Database=CarRental_EF_Core;User Id=sa;Password=se123456;TrustServerCertificate=True;"))
                .SetImplementaionDbContext(typeof(ImplementaionAddChainService<>))
                .SetEntities(data)
                .Run();
           


            return Ok(res);
        }
    }
}

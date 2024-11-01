using Business;
using Data.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Car_rental_by_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController(GeneralTransactionRelationRepository_Business<Vehicle> ReposASBusiness , GeneralRepositoryBusiness<Vehicle> GeneralReposASBusiness) : GenericController<Vehicle>(GeneralReposASBusiness)
    {
        [HttpGet]
        [Route("GetAllVehicle/pageIndex={pageIndex}/pageSize={pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVehicle(int pageIndex, int pageSize)
        {
            if (pageIndex < 1 || pageSize < 1)
                return BadRequest("Enter correct data");

            try
            {
                //you can pass "null " replacement 'expression'
                var res = await ReposASBusiness.GetAllByTransaction(pageIndex, pageSize, x => x.IsAvailableForRent == true, ["company", "fule", "category"]);
                if (res == null)
                    return NotFound("Not found vehicle");

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }

        [HttpGet]
        [Route("GetVehicleImage/FileName={FileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicleImage(string FileName)
        {
            if (FileName.Length < 1 )
                return BadRequest("Enter correct Name");

            try
            {
                var uploadDirectory = @"C:\Users\Asus\Desktop\Projects\Car rental\Car rental online\Car rental by EF\Images";
                var filePath = Path.Combine(uploadDirectory, FileName);

                if (!System.IO.File.Exists(filePath))
                    return NotFound("Image not found.");

                var image = System.IO.File.OpenRead(filePath);
                var mimeType = GetMimeType(filePath);

                return File(image, mimeType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }

        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

        [HttpGet]
        [Route("GetCountVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async  Task<IActionResult> GetCountVehicle()
        {
            try
            {
                return Ok(await GeneralReposASBusiness.CountElement(x => x.IsAvailableForRent == true));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }
    }
}

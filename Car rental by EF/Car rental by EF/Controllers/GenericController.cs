using AddChainServiceLibrary;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;


namespace Car_rental_by_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T>(IGeneralRepository<T> Repo) : ControllerBase where T : class, AddChainServiceLibrary.IEntity
    {

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await Repo.GetById(id);
            return Ok(res);
        }


        [HttpGet]
        [Route("GetCountVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetCountVehicle()
        {
            try
            {
                 return Ok(await Repo.CountElement(null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }
    }
}

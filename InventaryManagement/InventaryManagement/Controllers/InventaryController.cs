using Inventary.Core.Domain.VM;
using Inventary.Services.Servies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventaryController : ControllerBase
    {
        private readonly IInventaryService _inventaryService;

        public InventaryController(IInventaryService inventaryService)
        {
            _inventaryService = inventaryService;
        }

        [HttpPost]
        [Route("SaveInventrary")]
        public async Task<IActionResult> SaveInventrary([FromBody] InventaryDetail inventary)
        {
            try
            {
                var data = await _inventaryService.SaveInventrary(inventary);
                if (data == 1)
                    return new OkObjectResult("Inventary is added successfully");
                else
                    return new BadRequestObjectResult("Inventary is not added successfully");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetInventries")]
        public async Task<IActionResult> GetInventries()
        {
            try
            {
                var data = await _inventaryService.GetAllInventrary();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetInventrary")]
        public async Task<IActionResult> GetInventrary(int id)
        {
            try
            {
                var data = await _inventaryService.GetInventrary(id);
                if (data != null)
                    return new OkObjectResult(data);
                else
                    return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("UpdateInventray")]
        public async Task<IActionResult> UpdateInventray([FromBody] InventaryDetail inventaryDetail, int id)
        {

            try
            {
                var data = await _inventaryService.UpdateInventrary(inventaryDetail, id);
                if (data == 1)
                    return new OkObjectResult("Inventary is updated successfully");
                else
                    return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteInventrary")]
        public async Task<IActionResult> DeleteInventrary(int id)
        {
            try
            {
                var data = await _inventaryService.DeleteInventrary(id);
                if (data == 1)
                    return new OkObjectResult("Inventary is deleted successfully");
                else
                    return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

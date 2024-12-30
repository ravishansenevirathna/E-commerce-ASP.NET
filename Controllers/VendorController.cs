using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/vendor")]
    [ApiController]
    [Authorize]
    public class VendorController : ControllerBase
    {
       private readonly IVendorService vendorService;

       public VendorController(IVendorService vendorService)
    {
        this.vendorService = vendorService;
    }  

         [HttpPost("save")]
        public async Task<ActionResult<VendorDto>> SaveVendor(VendorDto vendorDto){

            if (vendorDto == null)
            {
                return BadRequest("Vendor cannot be null.");
            }

            try
            {
                var savedVendor = await vendorService.SaveVendorAsync(vendorDto);
                return CreatedAtAction(nameof(SaveVendor), new { id = savedVendor.Id }, savedVendor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<VendorDto>>> GetVendors(){
            var vendors = await vendorService.GetAllVendorsAsync();
            return Ok(vendors);
        }
    }
}
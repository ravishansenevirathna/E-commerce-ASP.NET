using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
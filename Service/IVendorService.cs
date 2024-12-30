using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;

namespace EcommerceApi.Service
{
    public interface IVendorService
    {
        Task<VendorDto> SaveVendorAsync(VendorDto vendorDto);

        Task<IEnumerable<VendorDto>> GetAllVendorsAsync();
    }
}
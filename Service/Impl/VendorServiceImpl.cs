using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Service.Impl
{
    public class VendorServiceImpl : IVendorService
    {
        private readonly ProductContext productContext;

        public VendorServiceImpl(ProductContext productContext)
        {
            this.productContext = productContext;
        }

            public async Task<VendorDto> SaveVendorAsync(VendorDto vendorDto)
        {
            string newVendorUID = await GenerateVendorUIDAsync();

            Vendor vendor = new Vendor
            {
                Venodr_u_id = newVendorUID,
                Owner_Name = vendorDto.Owner_Name,
                Business_Name = vendorDto.Business_Name,
                Business_Address = vendorDto.Business_Address,
                Bank_Account= vendorDto.Bank_Account,
                Email = vendorDto.Email,
                Nic = vendorDto.Nic,
                Mobile = vendorDto.Mobile,
                Requested_Time = DateTime.Now,
                Admin_Approved = 0,
            };

            await productContext.Vendors.AddAsync(vendor);
           
            await productContext.SaveChangesAsync();

            VendorDto savedVendorDto = new VendorDto
            {
                Venodr_u_id = vendor.Venodr_u_id,
                Owner_Name = vendor.Owner_Name,
                Business_Name = vendor.Business_Name,
                Business_Address = vendor.Business_Address,
                Bank_Account= vendor.Bank_Account,
                Email = vendor.Email,
                Nic = vendor.Nic,
                Mobile = vendor.Mobile,
                Admin_Approved = 0,
         
            };

            return savedVendorDto;
        }

         private async Task<string> GenerateVendorUIDAsync()
        {
            // Fetch the latest vendor with the highest ID
            var lastVendor = await productContext.Vendors
                .OrderByDescending(v => v.Id)
                .FirstOrDefaultAsync();

            int newIdNumber = 1;

            if (lastVendor != null && !string.IsNullOrEmpty(lastVendor.Venodr_u_id))
            {
                string lastId = lastVendor.Venodr_u_id.Substring(3);
                if (int.TryParse(lastId, out int lastIdNumber))
                {
                    newIdNumber = lastIdNumber + 1;
                }
            }

            return $"VEN{newIdNumber:D9}";
        }

            public async Task<IEnumerable<VendorDto>> GetAllVendorsAsync()
        {
            List<Vendor> vendors = await productContext.Vendors.ToListAsync();
            
            List<VendorDto> vendorDtos = vendors.Select(Vendor => new VendorDto
            {
                Venodr_u_id = Vendor.Venodr_u_id,
                Owner_Name = Vendor.Owner_Name,
                Business_Name = Vendor.Business_Name,
                Business_Address = Vendor.Business_Address,
                Bank_Account= Vendor.Bank_Account,
                Email = Vendor.Email,
                Nic = Vendor.Nic,
                Mobile = Vendor.Mobile,
                Admin_Approved = Vendor.Admin_Approved,
                

            }).ToList();

            return vendorDtos;
        }
    }
}
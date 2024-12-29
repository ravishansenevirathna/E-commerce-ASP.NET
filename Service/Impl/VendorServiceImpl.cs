using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;

namespace EcommerceApi.Service.Impl
{
    public class VendorServiceImpl : IVendorService
    {
        private readonly ProductContext productContext;

        public VendorServiceImpl(ProductContext productContext)
        {
            this.productContext = productContext;
        }
    }
}
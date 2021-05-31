using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Microservice.Models;
using Product.Microservice.Context;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Product.Microservice.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<Product.Microservice.Models.Product>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product.Microservice.Models.Product>
        {
            private readonly IApplicationContext _context;
            public GetProductByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }
            public  async Task<Product.Microservice.Models.Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (product == null) return null;
                return product;
            }
        }
    }
}

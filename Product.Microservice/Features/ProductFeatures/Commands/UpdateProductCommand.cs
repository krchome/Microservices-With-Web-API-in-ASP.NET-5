using MediatR;
using Product.Microservice.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Microservice.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateProductCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == command.Id).FirstOrDefault();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.Name = command.Name;
                    product.Price = command.Price;
                    product.Description = command.Description;
                    await _context.SaveChanges();
                    return product.Id;
                }
            }
        }
    }
}

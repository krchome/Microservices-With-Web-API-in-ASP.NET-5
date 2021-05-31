using MassTransit;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Microservice.Consumer
{
    public class ProductConsumer : IConsumer<CustomerProduct>
    {
        public async Task Consume(ConsumeContext<CustomerProduct> context)
        {

            await Task.Run(() => { var obj = context.Message; });
        }
    }
}

using System;
using System.Threading.Tasks;
using CarWashProcessing.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication2;

namespace CarWashProcessing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CarWashProcessingContext _dbContext;
        public OrderController(CarWashProcessingContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<Orders> Create(OrderModerRequest model)
        {
            
            EntityEntry<Orders> order = _dbContext.Orders.Add(new Orders()
            {
                CarNumber = model.CarNumber,
                ClientName = model.ClientName,
                DataPost = DateTime.Now,
                OrderType = (int)model.OrderType

            });
            await _dbContext.SaveChangesAsync();

            return order.Entity;

        }

        [HttpGet]
        public async Task<Orders[]> GetAll()
        {
            Orders[] orders = await _dbContext.Orders.ToArrayAsync();
            return orders;
        }
    }

    public class OrderModerRequest
    {
        public OrderEnum OrderType { get; set; }
        public string CarNumber { get; set; }
        public string ClientName { get; set; }
    }

    public enum OrderEnum
    {
        Type1 = 1,
        Type2 = 2,
        Type3 = 3,
        Type4 = 4,
    }

   

    
}
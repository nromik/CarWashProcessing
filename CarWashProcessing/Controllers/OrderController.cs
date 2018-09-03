using System;
using System.Linq;
using System.Threading.Tasks;
using CarWashProcessing.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
                OrderTypeId = (int)model.OrderType
            });
            var task = await _dbContext.vw_OrderTasks.Where(o => o.OrderTypeId == order.Entity.OrderTypeId)
                .ToArrayAsync();

            order.Entity.Price = task.Sum(t => t.Price);

            await _dbContext.SaveChangesAsync();
            


            return order.Entity;

        }

        [HttpGet]
        public async Task<vw_OrderTask[]> GetAll(int orderTypeId)
        {
            var orders = await _dbContext.vw_OrderTasks.Where(o => o.OrderTypeId == orderTypeId).ToArrayAsync();
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
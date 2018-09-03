using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashProcessing.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyNamespace;

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
            var orderTasks = await _dbContext.vw_OrderTasks.Where(o => o.OrderTypeId == order.Entity.OrderTypeId)
                .ToArrayAsync();

            order.Entity.Price = orderTasks.Sum(t => t.Price);

            await _dbContext.SaveChangesAsync();

            order.Entity.StartTime = DateTime.Now;
            
            Client client = new Client("http://localhost:62428");
            var tasks = new List<Task<bool>>();
            foreach (var orderTask in orderTasks)
            {
                TaskInfo taskInfo = new TaskInfo
                {
                    NeedWorker = orderTask.NeedWorker,
                    Duration = orderTask.Duration,
                };
                tasks.Add(client.ApiTaskPostAsync(orderTask.TaskId, taskInfo));
            }

            await Task.WhenAll(tasks);
            order.Entity.EndTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return order.Entity;
        }

        [HttpGet]
        [Route("orderTypes")]
        public async Task<vw_OrderTask[]> GetAll(int? orderTypeId)
        {

            var orders = _dbContext.vw_OrderTasks.Where(o => true);
            if (orderTypeId != null)
                orders = orders.Where(o => o.OrderTypeId == orderTypeId);

            return await orders.ToArrayAsync();
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
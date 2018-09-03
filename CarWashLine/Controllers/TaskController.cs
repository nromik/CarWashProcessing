using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWashLine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static int _countWorker = 1;

        static SemaphoreSlim sem = new SemaphoreSlim(_countWorker);
        
        [HttpPost]
        public async Task<bool> Run(int taskId, [FromBody] TaskInfo info)
        {
            if (info.NeedWorker)
            {
                try
                {
                    await sem.WaitAsync();
                    await RumTask(info);
                }
                finally
                {
                    sem.Release();
                }
            }
            else
            {
                await RumTask(info);
            }

            return true;
        }

        private async  Task RumTask(TaskInfo info)
        {
            await Task.Delay(info.Duration);
        }
    }

    public class TaskInfo
    {
        public bool NeedWorker { get; set; }

        public int Duration { get; set; }
    }
}
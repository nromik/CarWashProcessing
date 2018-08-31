using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class TaskType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool NeedWorker { get; set; }
        public decimal Price { get; set; }
        public int TimeWork { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CarWashProcessing.DataModels
{
    public partial class Orders
    {
        public int Id { get; set; }
        public DateTime DataPost { get; set; }
        public string ClientName { get; set; }
        public string CarNumber { get; set; }
        public int OrderTypeId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsActive { get; set; }
    }
}

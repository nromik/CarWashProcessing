using System;

namespace CarWashProcessing.DataModel
{
    public partial class Orders
    {
        public int Id { get; set; }
        public DateTime DataPost { get; set; }
        public string ClientName { get; set; }
        public string CarNumber { get; set; }
        public int OrderType { get; set; }
    }
}

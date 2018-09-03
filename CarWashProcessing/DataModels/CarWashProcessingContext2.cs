using Microsoft.EntityFrameworkCore;

namespace CarWashProcessing.DataModels
{
    public partial class CarWashProcessingContext : DbContext
    {
        protected void OnModelCreatingViews(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<vw_OrderTask>().ToView("vw_OrderTask");
        }

        public DbQuery<vw_OrderTask> vw_OrderTasks { get; set; }
    }




    public class vw_OrderTask
    {
        public int OrderTypeId{get; set;}
        public string OrderName{get; set;}
        public string OrderDescription{get; set;}
        public int TaskId{get; set;}
        public string TaskName{get; set;}
        public bool NeedWorker{get; set;}
        public decimal Price{get; set;}
        public int Duration{get; set;}
    }
}
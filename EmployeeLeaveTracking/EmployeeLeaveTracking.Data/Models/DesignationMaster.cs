namespace EmployeeLeaveTracking.Data.Models
{
    public class DesignationMaster
    {
        public int Id { get; set; }

        public string? DesignationName { get; set; }


        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;



        public List<User>? Users { get; set; }

        public DesignationMaster()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
namespace Advice.Common.Models
{
    public class DepartmentModel
    {
        public DepartmentModel(long departmentId, string description)
        {
            DepartmentId = departmentId;
            Description = description;
        }

        public long DepartmentId { get; private set; }
        public string Description { get; private set; }
    }
}

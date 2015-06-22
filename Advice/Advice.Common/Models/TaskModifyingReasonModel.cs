namespace Advice.Common.Models
{
    public class TaskModifyingReasonModel
    {
        public TaskModifyingReasonModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}

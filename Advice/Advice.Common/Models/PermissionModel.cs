namespace Advice.Common.Models
{
    public class PermissionModel
    {
        public PermissionModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}

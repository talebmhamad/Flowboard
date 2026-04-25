

namespace Flowboard.Application.DTOs
{
    public class WorkflowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string GroupName { get; set; }
        public bool EnableTaskAssignment { get; set; }
        public bool EnableTaskDelegation { get; set; }
    }
}

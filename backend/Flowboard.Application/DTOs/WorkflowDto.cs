

namespace Flowboard.Application.DTOs
{
    public class WorkflowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public bool EnableTaskAssignment { get; set; }
        public bool EnableTaskDelegation { get; set; }
    }
}

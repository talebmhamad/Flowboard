using Intalio.Case.Core.Objects;
using Intalio.Case.Core.Templates;
using Intalio.Core;

namespace Flowboard.Intalio.Activities
{
    public class CASetAssignedUser : ActivityTemplate
    {
        public override void Execute(WorkflowItem workflowItem)
        {
            try
            {
                var assignedUser = 
                    workflowItem.Properties["assignedUser"]?.Value;

                Property assignedUserProp = workflowItem!.Properties.FirstOrDefault(x => x.Name == "assignedUser")!;

                assignedUserProp!.SetValue(
                    workflowItem.ActivityInstance.ActivityInstanceId,
                    assignedUser!.ToString()
                );
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, null, null, LoggingLevel.Error);
                throw;
            }
        }

        public override void Complete(WorkflowItem workflowItem)
        {
        }

    }
}
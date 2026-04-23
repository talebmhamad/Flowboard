namespace Flowboard.Application.DTOs
{
   public class UserSummaryDto
    {
        public int Draft { get; set; }
        public int Inbox { get; set; }
        public int Completed { get; set; }
        public int MyRequests { get; set; }
        public int Closed { get; set; }
    }

}

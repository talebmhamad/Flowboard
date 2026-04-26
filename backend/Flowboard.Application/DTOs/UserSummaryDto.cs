namespace Flowboard.Application.DTOs
{
   public class UserSummaryDto
    {
        public CountDto Draft { get; set; }
        public CountDto Inbox { get; set; }
        public CountDto Completed { get; set; }
        public CountDto MyRequests { get; set; }
        public CountDto Closed { get; set; }
    }

}

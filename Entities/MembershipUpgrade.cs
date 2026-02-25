namespace Career_Path.Entities
{
    public class MembershipUpgrade
    {
        public string Id { get; set; } = Guid.CreateVersion7().ToString();

        public string UserId { get; set; }
        public ApplicationUser User { get; set; } 
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public string? Note { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
    public enum RequestStatus
    {
        Pending,  
        Approved, 
        Rejected  
    }
}

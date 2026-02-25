namespace Career_Path.Entities
{
    public class PrompetRoadMap
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

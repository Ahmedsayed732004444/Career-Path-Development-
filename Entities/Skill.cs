namespace Career_Path.Entities
{
    public class Skill
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}

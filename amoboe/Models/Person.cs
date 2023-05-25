namespace amoboe.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }

    }
}

namespace amoboe.ViewModels
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public IFormFile? Photo { get; set; }
    }
}

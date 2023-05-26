namespace amoboe.ViewModels.Account
{
    public class UpdateEmployeeVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PositionId { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}

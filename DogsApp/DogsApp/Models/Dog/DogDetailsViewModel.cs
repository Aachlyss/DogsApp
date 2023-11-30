using System.ComponentModel.DataAnnotations;

namespace DogsApp.Models.Dog
{
    public class DogDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age {  get; set; }
        public string BreedName { get; set; } = null!;
        public string? Picture { get; set; }
        [Display(Name = "Owner Name")]
        public string FullName { get; set; } = null!;
    }
}

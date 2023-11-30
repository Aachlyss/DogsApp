﻿using DogsApp.Models.Breed;
using System.ComponentModel.DataAnnotations;

namespace DogsApp.Models.Dog
{
    public class DogAllViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Breed")]
        public string BreedName { get; set; } = null!;

        [Display(Name = "Dog Picture")]
        public string? Picture { get; set; }

        public virtual List<BreedPairViewModel> Breeds { get; set; } = new List<BreedPairViewModel>();
        [Display(Name = "Owner Name")]
        public string FullName { get; set; } = null!;
    }
}

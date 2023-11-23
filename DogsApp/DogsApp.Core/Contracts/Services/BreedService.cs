using DogsApp.Data;
using DogsApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsApp.Core.Contracts.Services
{
    public class BreedService : IBreedService
    {
        private readonly ApplicationDbContext _context;
        public BreedService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Breed GetBreedById(int breedId)
        {
            return _context.Breeds.Find(breedId);
        }

        public List<Breed> GetBreeds()
        {
            List<Breed> breeds = _context.Breeds.ToList();
            return breeds;
        }

        public List<Dog> GetDogsByBreed(int breedId)
        {
            return _context.Dogs.Where(x => x.BreedId == breedId).ToList();
        }

        public List<Dog> GetDogsById(int breedId)
        {
            return _context.Dogs
                .Where(x=>x.BreedId == breedId)
                .ToList();
        }
    }
}

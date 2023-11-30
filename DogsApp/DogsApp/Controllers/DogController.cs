using DogsApp.Core.Contracts;
using DogsApp.Data;
using DogsApp.Infrastructure.Data.Domain;
using DogsApp.Models.Breed;
using DogsApp.Models.Dog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogsApp.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        private readonly IDogService _dogService;
        private readonly IBreedService _breedService;
        public DogController(IDogService dogService, IBreedService breedService)
        {
            this._dogService = dogService;
            this._breedService = breedService;
        }
        // GET: DogController
        [AllowAnonymous]
        public ActionResult Index(string searchStringBreed, string searchStringName)
        {
            List<DogAllViewModel> dogs = _dogService.GetDogs(searchStringBreed, searchStringName)
                .Select(dogFromDb => new DogAllViewModel
                {
                    Id = dogFromDb.Id,
                    Name = dogFromDb.Name,
                    Age = dogFromDb.Age,
                    BreedName = dogFromDb.Breed.Name,
                    Picture = dogFromDb.Picture,
                    FullName = dogFromDb.Owner.FirstName + " " + dogFromDb.Owner.LastName
                }).ToList();
            //if (!String.IsNullOrEmpty(searchStringBreed) && !String.IsNullOrEmpty(searchStringName))
            //{
            //    dogs = dogs.Where(d=>d.Breed.Contains(searchStringBreed) && d.Name.Contains(searchStringName)).ToList();
            //}
            //else if (!String.IsNullOrEmpty(searchStringBreed))
            //{
            //    dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed)).ToList();
            //}
            //else if (!String.IsNullOrEmpty(searchStringName))
            //{
            //    dogs = dogs.Where(d => d.Name.Contains(searchStringName)).ToList();

            //}
            return View(dogs);
        }
        //djagaradjugara
        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }

            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName=item.Breed.Name,
                Picture = item.Picture,
                FullName = item.Owner.FirstName + " " +item.Owner.LastName
            };
            return View(dog);
        }

        // GET: DogController/Create
        public IActionResult Create()
        {
            var dog = new DogCreateViewModel();
            dog.Breeds = _breedService.GetBreeds()
                .Select(c => new BreedPairViewModel()
                { 
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
            return View(dog);
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] DogCreateViewModel dog)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var created = _dogService.Create(dog.Name, dog.Age, dog.BreedName, dog.Picture, currentUserId);
                if (created)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        public IActionResult Success()
        {
            return this.View();
        }

        // GET: DogController/Edit/5
        public IActionResult Edit(int id)
        {
            
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogEditViewModel dog = new DogEditViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedId = item.BreedId,
                Picture = item.Picture,
            };
            dog.Breeds = _breedService.GetBreeds()
                .Select(c => new BreedPairViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DogEditViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _dogService.UpdateDog(id, bindingModel.Name, bindingModel.Age, bindingModel.BreedId, bindingModel.Picture);
                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(bindingModel);
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture
            };
            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _dogService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Index", "Dog");
            }
            else
            {
                return View();
            }
        }
    }
}

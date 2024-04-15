using Microsoft.AspNetCore.Mvc;

namespace apbd19_cwiczenia5;

[Route("api/animals")]
[ApiController]
public class AnimalController : ControllerBase
{
   private static readonly List<Animal> animals = new()
   {
      new Animal { Id = 1, Name = "Dog", Category = "Mammal", Age = 3, Color = "Brown" },
      new Animal { Id = 2, Name = "Cat", Category = "Mammal", Age = 5, Color = "White" },
      new Animal { Id = 3, Name = "Bird", Category = "Bird", Age = 2, Color = "Yellow" },
      new Animal { Id = 4, Name = "Fish", Category = "Aquatic", Age = 1, Color = "Blue" },
      new Animal { Id = 5, Name = "Rabbit", Category = "Mammal", Age = 4, Color = "Gray" }
   };

   private static readonly List<Visit> visits = new()
   {
      new Visit { Id = 1, VisitDate = DateTime.Now.AddDays(-7), Description = "Vaccination", Price = 50, AnimalId = 1 },
      new Visit { Id = 2, VisitDate = DateTime.Now.AddDays(-2), Description = "Checkup", Price = 30, AnimalId = 2 }
   };


   [HttpGet]
   public IActionResult GetAnimals()
   {
      return Ok(animals);
   }

   [HttpGet("{id}")]
   public ActionResult<Animal> GetAnimalById(int id)
   {
      var animal = animals.FirstOrDefault(a => a.Id == id);
      if (animal == null)
      {
         return NotFound();
      }

      return animal;
   }

   [HttpPost]
   public void AddAnimal(Animal animal)
   {
      int newId = animals.Max(a => a.Id) + 1;
      animal.Id = newId;

      animals.Add(animal);
   }

   [HttpPut("{id}")]
   public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
   {
      var existingAnimal = animals.FirstOrDefault(a => a.Id == id);
      if (existingAnimal == null)
      {
         return NotFound();
      }

      existingAnimal.Name = updatedAnimal.Name;
      existingAnimal.Category = updatedAnimal.Category;
      existingAnimal.Age = updatedAnimal.Age;
      existingAnimal.Color = updatedAnimal.Color;

      return NoContent();
   }

   [HttpDelete("{id}")]
   public IActionResult DeleteAnimal(int id)
   {
      var existingAnimal = animals.FirstOrDefault(a => a.Id == id);
      if (existingAnimal == null)
      {
         return NotFound();
      }

      animals.Remove(existingAnimal);
      return NoContent();
   }

   [HttpGet("animal/{animalId}")]
   public ActionResult<IEnumerable<Visit>> GetVisitsByAnimalId(int animalId)
   {
      var visitsForAnimal = visits.Where(v => v.AnimalId == animalId).ToList();
      if (visitsForAnimal.Count == 0)
      {
         return NotFound();
      }

      return Ok(visitsForAnimal);
   }

   [HttpPost("animal/{animalId}")]
   public void AddVisitForAnimal(int animalId, Visit newVisit)
   {
      int newId = visits.Max(v => v.Id) + 1;
      newVisit.Id = newId;
      newVisit.AnimalId = animalId;

      visits.Add(newVisit);
   }
}



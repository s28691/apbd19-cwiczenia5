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
public ActionResult<Animal> AddAnimal(Animal animal)
{
   int newId = animals.Max(a => a.Id) + 1;
   animal.Id = newId;

   animals.Add(animal);
   return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
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
}

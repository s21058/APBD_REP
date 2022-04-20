using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;


namespace WebApplication_Animals.Controllers;

[ApiController]
[Route("/animals")]
public class AnimalsContoller : ControllerBase
{
    private static string conString = "Data Source=DESKTOP-8HK6M8L\\SQLEXPRESS;Initial Catalog=master;Integrated Security=true";
    private AnimalsDb _animalsDb = new AnimalsDb(conString);
    [HttpGet]
    public IActionResult getAnimalsBy(string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            orderBy="name";
        }
        
        return Ok(AnimalsDb.getBy(orderBy));
    }

    [HttpPost]
    public ObjectResult addNewAnimal(Animals animals)
    {
        if (string.IsNullOrWhiteSpace(animals.idAnimal.ToString()) || string.IsNullOrWhiteSpace(animals.name) ||
            string.IsNullOrWhiteSpace(animals.description) || string.IsNullOrWhiteSpace(animals.category)
            || string.IsNullOrWhiteSpace(animals.area))
        {

            return StatusCode((int) HttpStatusCode.BadRequest, "Some of values were empty");
        }
        AnimalsDb.Add(animals);

        return StatusCode((int) HttpStatusCode.OK, "Data was input");
    }

    [HttpPut("{idAnimal}")]
    public ObjectResult updateAnimal(int idAnimal, Animals animals)
    {
        if (idAnimal != animals.idAnimal)
        {
            return StatusCode((int) HttpStatusCode.BadRequest,
                "Were providing two different indexes " + idAnimal + " and " + animals.idAnimal);
        }
        _animalsDb.Update(animals,idAnimal);
        return StatusCode((int) HttpStatusCode.OK, "Data was updated");
    }

    [HttpDelete("{idAnimal}")]
    public ObjectResult deleteAnimal(int idAnimal)
    {
        AnimalsDb.Delete(idAnimal);
        return StatusCode((int) HttpStatusCode.OK, "Animal with Id "+idAnimal+" was deleted");
    }
}

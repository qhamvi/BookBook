// using Contracts;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace BookBook.API.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class WeatherForecastController : ControllerBase
// {
//     private IRepositoryWrapper _repositoryWrapper;
//     public WeatherForecastController(IRepositoryWrapper repositoryWrapper)
//     {
//         _repositoryWrapper = repositoryWrapper;
//     }

//     [HttpGet]
//     public IEnumerable<string> Get()
//     {
//         var queryAuthor = _repositoryWrapper.Author.FindAll(false);
//         var authors = queryAuthor.ToList();
//         var res = authors.Select(v => v.LastName).ToArray();
//         return res;
//     }
// }

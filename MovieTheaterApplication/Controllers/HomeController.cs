using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Repositories;

namespace MovieTheaterApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public HomeController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public async Task<IActionResult> Index()
        {
            // Might want to narrow to showings to today or add range somehow
            var movies = await _movieRepository.GetMovies();


            return View(movies);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

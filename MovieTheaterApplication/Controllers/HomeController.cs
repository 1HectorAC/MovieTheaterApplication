using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Repositories;
using MovieTheaterApplication.Repositories.Implementations;

namespace MovieTheaterApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IShowingRepository _showingRepository;

        public HomeController(IMovieRepository movieRepository, IShowingRepository showingRepository)
        {
            _movieRepository = movieRepository;
            _showingRepository = showingRepository;
        }


        public async Task<IActionResult> Index()
        {
            // Might want to narrow to showings to today or add range somehow
            var movies = await _movieRepository.GetMovies();


            return View(movies);
        }

        public async Task<IActionResult> ShowingSelection(int MovieId)
        {
            var showings = await _showingRepository.GetShowingsByMovieId(MovieId);

            return View(showings);
        }

        public async Task<IActionResult> SeatSelection(int ShowingId)
        {
            // Get seats grouped by 
            var showingSeats = await _showingRepository.GetShowingSeatsByShowingId(ShowingId);
            var rowGroupedShowingSeats = showingSeats.GroupBy(i => i.Seat!.Row);



            return View(rowGroupedShowingSeats);
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

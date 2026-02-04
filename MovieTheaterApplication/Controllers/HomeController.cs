using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Models.ViewModels;
using MovieTheaterApplication.Repositories;
using MovieTheaterApplication.Repositories.Implementations;

namespace MovieTheaterApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IShowingRepository _showingRepository;
        private readonly IMovieTheaterRepository _movieTheaterRepository;

        public HomeController(IMovieRepository movieRepository, IShowingRepository showingRepository, IMovieTheaterRepository movieTheaterRepository)
        {
            _movieRepository = movieRepository;
            _showingRepository = showingRepository;
            _movieTheaterRepository = movieTheaterRepository;
        }


        public async Task<IActionResult> Index()
        {
            // Might want to narrow to showings to today or add range somehow
            var movies = await _movieTheaterRepository.GetMovies();


            return View(movies);
        }

        public async Task<IActionResult> ShowingSelection(int MovieId)
        {
            var showings = await _movieTheaterRepository.GetShowingsByMovieId(MovieId);

            return View(showings);
        }

        public async Task<IActionResult> SeatSelection(int ShowingId)
        {
            // Get seats grouped by 
            var seats = await _movieTheaterRepository.GetSeatsByShowingId(ShowingId);
          
            var seatIdsForTickets = await _movieTheaterRepository.GetSeatIdsOfTicketsByShowingId(ShowingId);

            var formatedSeats = seats.Select(i => new SeatSelectionViewModel { SeatId = i.Id, Column = i.Column, Row = i.Row, IsSelected = seatIdsForTickets.Contains(i.Id)});

            var groupedFormatedSeats = formatedSeats.GroupBy(i => i.Row);


            return View(groupedFormatedSeats);
        }

        [HttpPost]
        public async Task<IActionResult> SelectShowingSeat(int[] seatsId)
        {
            //check if each seat is available and change isAvailable

            //make ticket with reference to showingSeat

            //if success: pass to confirmation page
            // if fail: return to SeatSelection?
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryApplication.Models;
using LibraryApplication.ViewModels;

namespace LibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository; //readonly prevents accidental re-assign value with other methods

        //Allows the HomeController to access the IBookRepository(needs instance in startup not here)(4)
        //Use only 1 instance of HomeController... Couple more functions with ","
        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            //_bookRepository = new MockBookRepository();    <----BAD CODE. use Startup.

        }

        //Allows the HomeController to access the IBookRepository(needs instance in startup not here)(4)

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Browse()
        {
            var model = _bookRepository.GetAllBooks();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                Book newBook = _bookRepository.Add(book);
                return RedirectToAction("details", new { id = newBook.Id });
            }
            return View();
        }


        public IActionResult Details(int? id)
        {
            //displays details of selected book(6)
                /*Doesn't make sence to have this information here. Create ViewModel to store 
                data the Detials Page will need.
                Book model = _bookRepository.GetBook(1);
                ViewBag.PageTitle = _bookRepository.GetBook(1).Title;   (8)*/
            //View(model) allows us to retreive model data in razor page by the object instance
                //return View(model); (8)

            //Pulls this data from HomeDetailsViewModel
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Book = _bookRepository.GetBook(id??1),
                PageTitle = _bookRepository.GetBook(id??1).Title
            };
            return View(homeDetailsViewModel);
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

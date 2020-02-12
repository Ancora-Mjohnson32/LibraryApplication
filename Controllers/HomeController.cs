using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryApplication.Models;
using LibraryApplication.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository; //readonly prevents accidental re-assign value with other methods
        private readonly IHostingEnvironment hostingEnvironment;

        //Allows the HomeController to access the IBookRepository(needs instance in startup not here)(4)
        //Use only 1 instance of HomeController... Couple more functions with ","
        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository,
                              IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            this.hostingEnvironment = hostingEnvironment;
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
        public IActionResult AddBook(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Book newBook = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    SubGenre = model.SubGenre,
                    PageCount = model.PageCount,
                    Series = model.Series,
                    BookNumber = model.BookNumber,
                    Overview = model.Overview,
                    PhotoPath = uniqueFileName
                };

                _bookRepository.Add(newBook);
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

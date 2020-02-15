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
using Microsoft.AspNetCore.Authorization;

namespace LibraryApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository; //readonly prevents accidental re-assign value with other methods
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        //Allows the HomeController to access the IBookRepository(needs instance in startup not here)(4)
        //Use only 1 instance of HomeController... Couple more functions with ","
        public HomeController(IBookRepository bookRepository,
                              IHostingEnvironment hostingEnvironment, ILogger<HomeController> logger)
        {
            _bookRepository = bookRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            //_bookRepository = new MockBookRepository();    <----BAD CODE. use Startup.

        }

        //Allows the HomeController to access the IBookRepository(needs instance in startup not here)(4)

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
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
                string uniqueFileName = ProcessUploadedFile(model);

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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book book = _bookRepository.GetBook(id);
            BookEditViewModel bookEditViewModel = new BookEditViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                SubGenre = book.SubGenre,
                PageCount = book.PageCount,
                Series = book.Series,
                BookNumber = book.BookNumber,
                Overview = book.Overview,
                ExistingPhotoPath = book.PhotoPath
            };
            return View(bookEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = _bookRepository.GetBook(model.Id);
                book.Title = model.Title;
                book.Author = model.Author;
                book.Genre = model.Genre;
                book.SubGenre = model.SubGenre;
                book.PageCount = model.PageCount;
                book.Series = model.Series;
                book.BookNumber = model.BookNumber;
                book.Overview = model.Overview;
                if(model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                   book.PhotoPath = ProcessUploadedFile(model);

                }

                _bookRepository.Update(book);
                return RedirectToAction("browse");
            }
            return View();
        }

        private string ProcessUploadedFile(BookCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);

                }
            }

            return uniqueFileName;
        }

        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            //throw new Exception("Error in Details View");
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            //displays details of selected book(6)
            /*Doesn't make sence to have this information here. Create ViewModel to store 
            data the Detials Page will need.
            Book model = _bookRepository.GetBook(1);
            ViewBag.PageTitle = _bookRepository.GetBook(1).Title;   (8)*/
            //View(model) allows us to retreive model data in razor page by the object instance
            //return View(model); (8)
            Book book = _bookRepository.GetBook(id.Value);
            if (book == null)
            {
                Response.StatusCode = 404;
                return View("BookNotFound", id.Value);
            }

            //Pulls this data from HomeDetailsViewModel
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Book = book,
                PageTitle = _bookRepository.GetBook(id??1).Title
            };
            return View(homeDetailsViewModel);
        }

        [AllowAnonymous]
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

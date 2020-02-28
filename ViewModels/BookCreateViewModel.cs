using LibraryApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.ViewModels
{
    public class BookCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public Genres? Genre { get; set; }
        [Required]
        [Display(Name = "Page Count")]
        public int PageCount { get; set; }
        public string Series { get; set; }
        [Display(Name = "Book Number")]
        public int BookNumber { get; set; }
        [Required]
        public string Overview { get; set; }
        public IFormFile Photo { get; set; }
        public bool Available { get; set; }
        public string RentalUserId { get; set; }

    }
}

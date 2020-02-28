using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    public class Book
    {
        //All book info to be in a table, information available to view.(1)
        public int Id { get; set; }
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
        public string PhotoPath { get; set; }


        [Required]
        public bool Available { get; set; }
        public string RentalUserId { get; set; }
    }
}

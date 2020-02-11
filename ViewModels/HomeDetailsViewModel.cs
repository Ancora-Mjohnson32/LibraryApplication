using LibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.ViewModels
{//Create a 'View Model' when a Model object does not contain all the data a view needs
    public class HomeDetailsViewModel
    {
        //Cleans up our Details method in HomeController(8)
        public Book Book { get; set; }
        public string PageTitle { get; set; }
    }
}

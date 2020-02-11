using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    public enum Genres
    {
        Fiction,
        Fantasy,
        Classics,
        Adventure,
        Novels,
        Literature,
        [Display(Name="Fantasy > Magic")]
        FantasyMagic,
        [Display(Name = "Fantasy > High Fantasy")]
        FantasyHighFantasy,
        [Display(Name = "Fantasy > Dragons")]
        FantasyDragons,
        [Display(Name = "Fantasy > Epic Fantasy")]
        FantasyEpicFantasy,
        [Display(Name = "Science Fiction Fantasy")]
        ScienceFictionFantasy,
        [Display(Name = "Young Adult")]
        YoungAdult,
        [Display(Name = "Young Adult > Young Adult Fantasy")]
        YoungAdultYoungAdultFantasy,
        Childrens,
        [Display(Name = "Childrens > Middle Grade")]
        ChildrensMiddleGrade
    }
}

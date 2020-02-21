using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    
    public enum Genres
    {
        None = 0,
        Fiction = 1 << 0,
        Fantasy = 1 << 1,
        Classics = 1 << 2,
        Adventure = 1 << 3,
        Novels = 1 << 4,
        Literature = 1 << 5,
        [Display(Name = "Fantasy > Magic")]
        FantasyMagic = 1 << 6,
        [Display(Name = "Fantasy > High Fantasy")]
        FantasyHighFantasy = 1 << 7,
        [Display(Name = "Fantasy > Dragons")]
        FantasyDragons = 1 << 8,
        [Display(Name = "Fantasy > Epic Fantasy")]
        FantasyEpicFantasy = 1 << 9,
        [Display(Name = "Science Fiction Fantasy")]
        ScienceFictionFantasy = 1 << 10,
        [Display(Name = "Young Adult")]
        YoungAdult = 1 << 11,
        [Display(Name = "Young Adult > Young Adult Fantasy")]
        YoungAdultYoungAdultFantasy = 1 << 12,
        Childrens = 1 << 13,
        [Display(Name = "Childrens > Middle Grade")]
        ChildrensMiddleGrade = 1 << 14
    }   

    /*public class Genres
    {
        public static readonly Genres Fiction = new Genres();
        public static readonly Genres Fantasy = new Genres();
        public static readonly Genres Classics = new Genres();
        public static readonly Genres Adventure = new Genres();
        public static readonly Genres Novels = new Genres();
        public static readonly Genres Literature = new Genres();
        [Display(Name = "Fantasy > Magic")]
        public static readonly Genres Fantasy_Magic = new Genres();
        [Display(Name = "Fantasy > High Fantasy")]
        public static readonly Genres Fantasy_High_Fantasy = new Genres();
        [Display(Name = "Fantasy > Dragons")]
        public static readonly Genres Fantasy_Dragons = new Genres();
        [Display(Name = "Fantasy > Epic Fantasy")]
        public static readonly Genres Fantasy_Epic_Fantasy = new Genres();
        [Display(Name = "Science Fiction Fantasy")]
        public static readonly Genres Science_Fiction_Fantasy = new Genres();
        [Display(Name = "Young Adult")]
        public static readonly Genres Young_Adult = new Genres();
        [Display(Name = "Young Adult > Young Adult Fantasy")]
        public static readonly Genres Young_Adult_Young_Adult_Fantasy = new Genres();
        public static readonly Genres Childrens = new Genres();
        [Display(Name = "Childrens > Middle Grade")]
        public static readonly Genres Childrens_Middle_Grade = new Genres();
    } */




    /* public class EnumModel
     {
         public Genres Sub { get; set; }
         public bool IsSelected { get; set; }
     }*/
}
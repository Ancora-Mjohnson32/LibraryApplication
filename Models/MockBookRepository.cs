﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    //using : IBookRepository allows to use dependency injection
    public class MockBookRepository : IBookRepository /*MockEmployeeRepository*/
    {//MockBookRepository pulls info from IBookRepository to use methods (3)
        private List<Book> _bookList;

        public MockBookRepository()
        {
            _bookList = new List<Book>()
            {
                new Book() { Id = 1, 
                    Title = "The Two Towers", 
                    Author = "J.R.R. Tolkien", 
                    Genre = Genres.Fiction, 
                    PageCount = 322, 
                    Series = "The Lord of the Rings", 
                    BookNumber = 2, 
                    Overview = "The Fellowship was scattered. Some were bracing hopelessly for war against the ancient evil of Sauron. Some were contending with the treachery of the wizard Saruman. Only Frodo and Sam were left to take the accursed Ring of Power to be destroyed in Mordor–the dark Kingdom where Sauron was supreme. Their guide was Gollum, deceitful and lust-filled, slave to the corruption of the Ring. Thus continues the magnificent, bestselling tale of adventure begun in The Fellowship of the Ring, which reaches its soul-stirring climax in The Return of the King."},
                new Book() { Id = 2, 
                    Title = "Eldest", 
                    Author = "Christopher Paolini", 
                    Genre = Genres.Fantasy, 
                    PageCount = 704, 
                    Series = "The Inheritance Cycle", 
                    BookNumber = 2, 
                    Overview = "Eragon and his dragon, Saphira, have just saved the rebel state from destruction by the mighty forces of King Galbatorix, cruel ruler of the Empire. Now Eragon must travel to Ellesmera, land of the elves, for further training in the skills of the Dragon Rider: magic and swordsmanship. Soon he is on the journey of a lifetime, his eyes open to awe-inspring new places and people, his days filled with fresh adventure. But chaos and betrayal plague him at every turn, and nothing is what it seems. Before long, Eragon doesn't know whom he can trust."},
                new Book() { Id = 3, 
                    Title = "Harry Potter and the Prisoner of Azkaban", 
                    Author = "J.K. Rowling", 
                    Genre = Genres.Fantasy, 
                    PageCount = 435, 
                    Series = "Harry Potter", 
                    BookNumber = 3, 
                    Overview = "Harry Potter's third year at Hogwarts is full of new dangers. A convicted murderer, Sirius Black, has broken out of Azkaban prison, and it seems he's after Harry. Now Hogwarts is being patrolled by the dementors, the Azkaban guards who are hunting Sirius. But Harry can't imagine that Sirius or, for that matter, the evil Lord Voldemort could be more frightening than the dementors themselves, who have the terrible power to fill anyone they come across with aching loneliness and despair. Meanwhile, life continues as usual at Hogwarts. A top-of-the-line broom takes Harry's success at Quidditch, the sport of the Wizarding world, to new heights. A cute fourth-year student catches his eye. And he becomes close with the new Defense of the Dark Arts teacher, who was a childhood friend of his father. Yet despite the relative safety of life at Hogwarts and the best efforts of the dementors, the threat of Sirius Black grows ever closer. But if Harry has learned anything from his education in wizardry, it is that things are often not what they seem. Tragic revelations, heartwarming surprises, and high-stakes magical adventures await the boy wizard in this funny and poignant third installment of the beloved series."}
                /*new Book() { Id = ,
                    Title = "",
                    Author = "",
                    Genre = "",
                    PageCount = 0,
                    Series = "",
                    BookNumber = 0,
                    Overview = ""}*/

            };/*goodreads.com (book info)*/
        }

        public Book Add(Book book)
        {
            //Adds book, increments ID.
            book.Id = _bookList.Max(b => b.Id) + 1;
            _bookList.Add(book);
            return book;
        }

        public Book Delete(int id)
        {
            Book book = _bookList.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _bookList.Remove(book);
            }
            return book;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookList;
        }

        public Book GetBook(int Id)
        {
            //returns Id that matches incoming Id
            return _bookList.FirstOrDefault(b => b.Id == Id);//e => e.Id == Id
        }

        public Book Update(Book bookChanges)
        {
            Book book = _bookList.FirstOrDefault(b => b.Id == bookChanges.Id);
            if (book != null)
            {
               book.Title = bookChanges.Title;
                book.Author = bookChanges.Author;
                book.Genre = bookChanges.Genre;
                book.PageCount = bookChanges.PageCount;
                book.Series = bookChanges.Series;
                book.BookNumber = bookChanges.BookNumber;
                book.Overview = bookChanges.Overview;
            }
            return book;            
        }
    }
}


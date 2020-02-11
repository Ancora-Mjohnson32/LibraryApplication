using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    public interface IBookRepository/*IEmployeeRepository*/
    {//interface to retrieve book details(2)
        Book GetBook(int Id);
        //interface to get a list of books (9)
        IEnumerable<Book> GetAllBooks();
        Book Add(Book book);
        Book Update(Book bookChanges);
        Book Delete(int id);
    }
}

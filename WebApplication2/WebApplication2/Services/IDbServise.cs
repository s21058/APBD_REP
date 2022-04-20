using WebApplication2.Models;

namespace WebApplication2.Services;

public interface IDbServise
{
    IList<Book> getBookList();
    
}
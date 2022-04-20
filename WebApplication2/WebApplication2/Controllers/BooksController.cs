using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

public class BooksController
{
    private readonly IDbServise _dbServise;

    public BooksController(IDbServise dbServise)
    {
        _dbServise = dbServise;
    }
    [HttpGet]
    public IList<Book> getBooks()
    {
        return _dbServise.getBookList();
    }
}
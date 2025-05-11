using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Controllers;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using System.Collections.Generic;

public class BooksControllerTests
{
    [Fact]
    public void GetAll_ReturnsOkResult_WithListOfBooks()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        mockRepo.Setup(repo => repo.GetAll()).Returns(new List<Book>
        {
            new Book { BookId = 1, Title = "Test Book", Author = "Author", ISBN = "123", AvailableCopies = 5 }
        });

        var controller = new BooksController(mockRepo.Object, null!); // hubContext not needed for GET test

        // Act
        var result = controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
        Assert.Single(books);
    }
}

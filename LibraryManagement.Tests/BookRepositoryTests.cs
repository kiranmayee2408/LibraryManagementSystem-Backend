using Xunit;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryManagement.Tests
{
    public class BookRepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void AddBook_ShouldStoreBookInDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new BookRepository(context);
            var book = new Book
            {
                Title = "Unit Testing",
                Author = "John Tester",
                ISBN = "UT123",
                AvailableCopies = 5
            };

            // Act
            repository.Add(book);
            var result = context.Books.FirstOrDefault();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Unit Testing", result.Title);
        }
    }
}

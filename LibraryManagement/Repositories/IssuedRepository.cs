using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class IssuedBookRepository : IIssuedBookRepository
    {
        private readonly AppDbContext _context;

        public IssuedBookRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IssuedBook> GetAll() => _context.IssuedBooks.ToList();
        public IssuedBook GetById(int id) => _context.IssuedBooks.Find(id);
        public void Add(IssuedBook issuedBook)
        {
            _context.IssuedBooks.Add(issuedBook);
            _context.SaveChanges();
        }

        public void Update(IssuedBook issuedBook)
        {
            _context.IssuedBooks.Update(issuedBook);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var issued = _context.IssuedBooks.Find(id);
            if (issued != null)
            {
                _context.IssuedBooks.Remove(issued);
                _context.SaveChanges();
            }
        }
    }
}

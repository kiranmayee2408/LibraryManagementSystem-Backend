using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IIssuedBookRepository
    {
        IEnumerable<IssuedBook> GetAll();
        IssuedBook GetById(int id);
        void Add(IssuedBook issuedBook);
        void Update(IssuedBook issuedBook);
        void Delete(int id);
    }
}

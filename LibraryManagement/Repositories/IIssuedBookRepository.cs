using LibraryManagement.Models;
using System.Collections.Generic;

namespace LibraryManagement.Repositories
{
    public interface IIssuedBookRepository
    {
        IEnumerable<IssuedBook> GetAll();
        IEnumerable<IssuedBook> GetAllWithDetails(); // ✅ Includes Book and Member joins

        IssuedBook GetById(int id);
        void Add(IssuedBook issuedBook);
        void Update(IssuedBook issuedBook);
        void Delete(int id);
    }
}

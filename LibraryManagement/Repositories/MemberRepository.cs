using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetAll() => _context.Members.ToList();
        public Member GetById(int id) => _context.Members.Find(id);
        public void Add(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }
        }
    }
}

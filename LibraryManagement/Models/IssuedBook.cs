namespace LibraryManagement.Models
{
    using System.ComponentModel.DataAnnotations;

    public class IssuedBook
    {
        [Key]
        public int IssueId { get; set; } // 👈 This should be your primary key
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}

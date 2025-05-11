using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibraryManagement.Models
{
    public class IssuedBook
    {
        [Key]
        public int IssueId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [ForeignKey(nameof(BookId))]
        [ValidateNever]
        public Book? Book { get; set; }

        [ForeignKey(nameof(MemberId))]
        [ValidateNever]
        public Member? Member { get; set; }
    }
}

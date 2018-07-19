using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookCopy> BooksCopies { get; set; }
    }
}
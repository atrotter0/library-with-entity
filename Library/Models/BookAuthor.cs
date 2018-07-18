using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("BookAuthors")]
    public class BookAuthor
    {
        [Key]
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }

        public BookAuthor() { }

        public BookAuthor(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
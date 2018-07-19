using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("BooksCopies")]
    public class BookCopy
    {
        [Key]
        public int BookCopyId { get; set; }
        public int BookId { get; set; }
        public int CopyId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Copy Copy { get; set; }

        public BookCopy() { }

        public BookCopy(int bookId, int copyId)
        {
            BookId = bookId;
            CopyId = copyId;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("PatronsBooks")]
    public class PatronBook
    {
        [Key]
        public int PatronBookId { get; set; }
        public int PatronId { get; set; }
        public int BookId { get; set; }
        public virtual Patron Patron { get; set; }
        public virtual Book Book { get; set; }

        public PatronBook() { }

        public PatronBook(int patronId, int bookId)
        {
            PatronId = patronId;
            BookId = bookId;
        }
    }
}

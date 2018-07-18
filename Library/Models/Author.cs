using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
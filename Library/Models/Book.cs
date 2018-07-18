using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public string Name { get; set; }
        public Author Author { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
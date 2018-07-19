using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Copies")]
    public class Copy
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public bool CheckedOut { get; set; }
        public DateTime DueDate { get; set; }
        public virtual ICollection<BookCopy> BooksCopies { get; set; }

    }
}

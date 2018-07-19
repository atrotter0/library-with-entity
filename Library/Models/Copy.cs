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
        public int CopyId { get; set; }
        public int Number { get; set; }
        public bool CheckedOut { get; set; }
        public DateTime DueDate { get; set; }
        public virtual ICollection<BookCopy> BooksCopies { get; set; }

        public Copy()
        {
            Number = 10;
            CheckedOut = false;
            DueDate = new DateTime();
        }
    }
}

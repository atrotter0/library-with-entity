using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(75)]
        public string Name { get; set; }
        public Author Author { get; set; }
    }
}
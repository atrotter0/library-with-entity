using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(75)]
        public string Name { get; set; }

        public int Copy { get; set; }

        public Author Author { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class Copy
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool CheckedOut { get; set; }
        public DateTime DueDate { get; set; }
    }
}

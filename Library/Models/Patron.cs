using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Patrons")]
    public class Patron
    {
        [Key]
        public int PatronId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<PatronBook> PatronsBooks { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("PatronsCopies")]
    public class PatronCopy
    {
        [Key]
        public int PatronCopyId { get; set; }
        public int PatronId { get; set; }
        public int CopyId { get; set; }
        public virtual Patron Patron { get; set; }
        public virtual Copy Copy { get; set; }

        public PatronCopy() { }

        public PatronCopy(int patronId, int copyId)
        {
            PatronId = patronId;
            CopyId = copyId;
        }
    }
}

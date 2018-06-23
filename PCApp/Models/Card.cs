namespace PCApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            Assignments = new HashSet<Assignment>();
        }

        public int CardID { get; set; }

        [StringLength(50)]
        public string CardName { get; set; }

        [StringLength(10)]
        public string CardType { get; set; }

        [StringLength(30)]
        public string Set { get; set; }

        [StringLength(30)]
        public string Deck { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}

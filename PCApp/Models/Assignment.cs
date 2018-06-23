namespace PCApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Assignment
    {
        public int AssignmentID { get; set; }

        public int? DeckID { get; set; }

        public int? CardID { get; set; }

        public virtual Card Card { get; set; }

        public virtual Deck Deck { get; set; }
    }
}

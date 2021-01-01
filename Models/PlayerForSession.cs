using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class PlayerForSession
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int SessionId { get; set; }
        public bool Attended { get; set; }
        public int WorkEthic { get; set; }
        public bool TechnicalImprovementDuringSession { get; set; }
        public bool TechnicalImprovementFromPreviousSession { get; set; }
        public string Notes { get; set; }

        public virtual Player Player { get; set; }
        public virtual Session Session { get; set; }
    }
}

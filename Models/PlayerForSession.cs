using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class PlayerForSession
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int SessionId { get; set; }
        public bool Attended { get; set; }
        [DisplayName("Work Ethic")]
        public int WorkEthic { get; set; }
        [DisplayName("Technical Improvement Shown During Session")]
        public bool TechnicalImprovementDuringSession { get; set; }
        [DisplayName("Technical Improvement Shown From Previous Session")]
        public bool TechnicalImprovementFromPreviousSession { get; set; }
        [AllowHtml]
        public string Notes { get; set; }
        [NotMapped]
        public bool isChecked { get; set; }

        public virtual Player Player { get; set; }
        public virtual Session Session { get; set; }
    }
}

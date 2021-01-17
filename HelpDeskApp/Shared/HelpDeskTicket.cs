using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HelpDeskApp.Shared
{
    public partial class HelpDeskTicket
    {
        public HelpDeskTicket()
        {
            HelpDeskTicketDetails = new HashSet<HelpDeskTicketDetail>();
        }

        public int Id { get; set; }
        [Required]
        public string TicketStatus { get; set; }
        [Required]
        public DateTime TicketDate { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be a minimum of 2 and maximum of 50 characters.")]
        public string TicketDescription { get; set; }
        [Required]
        [EmailAddress]
        public string TicketRequesterEmail { get; set; }
        public string TicketGuid { get; set; }

        public virtual ICollection<HelpDeskTicketDetail> HelpDeskTicketDetails { get; set; }
    }
}

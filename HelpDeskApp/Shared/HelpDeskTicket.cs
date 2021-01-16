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
        public string TicketStatus { get; set; }
        public DateTime TicketDate { get; set; }
        public string TicketDescription { get; set; }
        public string TicketRequesterEmail { get; set; }
        public string TicketGuid { get; set; }

        public virtual ICollection<HelpDeskTicketDetail> HelpDeskTicketDetails { get; set; }
    }
}

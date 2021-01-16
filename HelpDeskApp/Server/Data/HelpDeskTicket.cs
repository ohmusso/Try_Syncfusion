using System;
using System.Collections.Generic;

#nullable disable

namespace HelpDeskApp.Server.Data
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

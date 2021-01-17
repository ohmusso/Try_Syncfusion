using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;
using HelpDeskApp.Shared;

namespace HelpDeskApp.Client.Pages{
    public partial class CreateTicket : ComponentBase
    {
        [Inject] HttpClient Http { get; set; }
        SfToast ToastObj;
        private string ToastContent { get; set; } = "";

        // Global property for the help desk ticket.
        HelpDeskTicket objHelpDeskTicket =
            new HelpDeskTicket() { TicketDate = DateTime.Now };

        public async Task HandleValidSubmit()
        {
            try
            {
                // Create a HelpDeskTicket.
                HelpDeskTicket NewHelpDeskTicket = 
                    new HelpDeskTicket();
                
                // Set the values to the values entered
                // in the form.
                NewHelpDeskTicket.TicketDate = objHelpDeskTicket.TicketDate;
                NewHelpDeskTicket.TicketDescription = objHelpDeskTicket.TicketDescription;
                NewHelpDeskTicket.TicketRequesterEmail = objHelpDeskTicket.TicketRequesterEmail;
                NewHelpDeskTicket.TicketStatus = objHelpDeskTicket.TicketStatus;

                // Create a new GUID for this help desk ticket.
                NewHelpDeskTicket.TicketGuid = System.Guid.NewGuid().ToString();

                // Post a new help desk ticket.
                var resp = await Http.PostAsJsonAsync("HelpDesk", NewHelpDeskTicket);

                // Clear the form.
                objHelpDeskTicket = new HelpDeskTicket();

                // Show the toast
                ToastContent = "Saved";
                await Task.Delay(100);
                await this.ToastObj.Show();
            }
            catch(Exception ex)
            {
                ToastContent = ex.GetBaseException().Message;
                await this.ToastObj.Show();
            }
        }
    }
}

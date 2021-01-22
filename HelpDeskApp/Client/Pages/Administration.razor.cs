using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;
using HelpDeskApp.Shared;
using Syncfusion.Blazor.Grids;

namespace HelpDeskApp.Client.Pages{
    public partial class Administration : ComponentBase
    {
        [Inject] HttpClient Http { get; set; }

        SfGrid<HelpDeskTicket> gridObj;
        private HelpDeskTicket SelectedTicket = new HelpDeskTicket();
        // Global property for the help desk ticket.
        HelpDeskTicket objHelpDeskTicket = new HelpDeskTicket() { TicketDate = DateTime.Now };
        public IQueryable<HelpDeskTicket> colHelpDeskTickets { get; set; }
        public bool DeleteRecordConfirmVisibility { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            // GetHelpDeskTickets returns IQueryable that the
            // SfGrid will use to only pull records for the
            // page that is currently selected.
            var tickets = await Http.GetFromJsonAsync<IEnumerable<HelpDeskTicket>>("HelpDesk");
            colHelpDeskTickets = tickets.AsQueryable();
        }

        public void OnCommandClicked(CommandClickEventArgs<HelpDeskTicket> args)
        {
            // Get the selected help desk ticket.
            SelectedTicket = colHelpDeskTickets
                                    .Where(x => x.TicketGuid == args.RowData.TicketGuid)
                                    .FirstOrDefault();
            if (args.CommandColumn.ButtonOption.Content == "Delete")
            {
                // Open Delete confirmation dialog.
                this.DeleteRecordConfirmVisibility = true;
            }
        }

        public async void ConfirmDeleteNo()
        {
            await Task.Delay(100);
            // Open the dialog
            // to give the user a chance
            // to confirm they want to delete the record.
            this.DeleteRecordConfirmVisibility = false;
        }

        public async Task ConfirmDeleteYes()
        {
            // The user selected Yes to delete the
            // selected help desk ticket.
            // Delete the record.
            var result = await Http.DeleteAsync("HelpDesk/" + SelectedTicket.Id);

            var tickets = await Http.GetFromJsonAsync<IEnumerable<HelpDeskTicket>>("HelpDesk");
            colHelpDeskTickets = tickets.AsQueryable();

            // Close the dialog.
            this.DeleteRecordConfirmVisibility = false;

            // Refresh the SfGrid
            // so the deleted record will not show.
            gridObj.Refresh();
        }

        public async Task HandleValidSubmit()
        {

        }
    }
}

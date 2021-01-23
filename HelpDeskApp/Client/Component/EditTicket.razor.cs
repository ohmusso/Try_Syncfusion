using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using HelpDeskApp.Shared;

namespace HelpDeskApp.Client.Component{
    public partial class EditTicket : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Parameter]
        public HelpDeskTicket SelectedTicket { get; set; }
        ClaimsPrincipal CurrentUser = new ClaimsPrincipal();
        public bool isReadOnly = true;
        string NewHelpDeskTicketDetail = "";

        protected override async Task OnInitializedAsync()
        {
            // Get the current user.
            CurrentUser = (await authenticationStateTask).User;
            // If there is a logged in user
            // they are an administrator.
            // Enable editing.
            if( CurrentUser.IsInRole("Administrators") )
            {
                isReadOnly = false;
            }
        }
        private void AddHelpDeskTicketDetail()
        {
            // Create new HelpDeskTicketDetails record.
            HelpDeskTicketDetail NewHelpDeskTicketDetails = new HelpDeskTicketDetail();
            NewHelpDeskTicketDetails.HelpDeskTicketId = SelectedTicket.Id;
            NewHelpDeskTicketDetails.TicketDetailDate = DateTime.Now;
            NewHelpDeskTicketDetails.TicketDescription = NewHelpDeskTicketDetail;
            // Add to collection.
            SelectedTicket.HelpDeskTicketDetails.Add(NewHelpDeskTicketDetails);
            // Clear the Text Box.
            NewHelpDeskTicketDetail = "";
        }
    }
}

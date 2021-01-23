using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDeskApp.Server.Data;
using HelpDeskApp.Server.Models;
using HelpDeskApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskApp.Server.Controllers
{

    [Route("HelpDesk")]
    [ApiController]
    public class SyncfusionHelpDeskController : ControllerBase
    {
        private readonly SyncfusionHelpDeskContext _context;

        public SyncfusionHelpDeskController(SyncfusionHelpDeskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<HelpDeskTicket> GetHelpDeskTickets()
        {
            // Return all HelpDesk tickets as IQueryable.
            // SfGrid will use this to only pull records
            // for the page that it is currently displaying.
            // Note: AsNoTracking() is used because it is
            // quicker to execute, and we do not need
            // Entity Framework change tracking at this point.
            return　_context.HelpDeskTickets.AsNoTracking();
        }

        [HttpGet("{HelpDeskTicketGuid}")]
        public async Task<HelpDeskTicket> GetHelpDeskTicketAsync(string HelpDeskTicketGuid)
        {
            // Get the existing record.
            var ExistingTicket = await _context.HelpDeskTickets
            .Include(x => x.HelpDeskTicketDetails)
            .Where(x => x.TicketGuid == HelpDeskTicketGuid)
            .AsNoTracking()
            .FirstOrDefaultAsync();
            return ExistingTicket;
        }

        [HttpPost]
        public Task<HelpDeskTicket> CreateTicketAsync(HelpDeskTicket newHelpDeskTickets)
        {
            try
            {
                // Add a new help desk ticket.
                _context.HelpDeskTickets.Add(newHelpDeskTickets);
                _context.SaveChanges();
                return Task.FromResult(newHelpDeskTickets);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }

        [HttpPut]
        public Task<bool> UpdateTicketAsync(HelpDeskTicket UpdatedHelpDeskTicket)
        {
            try
            {
                // Get the existing record.
                var ExistingTicket =
                    _context.HelpDeskTickets
                    .Where(x => x.Id == UpdatedHelpDeskTicket.Id)
                    .FirstOrDefault();

                if (ExistingTicket != null)
                {
                    ExistingTicket.TicketDate           = UpdatedHelpDeskTicket.TicketDate;
                    ExistingTicket.TicketDescription    = UpdatedHelpDeskTicket.TicketDescription;
                    ExistingTicket.TicketGuid           = UpdatedHelpDeskTicket.TicketGuid;
                    ExistingTicket.TicketRequesterEmail = UpdatedHelpDeskTicket.TicketRequesterEmail;
                    ExistingTicket.TicketStatus         = UpdatedHelpDeskTicket.TicketStatus;
                    // Insert any new TicketDetails.
                    if (UpdatedHelpDeskTicket.HelpDeskTicketDetails != null)
                    {
                        foreach (var item in UpdatedHelpDeskTicket.HelpDeskTicketDetails)
                        {
                            if (item.Id == 0)
                            {
                                // Create new HelpDeskTicketDetails record.
                                HelpDeskTicketDetail newHelpDeskTicketDetails = new HelpDeskTicketDetail();
                                newHelpDeskTicketDetails.HelpDeskTicketId   = UpdatedHelpDeskTicket.Id;
                                newHelpDeskTicketDetails.TicketDetailDate   = DateTime.Now;
                                newHelpDeskTicketDetails.TicketDescription  = item.TicketDescription;
                                _context.HelpDeskTicketDetails.Add(newHelpDeskTicketDetails);
                            }
                        }
                    }
                    _context.SaveChanges();
                }
                else
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }

        [HttpDelete("{Id}")]
        public Task<bool> DeleteHelpDeskTicketsAsync(int Id)
        {
            // Get the existing record.
            var ExistingTicket = _context.HelpDeskTickets
                .Include(x => x.HelpDeskTicketDetails)
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            if (ExistingTicket != null)
            {
                // Delete the help desk ticket.
                _context.HelpDeskTickets.Remove(ExistingTicket);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        // Utility
        #region public void DetachAllEntities()
        public void DetachAllEntities()
        {
            // When we have an error, we need
            // to remove EF Core change tracking.
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where( e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted)
                .ToList();
            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }
        #endregion
    }
}
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
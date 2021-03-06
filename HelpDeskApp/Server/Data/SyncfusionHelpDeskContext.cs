﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HelpDeskApp.Server.Models;
using HelpDeskApp.Shared;

#nullable disable

namespace HelpDeskApp.Server.Data
{
    public partial class SyncfusionHelpDeskContext : DbContext
    {
        public SyncfusionHelpDeskContext()
        {
        }

        public SyncfusionHelpDeskContext(DbContextOptions<SyncfusionHelpDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HelpDeskTicket> HelpDeskTickets { get; set; }
        public virtual DbSet<HelpDeskTicketDetail> HelpDeskTicketDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HelpDeskTicket>(entity =>
            {
                entity.Property(e => e.TicketDate).HasColumnType("datetime");

                entity.Property(e => e.TicketDescription).IsRequired();

                entity.Property(e => e.TicketGuid)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("TicketGUID");

                entity.Property(e => e.TicketRequesterEmail)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TicketStatus)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<HelpDeskTicketDetail>(entity =>
            {
                entity.Property(e => e.TicketDescription).IsRequired();

                entity.Property(e => e.TicketDetailDate).HasColumnType("datetime");

                entity.HasOne(d => d.HelpDeskTicket)
                    .WithMany(p => p.HelpDeskTicketDetails)
                    .HasForeignKey(d => d.HelpDeskTicketId)
                    .HasConstraintName("FK_HelpDeskTicketDetails_HelpDeskTickets");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

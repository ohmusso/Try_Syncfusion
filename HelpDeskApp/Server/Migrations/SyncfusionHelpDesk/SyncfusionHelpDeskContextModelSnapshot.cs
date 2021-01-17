﻿// <auto-generated />
using System;
using HelpDeskApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MigrationsSyncfusionHelpDesk
{
    [DbContext(typeof(SyncfusionHelpDeskContext))]
    partial class SyncfusionHelpDeskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HelpDeskApp.Shared.HelpDeskTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TicketDate")
                        .HasColumnType("datetime");

                    b.Property<string>("TicketDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("TicketGuid")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT")
                        .HasColumnName("TicketGUID");

                    b.Property<string>("TicketRequesterEmail")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("TicketStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HelpDeskTickets");
                });

            modelBuilder.Entity("HelpDeskApp.Shared.HelpDeskTicketDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HelpDeskTicketId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TicketDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TicketDetailDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("HelpDeskTicketId");

                    b.ToTable("HelpDeskTicketDetails");
                });

            modelBuilder.Entity("HelpDeskApp.Shared.HelpDeskTicketDetail", b =>
                {
                    b.HasOne("HelpDeskApp.Shared.HelpDeskTicket", "HelpDeskTicket")
                        .WithMany("HelpDeskTicketDetails")
                        .HasForeignKey("HelpDeskTicketId")
                        .HasConstraintName("FK_HelpDeskTicketDetails_HelpDeskTickets")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HelpDeskTicket");
                });

            modelBuilder.Entity("HelpDeskApp.Shared.HelpDeskTicket", b =>
                {
                    b.Navigation("HelpDeskTicketDetails");
                });
#pragma warning restore 612, 618
        }
    }
}

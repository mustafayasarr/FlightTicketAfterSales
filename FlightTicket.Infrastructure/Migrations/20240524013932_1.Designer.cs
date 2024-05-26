﻿// <auto-generated />
using System;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlightTicket.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240524013932_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.AirportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AirportCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AirportName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.FlightEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AirlineName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ArrivalDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DestinationAirportId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FlightEntityId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("OriginAirportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("FlightEntityId");

                    b.HasIndex("OriginAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.PassengerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.TicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PNR")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PassengerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.FlightEntity", b =>
                {
                    b.HasOne("FlightTicket.Domain.Models.Entities.AirportEntity", "DestinationAirport")
                        .WithMany()
                        .HasForeignKey("DestinationAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlightTicket.Domain.Models.Entities.FlightEntity", null)
                        .WithMany("Flights")
                        .HasForeignKey("FlightEntityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FlightTicket.Domain.Models.Entities.AirportEntity", "OriginAirport")
                        .WithMany()
                        .HasForeignKey("OriginAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationAirport");

                    b.Navigation("OriginAirport");
                });

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.TicketEntity", b =>
                {
                    b.HasOne("FlightTicket.Domain.Models.Entities.FlightEntity", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlightTicket.Domain.Models.Entities.PassengerEntity", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("FlightTicket.Domain.Models.Entities.FlightEntity", b =>
                {
                    b.Navigation("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
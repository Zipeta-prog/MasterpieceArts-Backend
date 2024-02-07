﻿// <auto-generated />
using System;
using BiddingService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiddingService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240206081447_addBidTable")]
    partial class addBidTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BiddingService.Models.Bids", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArtName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BidAmmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("BidDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BidderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BidderName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bid");
                });
#pragma warning restore 612, 618
        }
    }
}

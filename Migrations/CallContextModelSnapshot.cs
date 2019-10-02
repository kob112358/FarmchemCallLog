﻿// <auto-generated />
using System;
using CallLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FarmchemCallLog.Migrations
{
    [DbContext(typeof(CallContext))]
    partial class CallContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CallLog.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("AddressID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("CallLog.Business", b =>
                {
                    b.Property<int>("BusinessID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressID");

                    b.Property<string>("CompanyName");

                    b.Property<string>("CompanyNotes");

                    b.Property<string>("CustomerCode");

                    b.Property<string>("OutsideRep");

                    b.HasKey("BusinessID");

                    b.HasIndex("AddressID");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("CallLog.Call", b =>
                {
                    b.Property<int>("CallID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressID");

                    b.Property<int?>("BusinessID");

                    b.Property<int?>("CallInformationCallInfoID");

                    b.Property<int?>("CustomerID");

                    b.HasKey("CallID");

                    b.HasIndex("AddressID");

                    b.HasIndex("BusinessID");

                    b.HasIndex("CallInformationCallInfoID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("CallLog.CallInfo", b =>
                {
                    b.Property<int>("CallInfoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CallDate");

                    b.Property<string>("CallNotes");

                    b.Property<bool>("CallResolved");

                    b.Property<string>("ReasonForCall");

                    b.HasKey("CallInfoID");

                    b.ToTable("CallInfo");
                });

            modelBuilder.Entity("CallLog.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressID");

                    b.Property<int?>("BusinessID");

                    b.Property<string>("ContactName");

                    b.Property<string>("CustomerNotes");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.HasKey("CustomerID");

                    b.HasIndex("AddressID");

                    b.HasIndex("BusinessID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("CallLog.Business", b =>
                {
                    b.HasOne("CallLog.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID");
                });

            modelBuilder.Entity("CallLog.Call", b =>
                {
                    b.HasOne("CallLog.Address", "Add")
                        .WithMany("Calls")
                        .HasForeignKey("AddressID");

                    b.HasOne("CallLog.Business", "Bus")
                        .WithMany("Calls")
                        .HasForeignKey("BusinessID");

                    b.HasOne("CallLog.CallInfo", "CallInformation")
                        .WithMany("Calls")
                        .HasForeignKey("CallInformationCallInfoID");

                    b.HasOne("CallLog.Customer", "Cust")
                        .WithMany("Calls")
                        .HasForeignKey("CustomerID");
                });

            modelBuilder.Entity("CallLog.Customer", b =>
                {
                    b.HasOne("CallLog.Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressID");

                    b.HasOne("CallLog.Business", "Business")
                        .WithMany("Customers")
                        .HasForeignKey("BusinessID");
                });
#pragma warning restore 612, 618
        }
    }
}

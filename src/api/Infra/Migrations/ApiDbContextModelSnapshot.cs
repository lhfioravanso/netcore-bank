﻿// <auto-generated />
using System;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasColumnName("Agency")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Balance")
                        .HasColumnName("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnName("Bank")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnName("Number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Domain.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnName("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TransactionOperationId")
                        .HasColumnName("TransactionOperationId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnName("Value")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionOperationId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Domain.Models.TransactionOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TransactionOperation");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Domain.Models.Account", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Transaction", b =>
                {
                    b.HasOne("Domain.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_ACCOUNT_TRANSACTION")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.TransactionOperation", "TransactionOperation")
                        .WithMany()
                        .HasForeignKey("TransactionOperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

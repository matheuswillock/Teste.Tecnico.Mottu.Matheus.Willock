﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure;

#nullable disable

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Migrations
{
    [DbContext(typeof(MottuDbContext))]
    [Migration("20240311182016_InitialMigrate")]
    partial class InitialMigrate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.DeliveryMan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<Guid>>("MotorcycleRental")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("DeliveryMan");
                });

            modelBuilder.Entity("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CnhNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CnhPhotoImgxUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CnhPhotoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CnhType")
                        .HasColumnType("integer");

                    b.Property<string>("IdCnhPhoto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.Motorcycle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRented")
                        .HasColumnType("boolean");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Motorcycle");
                });

            modelBuilder.Entity("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.RentedMotorcyles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRented")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MotorcycleId")
                        .HasColumnType("uuid");

                    b.Property<int>("RentValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RentedMotorcyles");
                });

            modelBuilder.Entity("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.UserAdmin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserAdmin");
                });

            modelBuilder.Entity("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.DeliveryMan", b =>
                {
                    b.HasOne("Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });
#pragma warning restore 612, 618
        }
    }
}
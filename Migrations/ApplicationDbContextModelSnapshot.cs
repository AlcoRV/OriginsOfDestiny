﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OriginsOfDestiny.Data;

#nullable disable

namespace OriginsOfDestiny.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "hstore");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OriginsOfDestiny.Models.Characters.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AttributesJson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Health")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("Mana")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Character", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("OriginsOfDestiny.Models.Dialogs.Dialog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("NeedReplace")
                        .HasColumnType("boolean");

                    b.Property<Dictionary<string, string>>("Responses")
                        .IsRequired()
                        .HasColumnType("hstore");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("OriginsOfDestiny.Models.Sessions.UserSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Guid>("ActiveDialogId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("OriginsOfDestiny.Models.Characters.Player", b =>
                {
                    b.HasBaseType("OriginsOfDestiny.Models.Characters.Character");

                    b.Property<int>("Expirience")
                        .HasColumnType("integer");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.HasIndex("TelegramId")
                        .IsUnique();

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("OriginsOfDestiny.Models.Characters.Player", b =>
                {
                    b.HasOne("OriginsOfDestiny.Models.Characters.Character", null)
                        .WithOne()
                        .HasForeignKey("OriginsOfDestiny.Models.Characters.Player", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OriginsOfDestiny.Models.Sessions.UserSession", "Session")
                        .WithOne("Player")
                        .HasForeignKey("OriginsOfDestiny.Models.Characters.Player", "TelegramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("OriginsOfDestiny.Models.Sessions.UserSession", b =>
                {
                    b.Navigation("Player")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using LimsServer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LimsServer.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("LimsServer.Entities.Log", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("action")
                        .HasColumnType("TEXT");

                    b.Property<string>("message")
                        .HasColumnType("TEXT");

                    b.Property<string>("processorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("taskHangfireID")
                        .HasColumnType("TEXT");

                    b.Property<string>("taskId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("time")
                        .HasColumnType("TEXT");

                    b.Property<string>("type")
                        .HasColumnType("TEXT");

                    b.Property<string>("workflowId")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("LimsServer.Entities.Processor", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("file_type")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("process_found")
                        .HasColumnType("INTEGER");

                    b.Property<string>("version")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("LimsServer.Entities.Task", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("archiveFile")
                        .HasColumnType("TEXT");

                    b.Property<string>("inputFile")
                        .HasColumnType("TEXT");

                    b.Property<string>("message")
                        .HasColumnType("TEXT");

                    b.Property<string>("outputFile")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("start")
                        .HasColumnType("TEXT");

                    b.Property<string>("status")
                        .HasColumnType("TEXT");

                    b.Property<string>("taskID")
                        .HasColumnType("TEXT");

                    b.Property<string>("workflowID")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("LimsServer.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Admin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LimsServer.Entities.Workflow", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("archiveFolder")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("filter")
                        .HasColumnType("TEXT");

                    b.Property<string>("inputFolder")
                        .HasColumnType("TEXT");

                    b.Property<int>("interval")
                        .HasColumnType("INTEGER");

                    b.Property<string>("message")
                        .HasColumnType("TEXT");

                    b.Property<bool>("multiFile")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("outputFolder")
                        .HasColumnType("TEXT");

                    b.Property<string>("processor")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Workflows");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using JobTrackerApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobTrackerApi.Migrations
{
    [DbContext(typeof(JobApplicationDb))]
    [Migration("20240719000644_AddJobApplicationTechStack")]
    partial class AddJobApplicationTechStack
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("JobTrackerApi.Models.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateApplied")
                        .HasColumnType("TEXT");

                    b.Property<string>("HiringTeam")
                        .HasColumnType("TEXT");

                    b.Property<string>("JobDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("MaxSalary")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("MinSalary")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostingUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("JobApplicationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JobApplicationId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Skill", b =>
                {
                    b.HasOne("JobTrackerApi.Models.JobApplication", null)
                        .WithMany("TechStack")
                        .HasForeignKey("JobApplicationId");
                });

            modelBuilder.Entity("JobTrackerApi.Models.JobApplication", b =>
                {
                    b.Navigation("TechStack");
                });
#pragma warning restore 612, 618
        }
    }
}

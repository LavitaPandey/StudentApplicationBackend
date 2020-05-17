﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentAppAPI.Models;

namespace StudentAppAPI.Migrations
{
    [DbContext(typeof(StudentDetailsContext))]
    [Migration("20200516110944_dhgs")]
    partial class dhgs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentAppAPI.Models.ClassDetails", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ClassId");

                    b.ToTable("ClassDetails");
                });

            modelBuilder.Entity("StudentAppAPI.Models.StuSubjectMarks", b =>
                {
                    b.Property<int>("SubMarksId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Marks")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("SubMarksId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StuSubjectMarks");
                });

            modelBuilder.Entity("StudentAppAPI.Models.StudentDetails", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.ToTable("StudentDetails");
                });

            modelBuilder.Entity("StudentAppAPI.Models.SubjectDetails", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SubjectId");

                    b.ToTable("SubjectDetails");
                });

            modelBuilder.Entity("StudentAppAPI.Models.StuSubjectMarks", b =>
                {
                    b.HasOne("StudentAppAPI.Models.StudentDetails", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("StudentAppAPI.Models.SubjectDetails", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("StudentAppAPI.Models.StudentDetails", b =>
                {
                    b.HasOne("StudentAppAPI.Models.ClassDetails", "classData")
                        .WithMany()
                        .HasForeignKey("ClassId");
                });
#pragma warning restore 612, 618
        }
    }
}
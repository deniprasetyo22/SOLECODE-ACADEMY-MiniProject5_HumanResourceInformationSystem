﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProject5.Persistence.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniProject5.Persistence.Migrations
{
    [DbContext(typeof(HrisContext))]
    [Migration("20241121021242_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MiniProject5.Persistence.Models.Department", b =>
                {
                    b.Property<int>("Deptid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("deptid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Deptid"));

                    b.Property<string>("Deptname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("deptname");

                    b.Property<int?>("Mgrempid")
                        .HasColumnType("integer")
                        .HasColumnName("mgrempid");

                    b.HasKey("Deptid")
                        .HasName("department_pkey");

                    b.HasIndex("Mgrempid");

                    b.ToTable("department");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Dependent", b =>
                {
                    b.Property<int>("Dependentid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("dependentid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Dependentid"));

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<int?>("Empid")
                        .HasColumnType("integer")
                        .HasColumnName("empid");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("relationship");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("sex");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("fname");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("lname");

                    b.HasKey("Dependentid")
                        .HasName("dependent_pkey");

                    b.HasIndex("Empid");

                    b.ToTable("dependent");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Employee", b =>
                {
                    b.Property<int>("Empid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("empid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Empid"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<int?>("Deptid")
                        .HasColumnType("integer")
                        .HasColumnName("deptid");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Emptype")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("emptype");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("fname");

                    b.Property<DateTime?>("Lastupdateddate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("lastupdateddate")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<string>("Lname")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("lname");

                    b.Property<string>("Phoneno")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("phoneno");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("position");

                    b.Property<string>("Reason")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("reason");

                    b.Property<double>("Salary")
                        .HasColumnType("double precision")
                        .HasColumnName("salary");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("sex");

                    b.Property<string>("Ssn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("ssn");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("status");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("integer")
                        .HasColumnName("supervisorid");

                    b.HasKey("Empid")
                        .HasName("employee_pkey");

                    b.HasIndex("Deptid");

                    b.HasIndex("SupervisorId");

                    b.HasIndex(new[] { "Ssn" }, "employee_ssn_key")
                        .IsUnique();

                    b.ToTable("employee");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Location", b =>
                {
                    b.Property<int>("Locationid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("locationid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Locationid"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<int?>("Deptid")
                        .HasColumnType("integer")
                        .HasColumnName("deptid");

                    b.HasKey("Locationid")
                        .HasName("location_pkey");

                    b.HasIndex("Deptid");

                    b.ToTable("location");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Project", b =>
                {
                    b.Property<int>("Projid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("projid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Projid"));

                    b.Property<int>("Deptid")
                        .HasColumnType("integer")
                        .HasColumnName("deptid");

                    b.Property<string>("Projname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("projname");

                    b.HasKey("Projid")
                        .HasName("project_pkey");

                    b.HasIndex("Deptid");

                    b.ToTable("project");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Workson", b =>
                {
                    b.Property<int>("Empid")
                        .HasColumnType("integer")
                        .HasColumnName("empid");

                    b.Property<int>("Projid")
                        .HasColumnType("integer")
                        .HasColumnName("projid");

                    b.Property<DateOnly>("Dateworked")
                        .HasColumnType("date")
                        .HasColumnName("dateworked");

                    b.Property<int?>("Hoursworked")
                        .HasColumnType("integer")
                        .HasColumnName("hoursworked");

                    b.HasKey("Empid", "Projid")
                        .HasName("workson_pkey");

                    b.HasIndex("Projid");

                    b.ToTable("workson");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Department", b =>
                {
                    b.HasOne("MiniProject5.Persistence.Models.Employee", "Mgremp")
                        .WithMany("Departments")
                        .HasForeignKey("Mgrempid")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_mgrempid");

                    b.Navigation("Mgremp");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Dependent", b =>
                {
                    b.HasOne("MiniProject5.Persistence.Models.Employee", "Emp")
                        .WithMany("Dependents")
                        .HasForeignKey("Empid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_employee");

                    b.Navigation("Emp");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Employee", b =>
                {
                    b.HasOne("MiniProject5.Persistence.Models.Department", "Dept")
                        .WithMany("Employees")
                        .HasForeignKey("Deptid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_department");

                    b.HasOne("MiniProject5.Persistence.Models.Employee", "Supervisor")
                        .WithMany("Subordinates")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Dept");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Location", b =>
                {
                    b.HasOne("MiniProject5.Persistence.Models.Department", "Dept")
                        .WithMany("Locations")
                        .HasForeignKey("Deptid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_department");

                    b.Navigation("Dept");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Project", b =>
                {
                    b.HasOne("MiniProject5.Persistence.Models.Department", "Dept")
                        .WithMany("Projects")
                        .HasForeignKey("Deptid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_deptid");

                    b.Navigation("Dept");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Workson", b =>
                {
                    b.HasOne("MiniProject5.Persistence.Models.Employee", "Emp")
                        .WithMany("Worksons")
                        .HasForeignKey("Empid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("workson_empid_fkey");

                    b.HasOne("MiniProject5.Persistence.Models.Project", "Proj")
                        .WithMany("Worksons")
                        .HasForeignKey("Projid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("workson_projid_fkey");

                    b.Navigation("Emp");

                    b.Navigation("Proj");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Locations");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Employee", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Dependents");

                    b.Navigation("Subordinates");

                    b.Navigation("Worksons");
                });

            modelBuilder.Entity("MiniProject5.Persistence.Models.Project", b =>
                {
                    b.Navigation("Worksons");
                });
#pragma warning restore 612, 618
        }
    }
}

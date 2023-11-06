﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using authentication.domain.Infra.Contexts;

#nullable disable

namespace Authentication.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("authentication.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<Guid>("PersonIdCreated")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Status")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("authentication.domain.entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PersonIdCreated")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SocialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Status")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("authentication.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("authentication.domain.entities.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("authentication.domain.entities.Person", b =>
                {
                    b.OwnsMany("authentication.domain.Entities.Address", "Addresses", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AddressDistrict")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AddressLine1")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AddressLine2")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AddressLine3")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AddressLine4")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AddressZip")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Comments")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Modified")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("PersonIdCreated")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool?>("Primary")
                                .HasColumnType("bit");

                            b1.Property<string>("StateOrProvince")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<decimal>("Status")
                                .HasColumnType("decimal(20,0)");

                            b1.HasKey("PersonId", "Id");

                            b1.ToTable("Persons_Addresses", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsMany("authentication.domain.Entities.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Comments")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<bool?>("IsPrimary")
                                .HasColumnType("bit");

                            b1.Property<bool?>("IsRecovery")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("Modified")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("PersonIdCreated")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Status")
                                .HasColumnType("decimal(20,0)");

                            b1.Property<decimal?>("Type")
                                .HasColumnType("decimal(20,0)");

                            b1.Property<bool?>("Verified")
                                .HasColumnType("bit");

                            b1.HasKey("PersonId", "Id");

                            b1.ToTable("Persons_Emails", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("authentication.domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Checked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("HashedPassword")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("IsFirstTime")
                                .HasColumnType("bit");

                            b1.Property<string>("PreviousHashedPassword")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("RecoveryCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("RecoveryExpirationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Salt")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("NVARCHAR")
                                .HasColumnName("Password_Salt");

                            b1.Property<DateTime>("Updated")
                                .HasColumnType("datetime2");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Addresses");

                    b.Navigation("Email");

                    b.Navigation("Password");
                });
#pragma warning restore 612, 618
        }
    }
}

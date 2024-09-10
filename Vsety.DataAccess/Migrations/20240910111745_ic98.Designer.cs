﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vsety.DataAccess;

#nullable disable

namespace Vsety.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240910111745_ic98")]
    partial class ic98
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PostEntityUserEntity", b =>
                {
                    b.Property<Guid>("PostLikesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserLikesId")
                        .HasColumnType("char(36)");

                    b.HasKey("PostLikesId", "UserLikesId");

                    b.HasIndex("UserLikesId");

                    b.ToTable("PostEntityUserEntity");
                });

            modelBuilder.Entity("PostEntityUserEntity1", b =>
                {
                    b.Property<Guid>("PostRepostsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserRepostsId")
                        .HasColumnType("char(36)");

                    b.HasKey("PostRepostsId", "UserRepostsId");

                    b.HasIndex("UserRepostsId");

                    b.ToTable("PostEntityUserEntity1");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.CommentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.ImgEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Imgs");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.PostEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ImgId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<int>("countComments")
                        .HasColumnType("int");

                    b.Property<int>("countLikes")
                        .HasColumnType("int");

                    b.Property<int>("countReposts")
                        .HasColumnType("int");

                    b.Property<Guid?>("logoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ImgId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.HasIndex("logoId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PostEntityUserEntity", b =>
                {
                    b.HasOne("Vsety.DataAccess.Entities.PostEntity", null)
                        .WithMany()
                        .HasForeignKey("PostLikesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vsety.DataAccess.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserLikesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostEntityUserEntity1", b =>
                {
                    b.HasOne("Vsety.DataAccess.Entities.PostEntity", null)
                        .WithMany()
                        .HasForeignKey("PostRepostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vsety.DataAccess.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserRepostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.CommentEntity", b =>
                {
                    b.HasOne("Vsety.DataAccess.Entities.PostEntity", "Post")
                        .WithMany("UsersComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vsety.DataAccess.Entities.UserEntity", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.ImgEntity", b =>
                {
                    b.HasOne("Vsety.DataAccess.Entities.PersonEntity", "Person")
                        .WithOne("Img")
                        .HasForeignKey("Vsety.DataAccess.Entities.ImgEntity", "PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.PersonEntity", b =>
                {
                    b.HasOne("Vsety.DataAccess.Entities.UserEntity", "User")
                        .WithOne("Person")
                        .HasForeignKey("Vsety.DataAccess.Entities.PersonEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.PostEntity", b =>
                {
                    b.HasOne("Vsety.DataAccess.Entities.ImgEntity", "Img")
                        .WithOne("Post")
                        .HasForeignKey("Vsety.DataAccess.Entities.PostEntity", "ImgId");

                    b.HasOne("Vsety.DataAccess.Entities.UserEntity", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vsety.DataAccess.Entities.ImgEntity", "logo")
                        .WithMany()
                        .HasForeignKey("logoId");

                    b.Navigation("Img");

                    b.Navigation("User");

                    b.Navigation("logo");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.ImgEntity", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.PersonEntity", b =>
                {
                    b.Navigation("Img");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.PostEntity", b =>
                {
                    b.Navigation("UsersComments");
                });

            modelBuilder.Entity("Vsety.DataAccess.Entities.UserEntity", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Person");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}

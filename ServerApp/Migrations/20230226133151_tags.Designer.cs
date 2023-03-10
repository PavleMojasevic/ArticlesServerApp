﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerApp.Infrastucture;

#nullable disable

namespace ServerApp.Migrations;

[DbContext(typeof(ArticlesDbContext))]
[Migration("20230226133151_Tags")]
partial class Tags
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "6.0.13");

        modelBuilder.Entity("CommentUser", b =>
            {
                b.Property<long>("LikedId")
                    .HasColumnType("INTEGER");

                b.Property<long>("LikedId1")
                    .HasColumnType("INTEGER");

                b.HasKey("LikedId", "LikedId1");

                b.HasIndex("LikedId1");

                b.ToTable("CommentUser");
            });

        modelBuilder.Entity("CommentUser1", b =>
            {
                b.Property<long>("DislikedId")
                    .HasColumnType("INTEGER");

                b.Property<long>("DislikedId1")
                    .HasColumnType("INTEGER");

                b.HasKey("DislikedId", "DislikedId1");

                b.HasIndex("DislikedId1");

                b.ToTable("CommentUser1");
            });

        modelBuilder.Entity("ServerApp.Models.Article", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<long>("AuthorId")
                    .HasColumnType("INTEGER");

                b.Property<long>("CategoryId")
                    .HasColumnType("INTEGER");

                b.Property<string>("Content")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime>("Date")
                    .HasColumnType("TEXT");

                b.Property<byte[]>("Image")
                    .HasColumnType("BLOB");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("AuthorId");

                b.HasIndex("CategoryId");

                b.ToTable("Articles");
            });

        modelBuilder.Entity("ServerApp.Models.ArticleTag", b =>
            {
                b.Property<long>("ArticleId")
                    .HasColumnType("INTEGER");

                b.Property<string>("TagName")
                    .HasColumnType("TEXT");

                b.HasKey("ArticleId", "TagName");

                b.HasIndex("TagName");

                b.ToTable("ArticleTags");
            });

        modelBuilder.Entity("ServerApp.Models.Category", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<long?>("ParentId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");

                b.HasIndex("ParentId");

                b.ToTable("Categories");
            });

        modelBuilder.Entity("ServerApp.Models.Comment", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<long>("ArticleId")
                    .HasColumnType("INTEGER");

                b.Property<long>("AuthorId")
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("Date")
                    .HasColumnType("TEXT");

                b.Property<int>("Dislikes")
                    .HasColumnType("INTEGER");

                b.Property<int>("Likes")
                    .HasColumnType("INTEGER");

                b.Property<long?>("ParentId")
                    .HasColumnType("INTEGER");

                b.Property<int>("Status")
                    .HasColumnType("INTEGER");

                b.Property<string>("Text")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("ArticleId");

                b.HasIndex("AuthorId");

                b.HasIndex("ParentId");

                b.ToTable("Comments");
            });

        modelBuilder.Entity("ServerApp.Models.Tag", b =>
            {
                b.Property<string>("Name")
                    .HasColumnType("TEXT");

                b.HasKey("Name");

                b.ToTable("Tags");
            });

        modelBuilder.Entity("ServerApp.Models.User", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("Created")
                    .HasColumnType("TEXT");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<int>("Role")
                    .HasColumnType("INTEGER");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Users");
            });

        modelBuilder.Entity("CommentUser", b =>
            {
                b.HasOne("ServerApp.Models.User", null)
                    .WithMany()
                    .HasForeignKey("LikedId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ServerApp.Models.Comment", null)
                    .WithMany()
                    .HasForeignKey("LikedId1")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("CommentUser1", b =>
            {
                b.HasOne("ServerApp.Models.User", null)
                    .WithMany()
                    .HasForeignKey("DislikedId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ServerApp.Models.Comment", null)
                    .WithMany()
                    .HasForeignKey("DislikedId1")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("ServerApp.Models.Article", b =>
            {
                b.HasOne("ServerApp.Models.User", "Author")
                    .WithMany("Articles")
                    .HasForeignKey("AuthorId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();

                b.HasOne("ServerApp.Models.Category", "Category")
                    .WithMany("Articles")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();

                b.Navigation("Author");

                b.Navigation("Category");
            });

        modelBuilder.Entity("ServerApp.Models.ArticleTag", b =>
            {
                b.HasOne("ServerApp.Models.Article", "Article")
                    .WithMany("Tags")
                    .HasForeignKey("ArticleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ServerApp.Models.Tag", "Tag")
                    .WithMany("Articles")
                    .HasForeignKey("TagName")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Article");

                b.Navigation("Tag");
            });

        modelBuilder.Entity("ServerApp.Models.Category", b =>
            {
                b.HasOne("ServerApp.Models.Category", "Parent")
                    .WithMany("Subcategories")
                    .HasForeignKey("ParentId")
                    .OnDelete(DeleteBehavior.SetNull);

                b.Navigation("Parent");
            });

        modelBuilder.Entity("ServerApp.Models.Comment", b =>
            {
                b.HasOne("ServerApp.Models.Article", "Article")
                    .WithMany("Comments")
                    .HasForeignKey("ArticleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ServerApp.Models.User", "Author")
                    .WithMany("Comments")
                    .HasForeignKey("AuthorId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();

                b.HasOne("ServerApp.Models.Comment", "Parent")
                    .WithMany("Replies")
                    .HasForeignKey("ParentId")
                    .OnDelete(DeleteBehavior.SetNull);

                b.Navigation("Article");

                b.Navigation("Author");

                b.Navigation("Parent");
            });

        modelBuilder.Entity("ServerApp.Models.Article", b =>
            {
                b.Navigation("Comments");

                b.Navigation("Tags");
            });

        modelBuilder.Entity("ServerApp.Models.Category", b =>
            {
                b.Navigation("Articles");

                b.Navigation("Subcategories");
            });

        modelBuilder.Entity("ServerApp.Models.Comment", b =>
            {
                b.Navigation("Replies");
            });

        modelBuilder.Entity("ServerApp.Models.Tag", b =>
            {
                b.Navigation("Articles");
            });

        modelBuilder.Entity("ServerApp.Models.User", b =>
            {
                b.Navigation("Articles");

                b.Navigation("Comments");
            });
#pragma warning restore 612, 618
    }
}

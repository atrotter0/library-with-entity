using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Library.Models;

namespace Library.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20180718231355_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Library.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorFirstName");

                    b.Property<string>("AuthorLastName");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookTitle");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Models.BookAuthor", b =>
                {
                    b.Property<int>("BookAuthorId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("BookId");

                    b.HasKey("BookAuthorId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("Library.Models.Copy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CheckedOut");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Copies");
                });

            modelBuilder.Entity("Library.Models.Patron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Patrons");
                });

            modelBuilder.Entity("Library.Models.BookAuthor", b =>
                {
                    b.HasOne("Library.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

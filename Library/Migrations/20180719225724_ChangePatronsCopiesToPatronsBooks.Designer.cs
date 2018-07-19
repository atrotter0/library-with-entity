using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Library.Models;

namespace Library.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20180719225724_ChangePatronsCopiesToPatronsBooks")]
    partial class ChangePatronsCopiesToPatronsBooks
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

            modelBuilder.Entity("Library.Models.BookCopy", b =>
                {
                    b.Property<int>("BookCopyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<int>("CopyId");

                    b.HasKey("BookCopyId");

                    b.HasIndex("BookId");

                    b.HasIndex("CopyId");

                    b.ToTable("BooksCopies");
                });

            modelBuilder.Entity("Library.Models.Copy", b =>
                {
                    b.Property<int>("CopyId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CheckedOut");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("Number");

                    b.HasKey("CopyId");

                    b.ToTable("Copies");
                });

            modelBuilder.Entity("Library.Models.Patron", b =>
                {
                    b.Property<int>("PatronId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("PatronId");

                    b.ToTable("Patrons");
                });

            modelBuilder.Entity("Library.Models.PatronBook", b =>
                {
                    b.Property<int>("PatronCopyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<int>("PatronId");

                    b.HasKey("PatronCopyId");

                    b.HasIndex("BookId");

                    b.HasIndex("PatronId");

                    b.ToTable("PatronsBooks");
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

            modelBuilder.Entity("Library.Models.BookCopy", b =>
                {
                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany("BooksCopies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Library.Models.Copy", "Copy")
                        .WithMany("BooksCopies")
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Library.Models.PatronBook", b =>
                {
                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Library.Models.Patron", "Patron")
                        .WithMany("PatronsBooks")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

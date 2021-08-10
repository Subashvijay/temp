using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TweetApplication.Models
{
    public partial class tweetAppDatabaseContext : DbContext
    {
        public tweetAppDatabaseContext()
        {
        }

        public tweetAppDatabaseContext(DbContextOptions<tweetAppDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAB-63755963995;Database=tweetAppDatabase;User Id=sa;Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasColumnName("comments");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.TweetId).HasColumnName("tweet_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Tweet)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TweetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__tweet_i__403A8C7D");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Comments)
                    .HasPrincipalKey(p => p.UserName)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__user_na__412EB0B6");
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.ToTable("tweet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text");

                entity.Property(e => e.TweetDate)
                    .HasColumnType("datetime")
                    .HasColumnName("tweet_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name")
                    .HasDefaultValueSql("('User')");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TweetUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tweet__user_id__276EDEB3");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.TweetUserNameNavigations)
                    .HasPrincipalKey(p => p.UserName)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tweet__user_name__4222D4EF");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("user_info");

                entity.HasIndex(e => e.UserName, "UQ__user_inf__7C9273C418DE6213")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__user_inf__AB6E6164E64D7808")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContactNumber).HasColumnName("contact_number");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.ImageName)
                    .IsUnicode(false)
                    .HasColumnName("image_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name")
                    .HasDefaultValueSql("('User')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

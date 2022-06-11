using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer
{
    public partial class DiabetesTrackerDbContext : DbContext
    {
        public DiabetesTrackerDbContext() { }
        public DiabetesTrackerDbContext(DbContextOptions<DiabetesTrackerDbContext> options) : base(options) { }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<FavouritePost> FavouritePosts { get; set; }
        public virtual DbSet<FollowingBlog> FollowingBlogs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuRecipe> MenuRecipes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DiabetesTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                // Set the property CreatedOn to have type datetime in the database
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");


                // Set the property Name to have type nvarchar(128) in the database
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                // Create a foreign key FK_Blogs.UserId with one User and many Blogs
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blogs.UserId");
            });

            modelBuilder.Entity<FavouritePost>(entity =>
            {
                // Make composit primary key that consists of UserId and PostId
                entity.HasKey(e => new { e.UserId, e.PostId });

                // Set the property SavedOn to have type datetime in the database
                entity.Property(e => e.SavedOn).HasColumnType("datetime");

                // Create a foreign key FK_Favourites.PostId with one Post and many FavouritePosts
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.FavouritePosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Favourites.PostId");

                // Create a foreign key FK_Favourites.UserId with one User and many FavouritePosts
                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouritePosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Favourites.UserId");
            });

            modelBuilder.Entity<FollowingBlog>(entity =>
            {
                // Make composit primary key that consists of BlogId and UserId
                entity.HasKey(e => new { e.BlogId, e.UserId });

                // Create a foreign key FK_FollowingBlogs.UserId with one User and many FollowingBlogs
                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowingBlogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowingBlogs.UserId");

                // Create a foreign key FK_FollowingBlogs.BlogId with one Blog and many FollowingBlogs
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.FollowingBlogs)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowingBlogs.BlogId");

                // Set the property FollowedOn to have type datetime2 in the database
                entity.Property(p => p.FollowedOn).HasColumnType("datetime2");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                // Set the property CreatedOn to have type datetime in the database
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                // Set the property Name to have type nvarchar(128) in the database
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<MenuRecipe>(entity =>
            {
                // Create a foreign key FK_MenuRecipes.RecipeId with one Menu and many MenuRecipes
                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuRecipes)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRecipes.RecipeId");

                // Create a foreign key FK_MenuRecipes.MenuId with one Recipe and many MenuRecipes
                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.MenuRecipes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRecipes.MenuId");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                // Set the property Content to have type nvarchar(1024) in the database
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1024);

                // Set the property Image to have type image in the database
                entity.Property(e => e.Image).HasColumnType("image");

                // Set the property PublishedOn to have type datetime in the database
                entity.Property(e => e.PublishedOn).HasColumnType("datetime");

                // Create a foreign key FK_Posts.UserId with one User and many Posts
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts.UserId");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                // Create a primary key PostCommentsId
                entity.HasKey(e => e.PostCommentsId);

                // Set the property CommentContend to have type nvarchar(1024) in the database
                entity.Property(e => e.CommentContend)
                    .IsRequired()
                    .HasMaxLength(1024);

                // Set the property CommentedOn to have type datetime in the database
                entity.Property(e => e.CommentedOn).HasColumnType("datetime");

                // Create a foreign key FK_PostComments.PostId with one Post and many PostComments
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComments.PostId");

                // Create a foreign key FK_PostComments.UserId with one User and many PostComments
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComments.UserId");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                // Make composit primary key that consists of UserId and PostId
                entity.HasKey(e => new { e.UserId, e.PostId })
                    .HasName("PK__PostLike__8D29EA4D4B7C8E8A");

                // Create a foreign key FK_PostLikes.PostId with one Post and many PostLikes
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostLikes.PostId");

                // Create a foreign key FK_PostLikes.UserId with one User and many PostLikes
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostLikes.UserId");
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                // Make composit primary key that consists of PostId and TagId
                entity.HasKey(e => new { e.PostId, e.TagId })
                    .HasName("PK__PostTags__7C45AF82E2438F65");

                // Create a foreign key FK_PostTags.PostTagId with one Post and many PostTags
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTags.PostTagId");

                // Create a foreign key FK_PostTags.TagId with one Tag and many PostTags
                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTags.TagId");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                // Set the property Content to have type text in the database
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                // Set the property CreatedOn to have type datetime in the database
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                // Set the property Name to have type nvarchar(128) in the database
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                // Create a foreign key FK_Recipes.UserId with one User and many Recipes
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes.UserId");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                // Set the property TagName to have type nvarchar(64) in the database
                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("Tag");
            });

            modelBuilder.Entity<User>(entity =>
            {
                // Set the property DateRegisterd to have type datetime in the database
                entity.Property(e => e.DateRegisterd).HasColumnType("datetime");

                // Set the property Email to have type nvarchar(256) in the database
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                // Set the property Password to have type nvarchar(256) in the database
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                // Set the property Salt to have type nvarchar(16) in the database
                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(16);

                // Set the property UserName to have type nvarchar(128) in the database
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                // Set the property About to have type text in the database
                entity.Property(e => e.About)
                    .IsRequired()
                    .HasColumnType("text");

                // Set the property City to have type nvarchar(128) in the database
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(128);

                // Set the property Country to have type nvarchar(128) in the database
                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(128);

                // Set the property DateUpdated to have type datetime in the database
                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                // Set the property Gender to have type nvarchar(1) in the database
                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                // Create a foreign key FK_UserProfiles_Users with one User and many UserProfiles
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfiles_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

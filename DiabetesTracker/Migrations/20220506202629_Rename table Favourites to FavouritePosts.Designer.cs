﻿// <auto-generated />
using System;
using DiabetesTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiabetesTracker.Migrations
{
    [DbContext(typeof(DiabetesTrackerDbContext))]
    [Migration("20220506202629_Rename table Favourites to FavouritePosts")]
    partial class RenametableFavouritestoFavouritePosts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DiabetesTracker.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BlogId");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("DiabetesTracker.Models.FavouritePost", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SavedOn")
                        .HasColumnType("datetime");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("FavouritePosts");
                });

            modelBuilder.Entity("DiabetesTracker.Models.FollowingBlog", b =>
                {
                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FollowedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("BlogId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FollowingBlogs");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("DiabetesTracker.Models.MenuRecipe", b =>
                {
                    b.Property<int>("MenuRecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("MenuRecipeId");

                    b.HasIndex("MenuId");

                    b.HasIndex("RecipeId");

                    b.ToTable("MenuRecipes");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<int>("CommentCount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DiabetesTracker.Models.PostComment", b =>
                {
                    b.Property<int>("PostCommentsId")
                        .HasColumnType("int");

                    b.Property<string>("CommentContend")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime>("CommentedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostCommentsId")
                        .HasName("PK__PostComm__A59DBB3A861A0D1F");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("DiabetesTracker.Models.PostLike", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PostId")
                        .HasName("PK__PostLike__8D29EA4D4B7C8E8A");

                    b.HasIndex("PostId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("DiabetesTracker.Models.PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId")
                        .HasName("PK__PostTags__7C45AF82E2438F65");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("Tag");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DiabetesTracker.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("AccountStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateRegisterd")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DiabetesTracker.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength(true);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserProfileId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Blog", b =>
                {
                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Blogs.UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.FavouritePost", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Post", "Post")
                        .WithMany("FavouritePosts")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Favourites.PostId")
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("FavouritePosts")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Favourites.UserId")
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.FollowingBlog", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Blog", "Blog")
                        .WithMany("FollowingBlogs")
                        .HasForeignKey("BlogId")
                        .HasConstraintName("FK_FollowingBlogs.BlogId")
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("FollowingBlogs")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_FollowingBlogs.UserId")
                        .IsRequired();

                    b.Navigation("Blog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.MenuRecipe", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Menu", "Menu")
                        .WithMany("MenuRecipes")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("FK_MenuRecipes.RecipeId")
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.Recipe", "Recipe")
                        .WithMany("MenuRecipes")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_MenuRecipes.MenuId")
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Post", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Blog", "Blogs")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Posts.UserId")
                        .IsRequired();

                    b.Navigation("Blogs");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.PostComment", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostComments.PostId")
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("PostComments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_PostComments.UserId")
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.PostLike", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostLikes.PostId")
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("PostLikes")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_PostLikes.UserId")
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.PostTag", b =>
                {
                    b.HasOne("DiabetesTracker.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTags.PostTagId")
                        .IsRequired();

                    b.HasOne("DiabetesTracker.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTags.TagId")
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Recipe", b =>
                {
                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Recipes.UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.UserProfile", b =>
                {
                    b.HasOne("DiabetesTracker.Models.User", "User")
                        .WithMany("UserProfiles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserProfiles_Users")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Blog", b =>
                {
                    b.Navigation("FollowingBlogs");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Menu", b =>
                {
                    b.Navigation("MenuRecipes");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Post", b =>
                {
                    b.Navigation("FavouritePosts");

                    b.Navigation("PostComments");

                    b.Navigation("PostLikes");

                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Recipe", b =>
                {
                    b.Navigation("MenuRecipes");
                });

            modelBuilder.Entity("DiabetesTracker.Models.Tag", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("DiabetesTracker.Models.User", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("FavouritePosts");

                    b.Navigation("FollowingBlogs");

                    b.Navigation("PostComments");

                    b.Navigation("PostLikes");

                    b.Navigation("Posts");

                    b.Navigation("Recipes");

                    b.Navigation("UserProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}

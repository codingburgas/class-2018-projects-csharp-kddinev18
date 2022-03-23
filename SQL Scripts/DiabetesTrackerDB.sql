USE [master]
GO
/****** Object:  Database [DiabetesTracker]    Script Date: 3/23/2022 4:44:16 PM ******/
CREATE DATABASE [DiabetesTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DiabetesTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DiabetesTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DiabetesTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DiabetesTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiabetesTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiabetesTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiabetesTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiabetesTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DiabetesTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiabetesTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiabetesTracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiabetesTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiabetesTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiabetesTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiabetesTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiabetesTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiabetesTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiabetesTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiabetesTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiabetesTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DiabetesTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiabetesTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiabetesTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiabetesTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiabetesTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiabetesTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiabetesTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiabetesTracker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DiabetesTracker] SET  MULTI_USER 
GO
ALTER DATABASE [DiabetesTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiabetesTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiabetesTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiabetesTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DiabetesTracker] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DiabetesTracker]
GO
/****** Object:  Table [dbo].[BlogPosts]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPosts](
	[BlogPostId] [int] NOT NULL,
	[BlogId] [int] NULL,
	[PostId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favourites]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favourites](
	[FavouritesId] [int] NOT NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
	[SavedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FavouritesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FollowingBlogs]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowingBlogs](
	[FollowingBlogId] [int] NOT NULL,
	[BlogId] [int] NULL,
	[UserId] [int] NULL,
	[FollowedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FollowingBlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuRecipes]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuRecipes](
	[Id] [int] NOT NULL,
	[RecipeId] [int] NULL,
	[MenuId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostComments]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComments](
	[PostCommentsId] [int] NOT NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
	[Comment] [nvarchar](50) NULL,
	[CommentedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PostCommentsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostLikes]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostLikes](
	[PostLikeId] [int] NOT NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PostLikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Content] [nvarchar](50) NULL,
	[Image] [image] NULL,
	[PublishedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostTags]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTags](
	[PostTagId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostTagId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[RecipeId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Content] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagId] [int] NOT NULL,
	[Tag] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[UserProfileId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Gender] [char](1) NULL,
	[About] [text] NULL,
	[DateUpdated] [datetime] NULL,
	[Country] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/23/2022 4:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[DateRegisterd] [datetime] NULL,
	[IsAdmin] [bit] NULL,
	[AccountStatus] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogPosts]  WITH CHECK ADD  CONSTRAINT [FK_BlogPosts.BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO
ALTER TABLE [dbo].[BlogPosts] CHECK CONSTRAINT [FK_BlogPosts.BlogId]
GO
ALTER TABLE [dbo].[BlogPosts]  WITH CHECK ADD  CONSTRAINT [FK_BlogPosts.PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
GO
ALTER TABLE [dbo].[BlogPosts] CHECK CONSTRAINT [FK_BlogPosts.PostId]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD  CONSTRAINT [FK_Blogs.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Blogs] CHECK CONSTRAINT [FK_Blogs.UserId]
GO
ALTER TABLE [dbo].[Favourites]  WITH CHECK ADD  CONSTRAINT [FK_Favourites.PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
GO
ALTER TABLE [dbo].[Favourites] CHECK CONSTRAINT [FK_Favourites.PostId]
GO
ALTER TABLE [dbo].[Favourites]  WITH CHECK ADD  CONSTRAINT [FK_Favourites.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Favourites] CHECK CONSTRAINT [FK_Favourites.UserId]
GO
ALTER TABLE [dbo].[FollowingBlogs]  WITH CHECK ADD  CONSTRAINT [FK_FollowingBlogs.BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO
ALTER TABLE [dbo].[FollowingBlogs] CHECK CONSTRAINT [FK_FollowingBlogs.BlogId]
GO
ALTER TABLE [dbo].[FollowingBlogs]  WITH CHECK ADD  CONSTRAINT [FK_FollowingBlogs.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[FollowingBlogs] CHECK CONSTRAINT [FK_FollowingBlogs.UserId]
GO
ALTER TABLE [dbo].[MenuRecipes]  WITH CHECK ADD  CONSTRAINT [FK_MenuRecipes.MenuId] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Recipes] ([RecipeId])
GO
ALTER TABLE [dbo].[MenuRecipes] CHECK CONSTRAINT [FK_MenuRecipes.MenuId]
GO
ALTER TABLE [dbo].[MenuRecipes]  WITH CHECK ADD  CONSTRAINT [FK_MenuRecipes.RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Menus] ([MenuId])
GO
ALTER TABLE [dbo].[MenuRecipes] CHECK CONSTRAINT [FK_MenuRecipes.RecipeId]
GO
ALTER TABLE [dbo].[PostComments]  WITH CHECK ADD  CONSTRAINT [FK_PostComments.PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
GO
ALTER TABLE [dbo].[PostComments] CHECK CONSTRAINT [FK_PostComments.PostId]
GO
ALTER TABLE [dbo].[PostComments]  WITH CHECK ADD  CONSTRAINT [FK_PostComments.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[PostComments] CHECK CONSTRAINT [FK_PostComments.UserId]
GO
ALTER TABLE [dbo].[PostLikes]  WITH CHECK ADD  CONSTRAINT [FK_PostLikes.PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
GO
ALTER TABLE [dbo].[PostLikes] CHECK CONSTRAINT [FK_PostLikes.PostId]
GO
ALTER TABLE [dbo].[PostLikes]  WITH CHECK ADD  CONSTRAINT [FK_PostLikes.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[PostLikes] CHECK CONSTRAINT [FK_PostLikes.UserId]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts.UserId]
GO
ALTER TABLE [dbo].[PostTags]  WITH CHECK ADD  CONSTRAINT [FK_PostTags.PostTagId] FOREIGN KEY([PostTagId])
REFERENCES [dbo].[Posts] ([PostId])
GO
ALTER TABLE [dbo].[PostTags] CHECK CONSTRAINT [FK_PostTags.PostTagId]
GO
ALTER TABLE [dbo].[PostTags]  WITH CHECK ADD  CONSTRAINT [FK_PostTags.TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
GO
ALTER TABLE [dbo].[PostTags] CHECK CONSTRAINT [FK_PostTags.TagId]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes.UserId]
GO
USE [master]
GO
ALTER DATABASE [DiabetesTracker] SET  READ_WRITE 
GO

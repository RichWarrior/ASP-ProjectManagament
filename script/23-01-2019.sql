USE [master]
GO
/****** Object:  Database [PM]    Script Date: 23.01.2019 09:07:14 ******/
CREATE DATABASE [PM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'aspnet-Web-20190111025246.mdf', FILENAME = N'C:\Databases\aspnet-Web-20190111025246.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'aspnet-Web-20190111025246_log.ldf', FILENAME = N'C:\Databases\aspnet-Web-20190111025246_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PM] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PM] SET ARITHABORT OFF 
GO
ALTER DATABASE [PM] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PM] SET TRUSTWORTHY ON 
GO
ALTER DATABASE [PM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PM] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PM] SET  MULTI_USER 
GO
ALTER DATABASE [PM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PM] SET QUERY_STORE = OFF
GO
USE [PM]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [PM]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [nvarchar](128) NOT NULL,
	[Message] [varchar](7) NOT NULL,
	[Completed] [bit] NOT NULL,
 CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Icon] [nvarchar](128) NOT NULL,
	[Controller] [nvarchar](128) NOT NULL,
	[Action] [nvarchar](128) NOT NULL,
	[Nested] [nvarchar](128) NOT NULL,
	[Rank] [int] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [nvarchar](128) NOT NULL,
	[User_Id] [nvarchar](128) NULL,
	[Title] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [date] NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [nvarchar](128) NOT NULL,
	[User_Id] [nvarchar](128) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenu]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenu](
	[Id] [nvarchar](128) NOT NULL,
	[Role_Id] [nvarchar](128) NOT NULL,
	[Menu_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_RoleMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerInfo]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerInfo](
	[Id] [nvarchar](128) NOT NULL,
	[key_str] [nvarchar](50) NOT NULL,
	[value_str] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ServerInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkDefinition]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkDefinition](
	[Id] [nvarchar](128) NOT NULL,
	[User_Id] [nvarchar](128) NOT NULL,
	[Project_Id] [nvarchar](128) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ExpireDate] [date] NOT NULL,
	[Rank] [int] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_WorkDefinition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201901111253444_InitialCreate', N'Web.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F20FF20E82959382D5F7606B3463B81D3B6B3C68E2F98F664F336604BECB63012A548946363912FDB87FDA4FD852D4AD48D1789EA96BBDBC1008316593C552C16C962B1E8FFFDE7BFD31F9FC3C07AC249EA47E4CC3E9A1CDA16266EE4F96475666774F9FD07FBC71FBEF9CBF4D20B9FAD5F4ABA1346072D497A663F521A9F3A4EEA3EE210A593D07793288D9674E246A183BCC8393E3CFCBB7374E46080B001CBB2A69F3242FD10E71FF0398B888B639AA1E026F27090F272A899E7A8D62D0A711A23179FD9FFC28B4941655BE7818F4082390E96B685088928A220DFE9E714CF691291D53C8602143CBCC418E89628483197FBB42637EDC2E131EB8253372CA1DC2CA5513810F0E884EBC4119BAFA559BBD21968ED12B44B5F58AF73CD9DD9D71ECE8B3E4501284064783A0B12467C66DF542CCED3F816D349D97052405E2500F77B947C9D34110F2CE37607950D1D4F0ED9BF036B9605344BF019C1194D507060DD678BC077FF895F1EA2AF989C9D1C2D96271FDEBD47DEC9FBBFE19377CD9E425F81AE550045F74914E30464C3CBAAFFB6E5B4DB3962C3AA59A34DA115B025980EB675839E3F62B2A28F30518E3FD8D695FF8CBDB2841BD767E2C3EC814634C9E0F3360B02B4087055EF74F264FF77703D7EF77E14AEB7E8C95FE5432FF0878993C0BCFA8483BC367DF4E3627AB5C6FB0B27BB4AA2907DB7EDABA8FD328FB2C4659D89B4240F285961DA966EEAD4C66B64D20C6A7CB32E51F7DFB499A4B2792B495987D69909258B6DCF8652DED7E56B6C71E7710C83979B16D34897C1D59BD444687560415D6D2A47A6A642A00B7FE695EF32447E30C2D267C005BC8DA59F84B8EAE54F11181A228365BE47690A33DFFB074A1F3B44879F23883EC76E968041CE290AE357E776FF18117C9B850B66E7DBE335DAD03CFC1E5D219746C92561AD36C6FB18B95FA38C5E12EF0251FC99BA2520FB7CF043738051C439775D9CA65760CCD89B45E04C9780D7849E1C0F86630BD3AE5D8F5980FC50ED7B084BE89792B4F63FD414920FA22153F9215DA27E8C563E3113B524D58B5A50F48ACAC9868ACAC0CC24E5947A4173825E390BAAD13CBB7C84C677ED72D8FDF7ED36DBBC756B41438D735821F1CF98E0049631EF1E518A13528F80C9BAB10B67211F3EC6F4D5F7A69CD32F28C8C666B5D66CC81781F167430EBBFFB32117138A9F7C8F792506079E9218E08DE8D567A9FE392748B6EDE9D0EAE6B6996F670DD04D97F3348D5C3F9F058A50170F54B4E5071FCEEA8F5A14BD11231FD03130749F6D7950027DB345A3BA231738C0145BE76E110A9CA1D4459EAC46E8903740B072475508564740DAC2FD55E209968E13D608B143500A33D527549E163E71FD1805BD5A125A1A6E61ACEF150FB1E602C7983086BD9A3061AE0E7830012A3EC2A0F46968EA342CAEDB10355EAB6ECCFB5CD87ADCA538C4566CB2C777D6D825F7DF5EC530BB35B605E3EC56898900DAE0DD2E0C949F554C0D403CB8EC9B810A27268D8172976A2B06DAD6D80E0CB4AD923767A0C511D574FC85F3EABE9967FBA0BCFD6DBD535D3BB0CD963EF6CC340BDF13DA50688113D93C2F16AC123F53C5E10CE4E4E7B394BBBAA28930F039A6ED904DEDEF2AFD50A71B4434A22EC0DAD07A40F9B59F04244DA801C295B1BC4EE9B8173100B68CBB75C2F2B55F806DD8808CDDBCFE6C10EA2F4945E3343A7D543DABAC413272A3C34203476110E2E2D5EEB88152747159593126BEF0106FB8D1313E181D0AEAF15C354A2A3B33BA964AD3ECD792CA211BE2926DA425C17DD268A9ECCCE85AE236DAAF24855330C02DD84845ED2D7CA4C956463AAADDA6AA9B3A4536142F983A9AB4A9E90D8A639FAC1A6954BCC49A173954B3EFE7C3938CC202C3715345AE51256DC58946095A61A1165883A4577E92D20B44D102B138CFCC0B2532E5DEAA59FE4B96CDED531EC4721F28A9D9EFA245E3B2BEB5C7CA4E086F7B053D0B99279387CF15E3AE6E6EB1743614A04411B19F45411612BD63A56F5DDCDB35DB172532C2D411E4971C27494B927BDB56B9D180C89361C3C1A9FC95F507480FA15373E96D3615ADF340F5286540AA89A20B52ED6CC0748E8BD12089BEE0F031EA45789D79C413509A00BC682046238741026BD499A3B6D34C9A98ED1A73442197A40929540D90B29931D212B259B1169E46A36A0A730E728E48135DAE354756648B34A115D56B602B6416EBCC511509254D6045B539769D5D222E9E7BBC53694F2883B7AAE2F0BAD95EA5C1789D95709CADAE7147DF046A140FC4E2B7F012182FDF4B2BD29EE0065B5111ABD8CC8A3418FA95A675ABDD5E683AAFE2F598ADABEAD662DE7555AFC71B66ABAF6A11D2C14D24A9B8570738E1A036E587A6FE4730D229AA20B1AD528DB091BFA4148713463099FF16CC021FB365BB24B841C45FE29416E919F6F1E1D1B1F09E667FDEB63869EA058A43A7EE814B7BCCB69069459E50E23EA244CE7BD8E0FD470D2A8594AF89879FCFEC7FE7AD4EF3E804FB95171F58D7E967E2FF9641C5439261EB0F398F739C7C7883171895A07FBC89A70DE62ABFFEF54BD1F4C0BA4B603A9D5A8782A2D719FEF6838741D2144D379066ED67106F77B6B5DE1C285185D9B2FE1383854F47795E504AF96D889EBF1B2A9AF209C146888A670263E18DA242DD338075B0B44F003CF8A4F91380619D553F09584734ED73009F0C07131F03982F4365CB1DEE438A83D23696A45CCFBDC9D41B6556EE7A6F9272AE379AE8725EF500B85173A7377351DE584EF2685BA722E57834EC5DDAFDABE719EF4B6A71EDB4EF36A3789B49C41D17477FAADCE13DC8765364EFEC3E4378DBB6A68BFCEE799AE5B03CE03D3336BECDEF3EDB77DBC6A60B10EFB9B10DCAE9DD335BDBD5FEB9634B33DE42779EA12B271B696E705451E4BE0CDC22E40EC7FF450446507894C5C34975CA5757BA6A0FC39A44CF549F6B263296268EC457A2E8663BACAF7CC3EFEC2CA7E966ABC9D0ECE2CDD7FF4EDE9CA69BB726EF7117B9C3CACC43553E77CF3AD69526F59672855B3DE9494DEFF3593BAFE3DF526AF0284A69CD1ECDEDF2DBC9041E4525634E9D0199BFF24531EC9D8DBFA908FB77EAAF6A08F6171609765BBB6645734D9651B9790B1295244284E60653E4C1967A9E507F895C0AD52C009DBFFCCE837AEC1A6481BD6B7297D138A3D0651C2E8256C08B39015DFCF3F4E6B6CCD3BB987DA5637401C4F459E0FE8EFC94F98157C97DA5880969209877C1C3BD6C2C290BFBAE5E2AA4DB88180271F5554ED1030EE300C0D23B32474F781DD9C0FC3EE215725FEA08A00EA47F20DA6A9F5EF86895A030E518757BF8041BF6C2E71FFE0F5E5D9F325A540000, N'6.2.0-61023')
SET IDENTITY_INSERT [dbo].[Action] ON 

INSERT [dbo].[Action] ([Id], [User_Id], [Message], [Completed]) VALUES (15, N'e20c5a66-a26a-4e96-b4a1-d86cc9846ce9', N'7ac2535', 1)
SET IDENTITY_INSERT [dbo].[Action] OFF
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'240F5858-108B-4733-B930-CAF4D3939D6E', N'Kullanici')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e20c5a66-a26a-4e96-b4a1-d86cc9846ce9', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e20c5a66-a26a-4e96-b4a1-d86cc9846ce9', N'faruk_thecno@hotmail.com', 0, N'AHA6XMYRoXkPiAOETs2AQWMmribfGqrpzJ8NFTRmrKvRdDJP3Lt7oEuiDN9T7W0N8w==', N'aea66010-62be-4b92-98e0-c56b837e49fb', NULL, 0, 0, NULL, 1, 0, N'faruk_thecno@hotmail.com')
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'1BAA792F-50B0-44E2-A22B-7D6B1661D25A', N'Parametreler', N'fas fa-server', N'Server', N'Index', N'6796F715-90FB-498B-AAC2-F496D37BB204', 7)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'3848AFAC-90AC-49DC-9D07-02AF54C03D7A', N'Yetkilendirme', N'fas fa-shield-alt', N'Protection', N'Index', N'6796F715-90FB-498B-AAC2-F496D37BB204', 11)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'6796F715-90FB-498B-AAC2-F496D37BB204', N'Sunucu Yonetimi', N'fas fa-server', N'Server', N'Index', N'0', 6)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'679E6404-4071-4BB5-A4C0-3DC5591D05C3', N'Dosyalarim(Kodlanmadı!)', N'fas fa-folder', N'My', N'File', N'850A7054-69F1-45E8-A652-B571FC635439', 4)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'7d302cb5-bf3e-4ade-9ef3-2434c5689332', N'Projelerim', N'fas fa-project-diagram', N'Project', N'Index', N'850A7054-69F1-45E8-A652-B571FC635439', 5)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'850A7054-69F1-45E8-A652-B571FC635439', N'Bana Ozel', N'fas fa-home', N'Home', N'Index', N'0', 1)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'9E101ACA-39C9-4B76-82A9-811AF631D3DB', N'Menu Yonetimi', N'fas fa-ellipsis-v', N'Menu', N'Index', N'6796F715-90FB-498B-AAC2-F496D37BB204', 8)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'A5AD4C24-A082-454D-B4D4-47FD6B9B1799', N'Is Tanimlamalarim', N'fas fa-network-wired', N'Works', N'Index', N'850A7054-69F1-45E8-A652-B571FC635439', 3)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'EFC3DD70-E513-4ED2-AF30-84BCFA6A7887', N'Kullanici Yonetimi', N'fas fa-users', N'Users', N'Index', N'6796F715-90FB-498B-AAC2-F496D37BB204', 10)
INSERT [dbo].[Menu] ([Id], [Name], [Icon], [Controller], [Action], [Nested], [Rank]) VALUES (N'F84CEBE6-6DD0-4B7F-934F-DA7B9824BD2F', N'Notlarim', N'fas fa-book-open', N'Notebook', N'Index', N'850A7054-69F1-45E8-A652-B571FC635439', 2)
INSERT [dbo].[Projects] ([Id], [User_Id], [Name]) VALUES (N'4991bfcc-22d2-49d1-b4d2-7c16620deec6', N'e20c5a66-a26a-4e96-b4a1-d86cc9846ce9', N'Proje Yönetim Sistemi')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'042406a7-f1fc-4600-8a0f-47c6b1798493', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'7d302cb5-bf3e-4ade-9ef3-2434c5689332')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'095667bd-0bce-4979-bb17-d2836de6e845', N'240F5858-108B-4733-B930-CAF4D3939D6E', N'7d302cb5-bf3e-4ade-9ef3-2434c5689332')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'11b1f781-6f5a-4a48-a86b-57ab999e893c', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'A5AD4C24-A082-454D-B4D4-47FD6B9B1799')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'12905f89-0d77-420f-877e-c80c3f033d06', N'240F5858-108B-4733-B930-CAF4D3939D6E', N'F84CEBE6-6DD0-4B7F-934F-DA7B9824BD2F')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'236c66da-4dd0-4c51-a425-d1eefdab4cc7', N'240F5858-108B-4733-B930-CAF4D3939D6E', N'A5AD4C24-A082-454D-B4D4-47FD6B9B1799')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'472156f5-ff1c-4c0f-bb7f-0a97c406ab1d', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'6796F715-90FB-498B-AAC2-F496D37BB204')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'488c2812-8178-473a-a091-0fc4e28005d8', N'240F5858-108B-4733-B930-CAF4D3939D6E', N'850A7054-69F1-45E8-A652-B571FC635439')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'5e09e38e-83db-40a4-95d6-1157113d3204', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'9E101ACA-39C9-4B76-82A9-811AF631D3DB')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'7e4232ae-6baf-4699-a303-1f4c3e36fc3b', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'F84CEBE6-6DD0-4B7F-934F-DA7B9824BD2F')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'b952cd60-aa74-43e7-8d9f-35719d66a286', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'EFC3DD70-E513-4ED2-AF30-84BCFA6A7887')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'b9d76fcc-e22f-455f-93de-bec38a872ee6', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'3848AFAC-90AC-49DC-9D07-02AF54C03D7A')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'd53ac5b1-699b-4bbe-a53b-2fa2c6f93839', N'240F5858-108B-4733-B930-CAF4D3939D6E', N'679E6404-4071-4BB5-A4C0-3DC5591D05C3')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'f1849049-afe6-4205-bc11-63c047df0ec2', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'679E6404-4071-4BB5-A4C0-3DC5591D05C3')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'fc0b0462-43aa-475c-b30e-cf416e873f31', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'1BAA792F-50B0-44E2-A22B-7D6B1661D25A')
INSERT [dbo].[RoleMenu] ([Id], [Role_Id], [Menu_Id]) VALUES (N'ff108f44-9bae-4237-b895-6f450f079457', N'4549AB1D-ABB4-4DF7-B5F1-9E49A56B9D94', N'850A7054-69F1-45E8-A652-B571FC635439')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'095f1545-d6d5-4f3c-85ba-6e9cbb8a3b1c', N'smtp_password', N'03102593')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'33f38287-d327-473b-95f1-246434a3a04a', N'smtp_host', N'smtp.hostinger.com')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'81534a11-0340-43f0-8432-b9d261a69d49', N'version', N'1')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'854bbc30-94f3-4133-94f8-3d65d5caa496', N'smtp_delay', N'1')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'8eea9f33-5222-448c-932e-79a0414a5c9b', N'smtp_sender', N'pm@cyberguilty.com')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'b5ef451f-657d-4e7d-b5fb-3fd3d12b0748', N'smtp_port', N'587')
INSERT [dbo].[ServerInfo] ([Id], [key_str], [value_str]) VALUES (N'cf2f3bcd-8e32-4613-ab94-156d2d4e3a53', N'workdef_delay', N'1')
INSERT [dbo].[WorkDefinition] ([Id], [User_Id], [Project_Id], [Title], [Description], [CreatedDate], [ExpireDate], [Rank], [IsCompleted]) VALUES (N'24b4d9d2-44ff-475c-8fd8-f6ca1ef89245', N'e20c5a66-a26a-4e96-b4a1-d86cc9846ce9', N'4991bfcc-22d2-49d1-b4d2-7c16620deec6', N'Is Tanimi 21.01.2019', N'<p></p><ol><li>

<b>Sistemde Bulunan Veri Sayilarini Admin Panelinde Dash Üzerinden Görüntüle.
</b>
<br></li><li><b>Hesabim ve Sifremi Unuttum Panelini Kodla.</b></li><li><b>Sifremi Unuttum Kismina Bildirim Gönderme Isini Action Tablosuna Gönder</b></li></ol><p></p>', CAST(N'2019-01-21' AS Date), CAST(N'2019-01-25' AS Date), 2, 1)
INSERT [dbo].[WorkDefinition] ([Id], [User_Id], [Project_Id], [Title], [Description], [CreatedDate], [ExpireDate], [Rank], [IsCompleted]) VALUES (N'c814ae08-cb93-4e28-9e76-934e0a71adb2', N'e20c5a66-a26a-4e96-b4a1-d86cc9846ce9', N'4991bfcc-22d2-49d1-b4d2-7c16620deec6', N'Is Tanimi 15.01.2019', N'<p></p><ol><li><b>Is Tanimlamalarini Silme Menüsü Kodlanacak. (Tamamlandi!)</b></li><li><b>Kullanicilarin Periyodik Olarak Bitirmedikleri Is Tanimlari Onlara Analiz Edilip Mail Olarak Gönderilecek. (Tamamlandi)</b></li><li><b>Task Scheduler Servisi Optimize Edilecek. (Tamamlandi!)</b></li><li><b>Kullanici Web Arayüzü Içerisinde Notlarim Kismini Hala Kodlamadim. Notlar Kismi Kodlanacak.(Tamamlandi).&nbsp;</b></li><li><b></b><b>Notlarim Kisminda Json Data Isleme Hala Kodlanmadi. (Tamamlandi)</b><b></b></li><li>I<b>s Tanimlamalari Menüsünde Json Islemleri Yapilacak. (Tamamlandi)</b><b></b></li><li><b>Is Tanimlamalarina Önem Sirasi Low - Medium - High Seklinde Kodlanacak.(Tamamlandi)</b></li><li><b>Sifre Güvenligi Sebebiyle Sifresini Unutan veya Yeni Olusturulan Kullanicilarin Sifreleri Belirli Periyotlarla Action Tablosundan Silinecek. (Tamamlandi)</b></li><li><b>Kullanicilara Gönderilen Elektronik Postalari Sablon Haline Getir. (Tamamlandi)</b></li></ol><br><p></p>', CAST(N'2019-01-15' AS Date), CAST(N'2019-01-01' AS Date), 3, 1)
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 23.01.2019 09:07:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 23.01.2019 09:07:14 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 23.01.2019 09:07:14 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 23.01.2019 09:07:14 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 23.01.2019 09:07:14 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 23.01.2019 09:07:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_AspNetUsers] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_AspNetUsers] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_AspNetUsers]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_AspNetUsers] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_AspNetUsers]
GO
ALTER TABLE [dbo].[RoleMenu]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenu_AspNetRoles] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleMenu] CHECK CONSTRAINT [FK_RoleMenu_AspNetRoles]
GO
ALTER TABLE [dbo].[RoleMenu]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenu_Menu] FOREIGN KEY([Menu_Id])
REFERENCES [dbo].[Menu] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleMenu] CHECK CONSTRAINT [FK_RoleMenu_Menu]
GO
ALTER TABLE [dbo].[WorkDefinition]  WITH CHECK ADD  CONSTRAINT [FK_WorkDefinition_AspNetUsers] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[WorkDefinition] CHECK CONSTRAINT [FK_WorkDefinition_AspNetUsers]
GO
ALTER TABLE [dbo].[WorkDefinition]  WITH CHECK ADD  CONSTRAINT [FK_WorkDefinition_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkDefinition] CHECK CONSTRAINT [FK_WorkDefinition_Projects]
GO
/****** Object:  StoredProcedure [dbo].[SP_Action]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Action]
AS
DECLARE @MIN AS INT
DECLARE @MAX AS INT
SELECT @MIN = MIN(act.Id),@MAX = MAX(act.Id) FROM Action act WHERE Completed='0';
SELECT act.Id,u.Email,act.Message FROM Action act
INNER JOIN AspNetUsers u on u.Id = act.User_Id
 WHERE Completed='0'
UPDATE Action SET Completed='1' WHERE Id>=@MIN AND Id<=@MAX
GO
/****** Object:  StoredProcedure [dbo].[SP_STATISTIC]    Script Date: 23.01.2019 09:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[SP_STATISTIC]
AS
SELECT (SELECT COUNT(*) FROM Action) AS 'ACTION_COUNT',
(SELECT COUNT(*) FROM AspNetUsers) AS 'USER COUNT',
(SELECT COUNT(*) FROM Notes) AS 'NOTE COUNT',
(SELECT COUNT(*) FROM Projects) AS 'PROJECT COUNT',
(SELECT COUNT(*) FROM ServerInfo) AS 'PARAMETER COUNT',
(SELECT COUNT(*) FROM WorkDefinition) AS 'WORKDEF COUNT'
GO
USE [master]
GO
ALTER DATABASE [PM] SET  READ_WRITE 
GO

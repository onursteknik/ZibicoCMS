
CREATE TABLE [dbo].[Announcements](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AnnouncementTitle] [nvarchar](150) NULL,
	[AnnouncementContent] [ntext] NULL,
	[CreateDate] [datetime] NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Announcements_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_Announcements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BlogTitle] [nvarchar](150) NULL,
	[BlogContent] [ntext] NULL,
	[BlogImageURL] [nvarchar](500) NULL,
	[BlogSeoURL] [ntext] NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Blogs_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](150) NULL,
	[CategoryDescription] [ntext] NULL,
	[CategoryIcon] [nvarchar](100) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Categories_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Editors]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Editors](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Nickname] [nvarchar](50) NULL,
	[Password] [char](32) NULL,
	[Email] [nvarchar](50) NULL,
	[ImageURL] [nvarchar](500) NULL,
	[HomeAddress] [ntext] NULL,
	[StaffDescription] [ntext] NULL,
	[SecretKey] [uniqueidentifier] NULL CONSTRAINT [DF_Editors_SecretKey]  DEFAULT (newid()),
	[Status] [bit] NULL CONSTRAINT [DF_Editors_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_Editors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GeneralSettings]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralSettings](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SettingKey] [nvarchar](500) NULL,
	[SettingValue] [nvarchar](500) NULL,
 CONSTRAINT [PK_GeneralSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuFoods]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuFoods](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FoodTitle] [nvarchar](500) NULL,
	[FoodDescription] [ntext] NULL,
	[FoodPrice] [money] NULL,
	[FoodImageURL] [nvarchar](1000) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_MenuFoods_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_MenuFoods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhotoGalleries]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoGalleries](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ImageURL] [nvarchar](1000) NULL,
	[ImageDescription] [ntext] NULL,
	[Status] [bit] NULL CONSTRAINT [DF_PhotoGalleries_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_PhotoGallery] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryID] [bigint] NULL,
	[ProductName] [nvarchar](250) NULL,
	[ProductDescription] [ntext] NULL,
	[ProductImageURL] [nvarchar](500) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Products_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](150) NULL,
	[PhoneNumber] [nvarchar](150) NULL,
	[PersonCount] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SocialMedia]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialMedia](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PlatformName] [nvarchar](50) NULL,
	[PlatformIcon] [nvarchar](150) NULL,
	[AccountName] [nvarchar](150) NULL,
	[AccountURL] [nvarchar](500) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_SocialMedia_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_SocialMedia] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staffs]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffName] [nvarchar](50) NULL,
	[StaffRole] [nvarchar](150) NULL,
	[StaffImageURL] [nvarchar](500) NULL,
	[StaffOrderNumber] [int] NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Staffs_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_Staffs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StaffSocialMedia]    Script Date: 3.02.2018 22:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffSocialMedia](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffID] [bigint] NULL,
	[PlatformName] [nvarchar](50) NULL,
	[PlatformIcon] [nvarchar](150) NULL,
	[AccountName] [nvarchar](150) NULL,
	[AccountURL] [nvarchar](500) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_StaffSocialMedia] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Announcements] ON 

INSERT [dbo].[Announcements] ([ID], [AnnouncementTitle], [AnnouncementContent], [CreateDate], [Status]) VALUES (1, N'Duyurular', N'Bu bölümde aşağıdaki blog kısmında paylaşmak istemediğiniz,restaurantınıza ait duyurulara vs. yer verebilirsiniz.', CAST(N'2018-01-13 00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Announcements] OFF
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([ID], [BlogTitle], [BlogContent], [BlogImageURL], [BlogSeoURL], [Status]) VALUES (1, N'Lorem ipsum dolor sit amet consectetur adipisicing elit iusto!(CMS)', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. It has survived not only five centuries.', N'/Content/img/blog-1.jpg', NULL, 1)
INSERT [dbo].[Blogs] ([ID], [BlogTitle], [BlogContent], [BlogImageURL], [BlogSeoURL], [Status]) VALUES (2, N'Lorem ipsum dolor sit amet consectetur adipisicing elit iusto!', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. It has survived not only five centuries.', N'/Content/img/blog-2.jpg', NULL, 1)
INSERT [dbo].[Blogs] ([ID], [BlogTitle], [BlogContent], [BlogImageURL], [BlogSeoURL], [Status]) VALUES (3, N'Lorem ipsum dolor sit amet consectetur adipisicing elit iusto!(CMS3)', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. It has survived not only five centuries.', N'/Content/img/blog-3.jpg', NULL, 1)
SET IDENTITY_INSERT [dbo].[Blogs] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [CategoryName], [CategoryDescription], [CategoryIcon], [Status]) VALUES (8, N'Burger', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. This survived five centuries.', N'/Uploads/bf711341-b82f-4c99-82eb-aae8f33111e7.png', 1)
INSERT [dbo].[Categories] ([ID], [CategoryName], [CategoryDescription], [CategoryIcon], [Status]) VALUES (9, N'Steaks', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. This survived five centuries.', N'/Uploads/069531e3-68d7-4456-8404-7a08d347135b.png', 1)
INSERT [dbo].[Categories] ([ID], [CategoryName], [CategoryDescription], [CategoryIcon], [Status]) VALUES (10, N'Delicous food', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. This survived five centuries.', N'/Uploads/b56042e4-9dba-471b-8c7e-e123b70757f8.png', 1)
INSERT [dbo].[Categories] ([ID], [CategoryName], [CategoryDescription], [CategoryIcon], [Status]) VALUES (11, N'Cake', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. This survived five centuries.', N'/Uploads/b56042e4-9dba-471b-8c7e-e123b70757f8.png', 1)
INSERT [dbo].[Categories] ([ID], [CategoryName], [CategoryDescription], [CategoryIcon], [Status]) VALUES (12, N'Coffee', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. This survived five centuries.', N'/Uploads/b56042e4-9dba-471b-8c7e-e123b70757f8.png', 1)
INSERT [dbo].[Categories] ([ID], [CategoryName], [CategoryDescription], [CategoryIcon], [Status]) VALUES (13, N'Chicken', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry..This survived five centuries.', N'/Uploads/b56042e4-9dba-471b-8c7e-e123b70757f8.png', 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Editors] ON 

INSERT [dbo].[Editors] ([ID], [Firstname], [Lastname], [Nickname], [Password], [Email], [ImageURL], [HomeAddress], [StaffDescription], [SecretKey], [Status]) VALUES (1, N'Admin', N'Admin', N'admin', N'25F9E794323B453885F5181F1B624D0B', N'test@test.com', N'/Content/img/user1.png', NULL, NULL, N'b470340b-6e76-4c1d-b242-c21eaa104776', 1)
SET IDENTITY_INSERT [dbo].[Editors] OFF
SET IDENTITY_INSERT [dbo].[GeneralSettings] ON 

INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (1, N'WebSiteLogoURL', N'/Uploads/c1f0a2d8-9c76-4d81-bde7-9b3d720b2e04.png')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (2, N'WebSiteTitle', N'Zibico CMS - Restaraunt')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (3, N'HomepageDescription', N'Restaurantınızla alakalı açıklamayı bu kısıma yazabilirsiniz. <br> Örneğin: "Anadolu ve Osmanlı mutfağının en özel lezzetlerini sunuyoruz."')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (4, N'AboutTitle', N'Restaurantımıza Hoşgeldiniz!')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (5, N'AboutDescription', N'Bu kısımda restaurantınızla alakalı genel bilgiler verebilirsiniz. Genel hatlarıyla menü, konsept, mekan ve restaurantınızın bulunduğu lokasyonla alakalı tanıtıcı bir yazı yazılabilir... <br>Sağ tarafta bulunan kısıma dilediğiniz 2 resim yükleyebilir ve tanıtım yazınızı resimle zenginleştirebilirsiniz.')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (6, N'AboutImage1URL', N'/Content/img/about-1.jpg')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (7, N'AboutImage2URL', N'/Content/img/about-2.jpg')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (8, N'MenuTitle', N'Menümüz')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (9, N'MenuDescription', N'Bu kısımda restaurantınızın menüsü hakkında kısaca bilgi verebilirsiniz.<br> Menünüz hakkında vereceğiniz kısa bilgiler müşterilerinizin kafasındaki soru işaretlerinin gitmesi için faydalı olabilir...')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (10, N'ShineProductID', N'1')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (11, N'StaffTitle', N'Ekibimiz')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (12, N'StaffDescription', N'Bu kısımda restaurant ekibinizden 1-3 kişinin İsim,Fotoğraf ve Rol bilgilerini gösterebilirsiniz.')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (13, N'GalleryTitle', N'Foto Galeri')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (14, N'GalleryDescription', N'Restaurantınıza ait, mutfak,yemek,dış cephe vs.  tüm fotoğrafları bu kısımda paylaşabilirsiniz.')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (15, N'ReservationTitle', N'Çevrimiçi Rezervasyon')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (16, N'ReservationDescription', N'Bu kısıma rezervasyon işlemi ile alakalı açıklama yazabilirsiniz, müşterilerinizi süreç hakkında bilgilendirebilirsiniz.')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (17, N'BlogTitle', N'Restaurantımızla İlgili Son Haberler')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (18, N'BlogDescription', N'Bu kısımda restaurantınızla ilgili kampanyalar veya haberlere yer verebilirsiniz.')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (19, N'MapTitle', N'Bizi Ziyaret Edin')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (20, N'MapAddress', N'Bu kısıma restaurantınızın adresini yazabilirsiniz.')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (21, N'OpeningHoursTitle', N'Mesai Saatlerimiz')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (22, N'OpeningHoursWeekdays', N'PAZARTESİ - CUMA | 10:00 - 23:00')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (23, N'OpeningHoursWeekend', N'CUMARTESİ - PAZAR | 10:00 - 22:00')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (24, N'HomepageImageURL', N'/Content/img/hero.jpg')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (25, N'MenuImageURL', N'/Uploads/2a6c6b05-258f-431b-bd32-4bf089eed438.jpg')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (26, N'Announcement', N'/content/img/event.jpg')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (27, N'MapLatitude', N'41.04473')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (28, N'MapLongitude', N'28.97930')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (29, N'MetaNameDescription', N'MetaTags')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (30, N'MetaNameKeywords', N'MetaTags')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (31, N'FaviconURL', N'/uploads/632e90e6-3523-45df-99ee-e17bdb85b365.ico')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (32, N'CategoriesTitle', N'Ürün Yelpazesi')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (33, N'CategoryDescription', N'Bu kısımda restaurantınızdaki ürün çeşitliliğini kısaca özetleyebilirsiniz.<br> Örn: "Restaurantımız Fast Food''tan Anadolu''nun Yöresel lezzetlerine kadar geniş bir ürün yelpazesine sahiptir..."')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (34, N'AdminPath', N'http://manage.restaurant.zibico.com')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (35, N'WebUIPath', N'http://restaurant.zibico.com')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (36, N'FTPAddress', N'ftp://restaurant.zibico.com')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (37, N'FTPUsername', N'FTPUSERNAME')
INSERT [dbo].[GeneralSettings] ([ID], [SettingKey], [SettingValue]) VALUES (38, N'FTPPassword', N'FTPPASSWORD')
SET IDENTITY_INSERT [dbo].[GeneralSettings] OFF
SET IDENTITY_INSERT [dbo].[MenuFoods] ON 

INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (1, N'Double Grape Martani', N'Lorem Ipsum is simply dummy text printing and industry.(CMS)', 105.0000, N'/Content/img/food-1.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (2, N'Buttered Popcorn', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-2.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (3, N'Masala Dosa', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-3.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (4, N'Double Grape Martani', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-4.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (5, N'Seafood Paella', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-5.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (6, N'Chicken Rice', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-6.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (7, N'Double Grape Martani', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-7.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (8, N'Poutine Canada', N'Lorem Ipsum is simply dummy text printing and industry.', 105.0000, N'/Content/img/food-8.jpg', 1)
INSERT [dbo].[MenuFoods] ([ID], [FoodTitle], [FoodDescription], [FoodPrice], [FoodImageURL], [Status]) VALUES (9, N'Grape Martani', N'Lorem Ipsum is simply dummy text printing and industry.', 120.0000, N'/Content/img/food-9.jpg', 1)
SET IDENTITY_INSERT [dbo].[MenuFoods] OFF
SET IDENTITY_INSERT [dbo].[PhotoGalleries] ON 

INSERT [dbo].[PhotoGalleries] ([ID], [ImageURL], [ImageDescription], [Status]) VALUES (1, N'/Content/img/gallery-1.jpg', N'Item1', 1)
INSERT [dbo].[PhotoGalleries] ([ID], [ImageURL], [ImageDescription], [Status]) VALUES (2, N'/Content/img/gallery-2.jpg', NULL, 1)
INSERT [dbo].[PhotoGalleries] ([ID], [ImageURL], [ImageDescription], [Status]) VALUES (3, N'/Content/img/gallery-3.jpg', NULL, 1)
INSERT [dbo].[PhotoGalleries] ([ID], [ImageURL], [ImageDescription], [Status]) VALUES (4, N'/Content/img/gallery-4.jpg', NULL, 1)
INSERT [dbo].[PhotoGalleries] ([ID], [ImageURL], [ImageDescription], [Status]) VALUES (5, N'/Content/img/gallery-5.jpg', NULL, 1)
INSERT [dbo].[PhotoGalleries] ([ID], [ImageURL], [ImageDescription], [Status]) VALUES (6, N'/Content/img/gallery-6.jpg', NULL, 1)
SET IDENTITY_INSERT [dbo].[PhotoGalleries] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ID], [CategoryID], [ProductName], [ProductDescription], [ProductImageURL], [Status]) VALUES (1, 8, N'Öne Çıkan Ürün', N'Bu kısımda öne çıkmasını istediğiniz ürünü kontrol panelinizden seçebilirsiniz. <br> Bu ürün, Şefin Önerisi, Ayın Ürünü gibi etiketlediğiniz ürününüz olabilir.', N'/Uploads/burger.png', 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[SocialMedia] ON 

INSERT [dbo].[SocialMedia] ([ID], [PlatformName], [PlatformIcon], [AccountName], [AccountURL], [Status]) VALUES (1, N'Facebook', N'social_facebook', N'Zibico', N'http://www.facebook/Zibico', 1)
INSERT [dbo].[SocialMedia] ([ID], [PlatformName], [PlatformIcon], [AccountName], [AccountURL], [Status]) VALUES (2, N'Twitter', N'social_twitter', N'Zibico', N'http://www.twitter.com/ZibicoTwitter', 1)
INSERT [dbo].[SocialMedia] ([ID], [PlatformName], [PlatformIcon], [AccountName], [AccountURL], [Status]) VALUES (3, N'Google+', N'social_googleplus', N'Zibico', N'http://www.google.com/ZibicoGooglePlus', 1)
INSERT [dbo].[SocialMedia] ([ID], [PlatformName], [PlatformIcon], [AccountName], [AccountURL], [Status]) VALUES (4, N'Pinterest', N'social_pinterest', N'Zibico', N'http://www.pinterest.com/Zibico', 1)
INSERT [dbo].[SocialMedia] ([ID], [PlatformName], [PlatformIcon], [AccountName], [AccountURL], [Status]) VALUES (6, N'Instagram', N'social_instagram', N'Zibico', N'http://www.instagram.com/Zibico', 1)
SET IDENTITY_INSERT [dbo].[SocialMedia] OFF
SET IDENTITY_INSERT [dbo].[Staffs] ON 

INSERT [dbo].[Staffs] ([ID], [StaffName], [StaffRole], [StaffImageURL], [StaffOrderNumber], [Status]) VALUES (1, N'Michel Brown', N'Executive Chef', N'/Content/img/stuff-1.jpg', 1, 1)
INSERT [dbo].[Staffs] ([ID], [StaffName], [StaffRole], [StaffImageURL], [StaffOrderNumber], [Status]) VALUES (2, N'Jonathan Smith', N'Executive Chef', N'/Content/img/stuff-2.jpg', 3, 1)
INSERT [dbo].[Staffs] ([ID], [StaffName], [StaffRole], [StaffImageURL], [StaffOrderNumber], [Status]) VALUES (3, N'Benderd Wilson', N'Executive Chef', N'/Content/img/stuff-3.jpg', 2, 1)
SET IDENTITY_INSERT [dbo].[Staffs] OFF
ALTER TABLE [dbo].[StaffSocialMedia] ADD  CONSTRAINT [DF_StaffSocialMedia_Status]  DEFAULT ((1)) FOR [Status]
GO
USE [master]
GO
ALTER DATABASE [ZibicoCMS] SET  READ_WRITE 
GO

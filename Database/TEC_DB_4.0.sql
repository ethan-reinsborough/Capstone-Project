
USE [master]
GO
/****** Object:  Database [TEC]    Script Date: 2021-04-30 12:01:54 AM ******/

USE master
GO
if exists (select * from sysdatabases where name='TEC')
		drop database TEC
go
CREATE DATABASE [TEC]
GO
USE [TEC]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](255) NOT NULL,
	[DepartmentDescription] [varchar](255) NOT NULL,
	[InvocationDate] [datetime] NOT NULL,
	[RecordVersion] [timestamp] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(10000000,1) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[MI] [varchar](10) NULL,
	[LastName] [varchar](255) NOT NULL,
	[SIN] [varchar](255) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[StreetAddress] [varchar](255) NOT NULL,
	[Municipality] [varchar](255) NOT NULL,
	[Province] [varchar](255) NOT NULL,
	[Country] [varchar](255) NOT NULL,
	[PostalCode] [varchar](255) NOT NULL,
	[SeniorityDate] [datetime] NOT NULL,
	[JobStartDate] [datetime] NOT NULL,
	[WorkPhone] [varchar](255) NOT NULL,
	[CellPhone] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Supervisor] [int] NULL,
	[Active][bit] NOT NULL,
	[RecordVersion] [timestamp] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobID] [int] NOT NULL,
	[JobName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[PONumber] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[OrderItemName] [varchar](255) NOT NULL,
	[OrderItemDescription] [varchar](255) NOT NULL,
	[OrderItemJustification] [varchar](255) NOT NULL,
	[OrderItemLocation] [varchar](255) NOT NULL,
	[OrderItemQty] [int] NOT NULL,
	[OrderItemPrice] [decimal](9, 2) NOT NULL,
	[RecordVersion] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[PONumber] [int] IDENTITY(100,1) NOT NULL,
	[StatusID] [int] NOT NULL,
	[POCreationDate] [datetime] NOT NULL,
	[POSubtotal] [decimal](9, 2) NOT NULL,
	[POTax] [decimal](9, 2) NOT NULL,
	[POTotal] [decimal](9, 2) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[RecordVersion] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PONumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusLookup]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusLookup](
	[StatusID] [int] NOT NULL,
	[StatusName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (1, N'HR', N'Human Resources', CAST(N'2005-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (2, N'PO', N'Purchase Order Requests', CAST(N'2010-05-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (3, N'Management', N'Overseers of the company', CAST(N'2004-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (4, N'Sales', N'Responsible for profits and accounting', CAST(N'2004-06-06T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province],[Country],[PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000000, 3, 11, N'123', N'Khraig', N'', N'Bentley', N'123456789', CAST(N'1969-04-20T00:00:00.000' AS DateTime), N'123 Real Street', N'Moncton', N'NB', N'CA', N'1NB 9P1', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), N'123-456-7890', N'987-654-3210', N'ceo@mail.com', 0, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province],[Country],[PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000001, 1, 9, N'123', N'Ethan', N'', N'Reinsborough', N'111111111', CAST(N'1998-04-16T00:00:00.000' AS DateTime), N'123 MiddleOfNoWhere', N'Dalhousie', N'NB', N'CA',  N'5GA 1C2', CAST(N'2021-04-12T00:00:00.000' AS DateTime), CAST(N'2021-04-26T00:00:00.000' AS DateTime), N'506-686-1111', N'506-684-2222', N'er@mail.com', 10000000, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province],[Country],[PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000002, 2, 9, N'123', N'Trevor', N'', N'Donovan', N'777777777', CAST(N'1969-04-20T00:00:00.000' AS DateTime), N'123 Mountain Rd', N'Moncton', N'NB', N'CA', N'6FB 3D1', CAST(N'2021-04-12T00:00:00.000' AS DateTime), CAST(N'2021-04-26T00:00:00.000' AS DateTime), N'506-121-1212', N'506-206-2069', N'td@mail.com', 10000000, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province],[Country],[PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000003, 4, 3, N'money', N'Jeff', N'', N'Bezos', N'123123123', CAST(N'1964-01-12T00:00:00.000' AS DateTime), N'Canadian Silicon Valley', N'Toronto', N'OT', N'CA', N'1AB 2C3', CAST(N'2006-07-04T00:00:00.000' AS DateTime), CAST(N'2008-01-01T00:00:00.000' AS DateTime), N'808-404-0101', N'808-200-1111', N'bigjeff@amazon.com', 10000000, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000005, 1, 1, N'jenniferppassword', N'Jennifer', N'P', N'Roseman', 312645978, CAST(N'1995-07-05T10:56:00.000' AS DateTime), N'109 Hidden Tree Road', N'Bathurst', N'NB', N'CA', N'A2C2A7', CAST(N'2021-04-28T10:56:00.000' AS DateTime), CAST(N'2021-05-26T10:56:00.000' AS DateTime), N'506-789-1243', N'506-685-0874', N'jennifer.roseman@outlook.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000006, 1, 8, N'mrsmithers123', N'Joseph', N'P', N'Smithers', 213546879, CAST(N'1986-06-03T11:00:00.000' AS DateTime), N'986 Fancy Lane', N'Miramichi', N'NB', N'CA', N'T6B3Y7', CAST(N'2021-06-07T11:00:00.000' AS DateTime), CAST(N'2021-08-19T11:00:00.000' AS DateTime), N'506-687-9432', N'506-685-3412', N'mrsmithers@yahoo.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000007, 1, 7, N'shicks321', N'Samantha', N'', N'Hicks', 980657213, CAST(N'1996-06-04T11:03:00.000' AS DateTime), N'125 Dillon St', N'Dalhousie', N'NB', N'CA', N'E7B2C7', CAST(N'2021-04-30T11:03:00.000' AS DateTime), CAST(N'2021-05-01T11:03:00.000' AS DateTime), N'506-686-1234', N'506-684-2102', N'shicks@aol.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000008, 1, 2, N'aross123', N'Alexander', N'', N'Ross', 749782165, CAST(N'2001-02-14T11:06:00.000' AS DateTime), N'Pine Needle Avenue', N'Fredericton', N'NB', N'CA', N'E7P1S0', CAST(N'2021-04-30T11:06:00.000' AS DateTime), CAST(N'2021-06-07T11:06:00.000' AS DateTime), N'289-391-5612', N'604-721-7732', N'alexross@yahoo.ca', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000009, 1, 6, N'jonaharvey321', N'Jonathan', N'A', N'Harvey', 935385278, CAST(N'1992-06-10T11:09:00.000' AS DateTime), N'321 Seacrest Lane', N'St Johns', N'NL', N'CA', N'N4N9P9', CAST(N'2021-04-20T11:09:00.000' AS DateTime), CAST(N'2021-08-05T11:09:00.000' AS DateTime), N'450-303-3865', N'519-557-1505', N'jonharvey@outlook.ca', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000010, 1, 8, N'ololsenpassword!', N'Olivia', N'T', N'Olsen', 689341071, CAST(N'1998-03-04T11:12:00.000' AS DateTime), N'234 Plad Street', N'Gander', N'NL', N'CA', N'V3Y0L1', CAST(N'2021-04-07T11:12:00.000' AS DateTime), CAST(N'2021-05-10T11:12:00.000' AS DateTime), N'778-825-4664', N'204-583-9081', N'oolsen@hotmail.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000011, 1, 4, N'iamelia123', N'Isabella', N'', N'Amelia', 794084608, CAST(N'1998-04-17T11:14:00.000' AS DateTime), N'454 Forest Lane', N'Blackville', N'NB', N'CA', N'E2A0J4', CAST(N'2021-04-30T11:14:00.000' AS DateTime), CAST(N'2021-05-10T11:14:00.000' AS DateTime), N'506-262-9120', N'506-886-2234', N'isabella.amelia@gmail.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000012, 1, 9, N'anneofgreengables123', N'Anne', N'G', N'Gables', 732450184, CAST(N'1989-06-14T11:17:00.000' AS DateTime), N'432 Twin Shores Lane', N'Charlottetown', N'PE', N'CA', N'E4S2A9', CAST(N'2021-04-30T11:17:00.000' AS DateTime), CAST(N'2021-05-05T11:17:00.000' AS DateTime), N'403-693-4708', N'647-949-3174', N'famouspei@outlook.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000013, 1, 10, N'jrob123', N'Joey', N'', N'Robertson', 166886754, CAST(N'1995-02-02T11:19:00.000' AS DateTime), N'341 Dalhousie Avenue', N'Halifax', N'NS', N'CA', N'J5W2Y8', CAST(N'2021-04-30T11:19:00.000' AS DateTime), CAST(N'2021-07-06T11:19:00.000' AS DateTime), N'418-967-2921', N'587-241-6744', N'jrobertson@gmail.com', 10000001, 1)
INSERT [dbo].[Employee] ([EmployeeID], [DepartmentID], [JobID], [Password], [FirstName], [MI], [LastName], [SIN], [DOB], [StreetAddress], [Municipality], [Province], [Country],  [PostalCode], [SeniorityDate], [JobStartDate], [WorkPhone], [CellPhone], [Email], [Supervisor],[Active]) VALUES (10000014, 1, 7, N'scharleson123', N'Sofia', N'M', N'Charleson', 669324691, CAST(N'1996-02-14T11:21:00.000' AS DateTime), N'221 UNB Road', N'Fredericton', N'NB', N'CA', N'E5J7Y5', CAST(N'2021-04-30T11:21:00.000' AS DateTime), CAST(N'2021-05-06T11:21:00.000' AS DateTime), N'902-381-3660', N'306-259-3662', N'sofia.charleson@outlook.com', 10000001, 0)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (1, N'IT Support')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (2, N'Janitor')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (3, N'Accountant')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (4, N'Business Analyst')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (5, N'Quality Assurance Tester')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (6, N'Receptionist')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (7, N'Secretary')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (8, N'Office Worker')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (9, N'Software Developer')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (10, N'Backend Maintenance')
INSERT [dbo].[Job] ([JobID], [JobName]) VALUES (11, N'CEO')
GO
SET IDENTITY_INSERT [dbo].[OrderItem] ON 

INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (1, 100, 1, N'Pen', N'Ball Point Pen', N'Need more pens', N'Staples', 2, CAST(1.30 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (2, 100, 1, N'Stapler', N'Large office stapler', N'Need to staple papers', N'Office Depot', 1, CAST(2.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (3, 101, 1, N'Paper Shredder', N'Industrial paper shredder. Extra strength.', N'Need one for each side of the office', N'Staples', 2, CAST(75.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (4, 102, 1, N'Calendar', N'Printed seasonal calender', N'Need to keep track of the days', N'Walmart', 1, CAST(10.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (5, 102, 1, N'Stamps', N'Stamp with company logo', N'Stamp to approve items', N'Staples', 1, CAST(5.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (6, 103, 1, N'Sticky Notes', N'Pack of 50', N'For reminders', N'Office Depot', 2, CAST(0.50 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (7, 103, 1, N'Envelopes', N'Mailable envelope. Pack of 100', N'For sending mail', N'Staples', 2, CAST(3.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (8, 104, 1, N'Stapler remover', N'Small stapler remover', N'Now that I am stapling things I must take them apart.', N'Staples', 1, CAST(2.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (9, 105, 1, N'Laptop Charger', N'Portable laptop charger', N'Broken charger', N'Walmart', 1, CAST(50.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (10, 105, 1, N'Solid State Drive', N'2TB SSD', N'Backing up files', N'Newegg', 2, CAST(150.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (11, 105, 1, N'Highlighter', N'Pack of 5', N'Highlighting notes', N'Staples', 1, CAST(3.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (12, 106, 1, N'Monitor', N'144Hz monitor', N'Need an extra monitor', N'BestBuy', 1, CAST(300.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (13, 106, 1, N'Office Chair', N'Black leather office chair', N'Need new chair', N'Sears', 1, CAST(300.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (14, 106, 1, N'Desk', N'Corner office desk', N'Previous desk was moved', N'Sears', 1, CAST(400.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (15, 107, 1, N'Keyboard', N'Mechanical keyboard', N'Need new keyboard for new hire', N'BestBuy', 1, CAST(50.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (16, 107, 1, N'Mouse', N'Ergonomic mouse', N'Need mouse for new hire', N'BestBuy', 1, CAST(35.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (17, 108, 1, N'GFX Card', N'GTX5090', N'For mining bitcoin', N'Newegg', 3, CAST(20000.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (18, 109, 1, N'Blinds', N'Window Office Blinds', N'Old blinds are broken', N'Sears', 1, CAST(30.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (19, 109, 1, N'Garbage can', N'Small garbage can', N'Stolen by another employee', N'Sears', 1, CAST(9.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (20, 110, 1, N'Paper clips', N'Pack of 200', N'Need to bind papers together', N'Staples', 2, CAST(0.59 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (21, 111, 1, N'Amazon Prime', N'Amazon Prime Subscription', N'For fast delivery to our office', N'Amazon', 1, CAST(14.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (22, 112, 1, N'Mouse Pad', N'Small mouse pad', N'Mouse tracking', N'Walmart', 1, CAST(7.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (23, 112, 1, N'Headset', N'Microsoft Headset Medium', N'Music production', N'Microsoft', 1, CAST(49.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (24, 113, 1, N'Motherboard', N'ASUS Motherboard', N'Motherboard fried with no surge protector', N'BestBuy', 1, CAST(99.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (25, 113, 1, N'Surge Protector', N'Standard surge protector', N'To keep from frying my new motherboard', N'BestBuy', 1, CAST(15.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (26, 114, 1, N'Paper Towel', N'Pack of 3', N'Spilling water on my desk', N'Walmart', 2, CAST(2.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (27, 115, 1, N'Company Car', N'Tesla', N'For travel to and from work', N'Tesla', 1, CAST(52999.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (28, 116, 1, N'USB Flash Drive', N'Pack of 5. 4GB ea.', N'Storing files', N'Staples', 1, CAST(4.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (29, 116, 1, N'Fountain Pen', N'Fancy Fountain pen', N'Writing letters', N'Staples', 1, CAST(12.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (30, 117, 1, N'Ink', N'Fountain pen ink (black)', N'Ink for my fountain pen', N'Staples', 1, CAST(1.99 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (31, 118, 1, N'Gaming Laptop', N'MSI Laptop', N'Gaming at the office', N'BestBuy', 1, CAST(1299.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (32, 118, 1, N'Gaming Mouse', N'Logitech gaming mouse', N'For gaming at the office', N'BestBuy', 1, CAST(76.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderItem] ([OrderItemID], [PONumber], [StatusID], [OrderItemName], [OrderItemDescription], [OrderItemJustification], [OrderItemLocation], [OrderItemQty], [OrderItemPrice]) VALUES (33, 119, 1, N'Plunger', N'Bathroom toilet plunger', N'For the washroom', N'Walmart', 1, CAST(5.00 AS Decimal(9, 2)))
SET IDENTITY_INSERT [dbo].[OrderItem] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrder] ON 

INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (100, 1, CAST(N'2021-04-29T22:36:48.973' AS DateTime), CAST(4.60 AS Decimal(9, 2)), CAST(0.60 AS Decimal(9, 2)), CAST(5.20 AS Decimal(9, 2)), 10000001)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (101, 1, CAST(N'2021-04-29T22:46:45.650' AS DateTime), CAST(150.00 AS Decimal(9, 2)), CAST(19.50 AS Decimal(9, 2)), CAST(169.50 AS Decimal(9, 2)), 10000001)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (102, 1, CAST(N'2021-04-29T22:47:54.670' AS DateTime), CAST(15.00 AS Decimal(9, 2)), CAST(1.95 AS Decimal(9, 2)), CAST(16.95 AS Decimal(9, 2)), 10000001)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (103, 1, CAST(N'2021-04-29T22:50:16.543' AS DateTime), CAST(7.00 AS Decimal(9, 2)), CAST(0.91 AS Decimal(9, 2)), CAST(7.91 AS Decimal(9, 2)), 10000001)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (104, 1, CAST(N'2021-04-29T22:56:45.927' AS DateTime), CAST(2.00 AS Decimal(9, 2)), CAST(0.26 AS Decimal(9, 2)), CAST(2.26 AS Decimal(9, 2)), 10000001)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (105, 1, CAST(N'2021-04-29T22:59:39.287' AS DateTime), CAST(353.00 AS Decimal(9, 2)), CAST(45.89 AS Decimal(9, 2)), CAST(398.89 AS Decimal(9, 2)), 10000002)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (106, 1, CAST(N'2021-04-29T23:02:12.640' AS DateTime), CAST(1000.00 AS Decimal(9, 2)), CAST(130.00 AS Decimal(9, 2)), CAST(1130.00 AS Decimal(9, 2)), 10000002)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (107, 1, CAST(N'2021-04-29T23:04:41.800' AS DateTime), CAST(85.00 AS Decimal(9, 2)), CAST(11.05 AS Decimal(9, 2)), CAST(96.05 AS Decimal(9, 2)), 10000002)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (108, 1, CAST(N'2021-04-29T23:07:02.320' AS DateTime), CAST(60000.00 AS Decimal(9, 2)), CAST(7800.00 AS Decimal(9, 2)), CAST(67800.00 AS Decimal(9, 2)), 10000002)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (109, 1, CAST(N'2021-04-29T23:08:39.287' AS DateTime), CAST(39.99 AS Decimal(9, 2)), CAST(5.20 AS Decimal(9, 2)), CAST(45.19 AS Decimal(9, 2)), 10000002)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (110, 1, CAST(N'2021-04-29T23:12:08.887' AS DateTime), CAST(1.18 AS Decimal(9, 2)), CAST(0.15 AS Decimal(9, 2)), CAST(1.33 AS Decimal(9, 2)), 10000003)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (111, 1, CAST(N'2021-04-29T23:13:04.427' AS DateTime), CAST(14.99 AS Decimal(9, 2)), CAST(1.95 AS Decimal(9, 2)), CAST(16.94 AS Decimal(9, 2)), 10000003)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (112, 1, CAST(N'2021-04-29T23:14:08.450' AS DateTime), CAST(57.98 AS Decimal(9, 2)), CAST(7.54 AS Decimal(9, 2)), CAST(65.52 AS Decimal(9, 2)), 10000003)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (113, 1, CAST(N'2021-04-29T23:15:50.447' AS DateTime), CAST(115.98 AS Decimal(9, 2)), CAST(15.08 AS Decimal(9, 2)), CAST(131.06 AS Decimal(9, 2)), 10000003)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (114, 1, CAST(N'2021-04-29T23:17:55.390' AS DateTime), CAST(5.98 AS Decimal(9, 2)), CAST(0.78 AS Decimal(9, 2)), CAST(6.76 AS Decimal(9, 2)), 10000003)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (115, 1, CAST(N'2021-04-29T23:18:41.440' AS DateTime), CAST(52999.00 AS Decimal(9, 2)), CAST(6889.87 AS Decimal(9, 2)), CAST(59888.87 AS Decimal(9, 2)), 10000000)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (116, 1, CAST(N'2021-04-29T23:19:24.343' AS DateTime), CAST(17.98 AS Decimal(9, 2)), CAST(2.34 AS Decimal(9, 2)), CAST(20.32 AS Decimal(9, 2)), 10000000)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (117, 1, CAST(N'2021-04-29T23:20:30.547' AS DateTime), CAST(1.99 AS Decimal(9, 2)), CAST(0.26 AS Decimal(9, 2)), CAST(2.25 AS Decimal(9, 2)), 10000000)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (118, 1, CAST(N'2021-04-29T23:21:25.127' AS DateTime), CAST(1375.00 AS Decimal(9, 2)), CAST(178.75 AS Decimal(9, 2)), CAST(1553.75 AS Decimal(9, 2)), 10000000)
INSERT [dbo].[PurchaseOrder] ([PONumber], [StatusID], [POCreationDate], [POSubtotal], [POTax], [POTotal], [EmployeeID]) VALUES (119, 1, CAST(N'2021-04-29T23:22:39.247' AS DateTime), CAST(5.00 AS Decimal(9, 2)), CAST(0.65 AS Decimal(9, 2)), CAST(5.65 AS Decimal(9, 2)), 10000000)
SET IDENTITY_INSERT [dbo].[PurchaseOrder] OFF
GO
INSERT [dbo].[StatusLookup] ([StatusID], [StatusName]) VALUES (1, N'Pending')
INSERT [dbo].[StatusLookup] ([StatusID], [StatusName]) VALUES (2, N'Approved')
INSERT [dbo].[StatusLookup] ([StatusID], [StatusName]) VALUES (3, N'Denied')
INSERT [dbo].[StatusLookup] ([StatusID], [StatusName]) VALUES (4, N'Under Review')
INSERT [dbo].[StatusLookup] ([StatusID], [StatusName]) VALUES (5, N'Closed')
GO
ALTER TABLE [dbo].[OrderItem] ADD  DEFAULT ((1)) FOR [StatusID]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  DEFAULT ((1)) FOR [StatusID]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  DEFAULT (getdate()) FOR [POCreationDate]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  DEFAULT ((0.0)) FOR [POSubtotal]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  DEFAULT ((0.0)) FOR [POTax]
GO
ALTER TABLE [dbo].[PurchaseOrder] ADD  DEFAULT ((0.0)) FOR [POTotal]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([PONumber])
REFERENCES [dbo].[PurchaseOrder] ([PONumber])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[StatusLookup] ([StatusID])
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[StatusLookup] ([StatusID])
GO
/****** Object:  StoredProcedure [dbo].[spAddDepartment]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROC [dbo].[spAddDepartment]
    @DepartmentOutputPARM INT OUTPUT,
    @DepartmentName VARCHAR(255),
    @DepartmentDescription VARCHAR(255),
    @InvocationDate DATETIME2
AS
BEGIN
    BEGIN TRY
        INSERT INTO Department(
            DepartmentName,
            DepartmentDescription,
            InvocationDate)
        VALUES(
            @DepartmentName,
            @DepartmentDescription,
            @InvocationDate)
        SET @DepartmentOutputPARM = SCOPE_IDENTITY()
    END TRY
    BEGIN CATCH
        ;THROW
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spAddEmployee]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROC [dbo].[spAddEmployee]
    @EmployeeOutputPARM INT OUTPUT,
    @DepartmentID INT,
    @JobID INT,
    @Password VARCHAR(255),
    @FirstName VARCHAR(255),
    @MI VARCHAR(10),
    @LastName VARCHAR(255),
    @SIN VARCHAR(255),
    @DOB DATETIME2,
    @StreetAddress VARCHAR(255),
    @Municipality VARCHAR(255),
    @Province VARCHAR(255),
    @Country VARCHAR(255),
    @PostalCode VARCHAR(255),
    @SeniorityDate DATETIME2,
    @JobStartDate DATETIME2,
    @WorkPhone VARCHAR(255),
    @CellPhone VARCHAR(255),
    @Email VARCHAR(255),
    @Supervisor INT,
	@Active BIT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employee(
            DepartmentID,
            JobID,
            Password,
            FirstName,
            MI,
            LastName,
            SIN,
            DOB,
            StreetAddress,
            Municipality,
            Province,
            Country,
            PostalCode,
            SeniorityDate,
            JobStartDate,
            WorkPhone,
            CellPhone,
            Email,
            Supervisor,
			Active)
        VALUES(
            @DepartmentID,
            @JobID,
            @Password,
            @FirstName,
            @MI,
            @LastName,
            @SIN,
            @DOB,
            @StreetAddress,
            @Municipality,
            @Province,
            @Country,
            @PostalCode,
            @SeniorityDate,
            @JobStartDate,
            @WorkPhone,
            @CellPhone,
            @Email,
            @Supervisor,
			@Active)
        SET @EmployeeOutputPARM = SCOPE_IDENTITY()
    END TRY
    BEGIN CATCH
        ;THROW
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spAddPurchaseOrder]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE      PROCEDURE [dbo].[spAddPurchaseOrder]
	@RecordVersion ROWVERSION OUTPUT,
	@PONumber INT OUTPUT,
	@EmployeeID INT,
	--update status, sub, tax, total later
	--record version does not need insert

	@OrderItemID INT OUTPUT,
	@OrderItemName VARCHAR(255),
	@OrderItemDescription VARCHAR(255),
	@OrderItemJustification VARCHAR(255),
	@OrderItemPrice DECIMAL(9,2),
	@OrderItemQty INT,
	@OrderItemLocation VARCHAR(255)
AS	
BEGIN
BEGIN TRY
	BEGIN TRAN
		IF(SELECT RecordVersion FROM PurchaseOrder WHERE PONumber = @PONumber) <> @RecordVersion
			THROW 50002, 'The record has been updated since last time you retrieved it', 1

		INSERT INTO PurchaseOrder (EmployeeID) VALUES (@EmployeeID);
		SET @PONumber = SCOPE_IDENTITY();
	SET @RecordVersion = (SELECT RecordVersion FROM PurchaseOrder WHERE PONumber = @PONumber)

		INSERT INTO OrderItem (PONumber, OrderItemName, OrderItemDescription, OrderItemJustification, OrderItemPrice, OrderItemQty, OrderItemLocation) VALUES
		(@PONumber, @OrderItemName, @OrderItemDescription, @OrderItemJustification, @OrderItemPrice, @OrderItemQty, @OrderItemLocation);
		SET @OrderItemID = SCOPE_IDENTITY();

	COMMIT TRAN; 

END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;  
				       
	;THROW
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spEmployeeLogin]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[spEmployeeLogin]
 	@Email varchar(255),
	@Password varchar(255)
AS	
BEGIN
	BEGIN TRY
		If NOT EXISTS (SELECT * FROM employee WHERE Email = @Email)  
			BEGIN
				DECLARE @Error nvarchar(100)
				SET @Error = 'Login Failed. Email or password incorrect. Please try again';
				THROW 51005, @Error, 1
			END	 
		
  			SELECT * FROM Employee WHERE Email = @Email AND Password = @Password
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spEmployeeLookup]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE        PROCEDURE [dbo].[spEmployeeLookup]
	@EmployeeID INT
AS	
BEGIN
	BEGIN TRY

		SELECT Employee.FirstName + ' ' + Employee.MI + ' ' + Employee.LastName AS SupervisorName, Department.DepartmentName
			FROM Employee JOIN Department 
				ON Employee.DepartmentID = Department.DepartmentID
			WHERE EmployeeID = @EmployeeID;
		
			
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetItemsList]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   PROCEDURE [dbo].[spGetItemsList]
	@PONumber INT
AS	
BEGIN
	BEGIN TRY
		
		SELECT * FROM OrderItem WHERE PONumber = @PONumber
		
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetOrdersByDate]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE         PROCEDURE [dbo].[spGetOrdersByDate]
	@EmployeeID INT,
	@Start DateTime,
	@End DateTime
AS	
BEGIN
	BEGIN TRY
		
		SELECT * FROM PurchaseOrder WHERE EmployeeID = @EmployeeID AND POCreationDate BETWEEN @Start AND @End AND StatusID = 1
		ORDER BY POCreationDate DESC
		
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetOrdersByPONumber]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE         PROCEDURE [dbo].[spGetOrdersByPONumber]
	@EmployeeID INT,
	@PONumber INT
AS	
BEGIN
	BEGIN TRY
		
		SELECT * FROM PurchaseOrder WHERE EmployeeID = @EmployeeID AND PONumber = @PONumber AND StatusID = 1
		
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetStatusesForLookup]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE      PROCEDURE [dbo].[spGetStatusesForLookup]
AS	
BEGIN
	BEGIN TRY
		
		SELECT * FROM [dbo].[StatusLookup]
			
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertItem]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spInsertItem]
	@OrderItemID INT OUTPUT,
    @RecordVersion ROWVERSION OUTPUT,

	@PONumber INT,
	@OrderItemName VARCHAR(255),
	@OrderItemDescription VARCHAR(255),
	@OrderItemJustification VARCHAR(255),
	@OrderItemPrice DECIMAL(9,2),
	@OrderItemQty INT,
	@OrderItemLocation VARCHAR(255)
AS	
BEGIN
BEGIN TRY
	BEGIN TRAN

		INSERT INTO OrderItem (PONumber, OrderItemName, OrderItemDescription, OrderItemJustification, OrderItemPrice, OrderItemQty, OrderItemLocation) VALUES
		(@PONumber, @OrderItemName, @OrderItemDescription, @OrderItemJustification, @OrderItemPrice, @OrderItemQty, @OrderItemLocation);

		SET @OrderItemID = SCOPE_IDENTITY();    
        SET @RecordVersion = (SELECT RecordVersion FROM OrderItem WHERE OrderItemID = @OrderItemID)

	COMMIT TRAN; 

END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;  
				       
	;THROW
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spSearchEmployeeByID]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spSearchEmployeeByID]
	@EmployeeID INT
AS

BEGIN
	SELECT
		FirstName,MI,LastName,StreetAddress + ', ' + Municipality + ' ' + PostalCode AS [MailAddress], WorkPhone, CellPhone, Email
	FROM	
		Employee	
	WHERE
		EmployeeID = @EmployeeID
END
GO
/****** Object:  StoredProcedure [dbo].[spSearchEmployeeByLName]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spSearchEmployeeByLName]
	@LastName VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FirstName,MI,LastName,StreetAddress + ', ' + Municipality + ' ' + PostalCode AS [MailAddress], WorkPhone, CellPhone, Email
	FROM	
		Employee	
	WHERE
		LastName LIKE '%' + @LastName + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateDupedQty]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATe    PROCEDURE [dbo].[spUpdateDupedQty]
 	@OrderItemID INT,
	@OrderItemQty INT

AS	
BEGIN
	BEGIN TRY
		UPDATE OrderItem 
			SET OrderItemQty = @OrderItemQty	
			WHERE OrderItemID = @OrderItemID;

	END TRY	

	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateOrderItem]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE       PROCEDURE [dbo].[spUpdateOrderItem]
	@RecordVersion ROWVERSION OUTPUT,

	@OrderItemID INT,
	@OrderItemName VARCHAR(255),
	@OrderItemDescription VARCHAR(255),
	@OrderItemJustification VARCHAR(255),
	@OrderItemPrice DECIMAL(19,0),
	@OrderItemQty INT,
	@OrderItemLocation VARCHAR(255)

AS
BEGIN

	BEGIN TRY

		--CHECK RECORD DELTETION E2/C1

		IF(SELECT RecordVersion FROM OrderItem WHERE OrderItemID = @OrderItemID) <> @RecordVersion
			THROW 50002, 'The record has been updated since last time you retrieved it', 1


		UPDATE OrderItem SET
			
		OrderItemName = @OrderItemName,
		OrderItemDescription = @OrderItemDescription,
		OrderItemJustification = @OrderItemJustification,
		OrderItemPrice = @OrderItemPrice,
		OrderItemQty = @OrderItemQty,
		OrderItemLocation = @OrderItemLocation
		WHERE OrderItemID = @OrderItemID;


		SET @RecordVersion = (SELECT RecordVersion FROM OrderItem WHERE OrderItemID = @OrderItemID)
		
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateTotals]    Script Date: 2021-04-30 12:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATe    PROCEDURE [dbo].[spUpdateTotals]
 	@PONumber INT,
	@subtotal decimal(11,2),
	@tax decimal(11,2),
	@total decimal(11,2)
AS	
BEGIN
	BEGIN TRY
		UPDATE PurchaseOrder 
			SET POSubtotal = @subtotal,
				POTax = @tax,
				POTotal = @total
			
			WHERE PONumber = @PONumber;

	END TRY	

	BEGIN CATCH
		;THROW
	END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [TEC] SET  READ_WRITE 
GO


USE [master]
GO
/****** Object:  Database [Employee_DB]    Script Date: 26/10/2020 2:42:36 PM ******/
CREATE DATABASE [Employee_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Employee_DB_Data', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLSERVER\MSSQL\DATA\Employee_DB_Data.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Employee_DB_Log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLSERVER\MSSQL\DATA\Employee_DB_Log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 1024KB )
GO
ALTER DATABASE [Employee_DB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Employee_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Employee_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Employee_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Employee_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Employee_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Employee_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Employee_DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Employee_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Employee_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Employee_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Employee_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Employee_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Employee_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Employee_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Employee_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Employee_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Employee_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Employee_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Employee_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Employee_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Employee_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Employee_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Employee_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Employee_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Employee_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Employee_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Employee_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Employee_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Employee_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Employee_DB]
GO
/****** Object:  Table [dbo].[branch]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[br_id] [nchar](10) NULL,
	[br_name] [nchar](10) NULL,
	[br_address] [nvarchar](50) NULL,
	[br_brgy] [nvarchar](50) NULL,
	[br_city] [nvarchar](50) NULL,
	[br_permit] [nchar](10) NOT NULL,
	[br_mngr] [nvarchar](max) NOT NULL,
	[date_opened] [date] NOT NULL,
	[isActive] [nvarchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeDetails]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[last_name] [nvarchar](50) NULL,
	[first_name] [nvarchar](50) NULL,
	[middle_name] [nvarchar](50) NULL,
	[date_hired] [date] NULL,
	[emp_image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[Add_New_Branch]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Add_New_Branch]
 
@br_id NVARCHAR(100) = NULL  
,@br_name NVARCHAR(15) = NULL  
,@br_address NVARCHAR(300) = 0  
,@br_brgy nvarchar(50)=null 
,@br_city nvarchar(50)=null
,@br_mngr nvarchar(max)=null
,@date_opened DATETIME = NULL
 ,@br_permit nvarchar(50)=null  
,@isactive NVARCHAR(1) = NULL  

AS
BEGIN
	

insert into branch (br_id,br_name,br_address,br_brgy,br_city,br_mngr,date_opened,br_permit,isActive) values (@br_id,@br_name,@br_address,@br_brgy,@br_city,@br_mngr,@date_opened,@br_permit,@isactive)
END

GO
/****** Object:  StoredProcedure [dbo].[Add_New_Employee]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Add_New_Employee]
 
@first_name NVARCHAR(100) = NULL  
,@middle_name NVARCHAR(15) = NULL  
,@last_name NVARCHAR(300) = 0  
,@date_hired DATETIME = NULL  
,@emp_image NVARCHAR(max) = NULL  

AS
BEGIN
	

insert into EmployeeDetails (first_name,middle_name,last_name,date_hired,emp_image) values (@first_name,@middle_name,@last_name,@date_hired,@emp_image)
END

GO
/****** Object:  StoredProcedure [dbo].[Delete_Branch]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_Branch]
@id int = 0 
AS
BEGIN
	delete from branch where ID= @id
END

GO
/****** Object:  StoredProcedure [dbo].[Delete_Employee]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_Employee]
@id int = 0 
AS
BEGIN
	delete from employeedetails where ID= @id
END


GO
/****** Object:  StoredProcedure [dbo].[Get_EmployeeNames]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_EmployeeNames] 
	 
AS
BEGIN
select id, first_name + ' '+ middle_name + ' ' + last_name as 'EmployeeName' from employeedetails
END


GO
/****** Object:  StoredProcedure [dbo].[Select_All_Branches]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_All_Branches]

AS
BEGIN
Select a.*,b.first_name + ' ' + b.middle_name + ' ' + b.last_name as 'manager'  from branch a left join employeedetails b on b.id = a.br_mngr

END


GO
/****** Object:  StoredProcedure [dbo].[Select_all_employees]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_all_employees]
	
AS
BEGIN
	SELECT *
  FROM [Employee_DB].[dbo].[EmployeeDetails]
  order by ID asc
 
END


GO
/****** Object:  StoredProcedure [dbo].[Select_Specific_Branches]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_Specific_Branches]
@id int = 0
AS
BEGIN

Select a.*,b.first_name + ' ' + b.middle_name + ' ' + b.last_name as 'manager'  from branch a left join employeedetails b on b.id = a.br_mngr
where a.id=@id
END


GO
/****** Object:  StoredProcedure [dbo].[Select_specific_employees]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_specific_employees]
@id int = 0
AS
BEGIN
	SELECT A.*, B.BR_NAME
  FROM [Employee_DB].[dbo].[EmployeeDetails] A
  LEFT JOIN BRANCH B ON A.ID = B.BR_MNGR
  
  where A.id = @id
 
END


GO
/****** Object:  StoredProcedure [dbo].[update_branch]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[update_branch]
@id int ,
@br_id NVARCHAR(100) = NULL  
,@br_name NVARCHAR(15) = NULL  
,@br_address NVARCHAR(300) = 0  
,@br_brgy nvarchar(50)=null 
,@br_city nvarchar(50)=null
,@br_mngr nvarchar(max)=null
,@date_opened DATETIME
 ,@br_permit nvarchar(50)=null  
,@isactive bit   
AS
BEGIN
	update branch set br_name = @br_name, br_address = @br_address,br_brgy=@br_brgy, br_city=@br_city,br_mngr=@br_mngr,br_permit=@br_permit,date_opened = @date_opened,isactive=@isactive 
	where id=@id
END


GO
/****** Object:  StoredProcedure [dbo].[Update_Employee]    Script Date: 26/10/2020 2:42:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Employee]
@id int = 0,	 
@first_name NVARCHAR(100) = NULL  
,@middle_name NVARCHAR(15) = NULL  
,@last_name NVARCHAR(300) = 0  
,@date_hired DATETIME = NULL  
,@emp_image NVARCHAR(max) = NULL 
AS
BEGIN
	update  employeedetails set first_name = @first_name, middle_name = @middle_name, last_name = @last_name, date_hired = @date_hired, emp_image = @emp_image where ID = @id
END


GO
USE [master]
GO
ALTER DATABASE [Employee_DB] SET  READ_WRITE 
GO

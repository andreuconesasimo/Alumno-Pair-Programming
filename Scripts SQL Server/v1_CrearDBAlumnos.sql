USE master;
GO

IF DB_ID(N'AlumnosDB') IS NULL
BEGIN
	
	DECLARE @os as nvarchar(200)
	DECLARE @separator as nchar(1)
	DECLARE @basedirectory as nvarchar(100)
	DECLARE @maindirectory as nvarchar(200)
	DECLARE @primarydirectory as nvarchar(300)
	DECLARE @secondarydirectory as nvarchar(300)
	DECLARE @logsdirectory as nvarchar(300)

	-- Get current OS
	SET @os = (SELECT @@VERSION)
	PRINT(@os)
	IF CHARINDEX('Windows',@os) != 0
	BEGIN
		SET @os = 'Windows'
		SET @separator = '\'
		-- Set Configurations to use xp_cmdshell
		EXEC sp_configure 'SHOW ADVANCED OPTIONS', 1
		RECONFIGURE
		EXEC sp_configure 'xp_cmdshell', 1
		RECONFIGURE
		EXEC sp_configure 'SHOW ADVANCED OPTIONS', 0
		RECONFIGURE
		-- Get value of SQLServerDB (the initial path for the DB files)
		CREATE TABLE #temptable (ruta nvarchar(100))
		INSERT #temptable EXEC xp_cmdshell 'echo %SQLServerDB%'
		SET @basedirectory = (SELECT TOP(1) ruta from #temptable)
		DROP TABLE #temptable
		-- Reset Configurations
		EXEC sp_configure 'SHOW ADVANCED OPTIONS', 1
		RECONFIGURE
		EXEC sp_configure 'xp_cmdshell', 0
		RECONFIGURE
		EXEC sp_configure 'SHOW ADVANCED OPTIONS', 0
		RECONFIGURE

		-- Assign path values
		SET @maindirectory = @basedirectory + N'\Alumno Pair Programming'
		SET @primarydirectory = @maindirectory + N'\Primary'
		SET @secondarydirectory = @maindirectory + N'\Secondary'
		SET @logsdirectory = @maindirectory + N'\Logs'

		-- Create folders
		EXEC master.sys.xp_create_subdir @basedirectory
		EXEC master.sys.xp_create_subdir @maindirectory
		EXEC master.sys.xp_create_subdir @primarydirectory
		EXEC master.sys.xp_create_subdir @secondarydirectory
		EXEC master.sys.xp_create_subdir @logsdirectory
	END
	IF CHARINDEX('LINUX',@os) != 0
	BEGIN
		SET @os = 'Linux'
		SET @separator = '/'
		SET @basedirectory = '$(SQLServerDB)'

		-- Assign path values
		SET @maindirectory = @basedirectory + N'/Alumno Pair Programming'
		SET @primarydirectory = @maindirectory + N'/Primary'
		SET @secondarydirectory = @maindirectory + N'/Secondary'
		SET @logsdirectory = @maindirectory + N'/Logs'

-- xp_create_subdir aun no esta soportado en Linux, pero Microsoft planea implementarlo en el futuro
-- https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes?view=sql-server-linux-2017
-- "The following features and services are not available on Linux at the time of the GA release. The support of these features will be increasingly enabled over time: ...System extended stored procedures (XP_CMDSHELL, etc.)..."

/*
		--Create directories of the Database using the assigned value of the EnvVar
		EXEC master.sys.xp_create_subdir @basedirectory
		EXEC master.sys.xp_create_subdir @maindirectory
		EXEC master.sys.xp_create_subdir @primarydirectory
		EXEC master.sys.xp_create_subdir @secondarydirectory
		EXEC master.sys.xp_create_subdir @logsdirectory
*/
		--RETURN
	END

	-- Declare the filenames
	DECLARE @primaryfilename as nvarchar(50)
	SET @primaryfilename = N'AlumnosDB_Prm.mdf'
	DECLARE @fg1dat1filename as nvarchar(50)
	SET @fg1dat1filename = N'AlumnosDB_FG1_1.ndf'
	DECLARE @fg1dat2filename as nvarchar(50)
	SET @fg1dat2filename = N'AlumnosDB_FG1_2.ndf'
	DECLARE @logfilename as nvarchar(50)
	SET @logfilename = N'AlumnosDBLogs.ldf'

	PRINT(@basedirectory)
	PRINT(@primaryfilename)
	PRINT(@fg1dat1filename)
	PRINT(@fg1dat2filename)
	PRINT(@logfilename)

	-- Declare the SQL sentence
	DECLARE @sql as nvarchar(1000)
	SET @sql = 'CREATE DATABASE AlumnosDB
			  ON PRIMARY
				( NAME='''+@primaryfilename+''',
				  FILENAME='''+@primarydirectory+@separator+@primaryfilename+''',
				  SIZE=4MB,
				  MAXSIZE=10MB,
				  FILEGROWTH=1MB),
			  FILEGROUP AlumnosDB_FG1
				( NAME='''+@fg1dat1filename+''',
				  FILENAME='''+@secondarydirectory+@separator+@fg1dat1filename+''',
				  SIZE=1MB,
				  MAXSIZE=10MB,
				  FILEGROWTH=1MB),
				( NAME='''+@fg1dat2filename+''',
				  FILENAME='''+@secondarydirectory+@separator+@fg1dat2filename+''',
				  SIZE=1MB,
				  MAXSIZE=10MB,
				  FILEGROWTH=1MB)
			  LOG ON
				( NAME='''+@logfilename+''',
				  FILENAME='''+@logsdirectory+@separator+@logfilename+''',
				  SIZE=1MB,
				  MAXSIZE=10MB,
				  FILEGROWTH=1MB);
				  '
	-- Print for debugging and execution
	--PRINT(@sql)
	--EXEC(@sql)
END
ELSE
	PRINT('La base de datos AlumnoDB ya existe')

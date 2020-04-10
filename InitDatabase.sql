create database webapp;

use webapp;

/**********************************************************************************
Group Project CSC 174 | Build The Lanes
Authors: Aaron Miller, Marina Stangl, Hector Romo
SQL Version: Microsoft SQL Server Express Edition 14.00.3223.3.v1
Host: build-the-lanes-0.cz837oegnsiw.us-west-1.rds.amazonaws.com,1433
User: admin
Password: [Ask an Author]
***********************************************************************************/


/**********TABLE CREATION STARTS HERE**********/
/*****ROLE BASED AUTH PROFILES SECTION STARTS HERE*****/
-- IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
-- BEGIN
--     CREATE TABLE [__EFMigrationsHistory] (
--         [MigrationId] nvarchar(150) NOT NULL,
--         [ProductVersion] nvarchar(32) NOT NULL,
--         CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
--     );
-- END;

CREATE TABLE [Users](
    id INT NOT NULL IDENTITY,
	email VARCHAR(320) NOT NULL,
	password_salt VARCHAR(max) NOT NULL,
	password_hash VARCHAR(max) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	/*d=Donator | s=Staff | e=Engineer | a=Admin   so...  ads=[Staff, Donator, Admin]*/
	roles VARCHAR (2) NOT NULL,
	/*For: Donator */
	amount_donated MONEY,
	/*For: Staff */
	title VARCHAR(128),
	/*For: Engineer */
	type VARCHAR(256),
	/*For: Admin */
	created DATETIME,
	PRIMARY KEY (email),
);
/***Role Based Sub-Class Tables***/
/***May be used in with "Users" table to create Named Queries***/
CREATE TABLE Donators(
    id INT NOT NULL IDENTITY,
	email VARCHAR(320) NOT NULL,
	password_salt VARCHAR(max) NOT NULL,
	password_hash VARCHAR(max) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	amount_donated MONEY NOT NULL,
	PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES [Users](email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Staffs(
    id INT NOT NULL IDENTITY,
	email VARCHAR(320) NOT NULL,
	password_salt VARCHAR(max) NOT NULL,
	password_hash VARCHAR(max) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	title VARCHAR(128) NOT NULL,
	type VARCHAR(256),
	created DATETIME,
	PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES [Users](email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABlE Admins(
    id INT NOT NULL IDENTITY,
    email VARCHAR(320) NOT NULL,
	password_salt VARCHAR(max) NOT NULL,
	password_hash VARCHAR(max) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	title VARCHAR(128) NOT NULL,
	created DATETIME NOT NULL,
    PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Staffs(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Engineers(
    id INT NOT NULL IDENTITY,
    email VARCHAR(320) NOT NULL,
	password_salt VARCHAR(max) NOT NULL,
	password_hash VARCHAR(max) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	title VARCHAR(128) NOT NULL,
	type VARCHAR(256) NOT NULL,
    PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Staffs(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
/***Engineer: Certification and Degree Data***/
/***Note: these aren't profiles. Just a tables for Engineer(s) with
          Multiple degrees/certifications. ***/
CREATE TABLE EngineerCertifications(
    id INT NOT NULL IDENTITY,
    email VARCHAR(320) NOT NULL,
    certification VARCHAR(256),
    PRIMARY KEY (email, certification),
    FOREIGN KEY (email) REFERENCES Engineers(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE EngineerDegrees(
    id INT NOT NULL IDENTITY,
    email VARCHAR(320) NOT NULL,
    degree VARCHAR(256),
    PRIMARY KEY (email, degree),
    FOREIGN KEY (email) REFERENCES Engineers(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
/*****ROLE BASED AUTH PROFILES SECTION ENDS HERE*****/

CREATE TABLE Projects(
    project_num INTEGER NOT NULL IDENTITY, /*IDENTITY AUTO INCREMENTS*/
    start_date DATE NOT NULL,
    status VARCHAR(16) NOT NULL,
    city VARCHAR(45) NOT NULL,
    zip_code CHAR(5) NOT NULL,
    PRIMARY KEY (project_num)
);

CREATE TABLE Responsibilities(
    number INTEGER NOT NULL IDENTITY,
    staff_email VARCHAR(320) FOREIGN KEY REFERENCES Staffs(email),
    project_num INT FOREIGN KEY REFERENCES Projects(project_num),
    PRIMARY KEY (staff_email, project_num)
)
/**********TABLE CREATION ENDS HERE**********/


GO -- This GO statement is required to end the query statements and differ between creation of Tables and Triggers


/**********PERSISTENT STORED MODULES START HERE**********/
/***TRIGGER CREATION STARTS HERE***/



CREATE TRIGGER User_Created_Check
ON [Users]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @new_email VARCHAR(320);
	DECLARE @new_password_salt VARCHAR(max);
    DECLARE @new_password_hash VARCHAR(max);
	DECLARE @new_token VARCHAR(320);
	DECLARE @new_f_name VARCHAR(64);
	DECLARE @new_l_name VARCHAR(64);
	DECLARE @new_roles VARCHAR (2);
	DECLARE @new_amount_donated MONEY;
	DECLARE @new_title VARCHAR(128);
	DECLARE @new_type VARCHAR(256);
	DECLARE @new_created DATETIME;

    SET @new_email = (SELECT email FROM Inserted);
	SET @new_password_salt = (SELECT password_salt FROM Inserted);
    SET @new_password_hash = (SELECT password_hash FROM Inserted);
	SET @new_token = (SELECT token FROM Inserted);
	SET @new_f_name = (SELECT f_name FROM Inserted);
	SET @new_l_name = (SELECT l_name FROM Inserted);
	SET @new_roles = (SELECT roles FROM Inserted);
	SET @new_amount_donated = (SELECT amount_donated FROM Inserted);
	SET @new_title = (SELECT title FROM Inserted);
	SET @new_type = (SELECT type FROM Inserted);
	SET @new_created = (SELECT created FROM Inserted);

    IF @new_roles != 'd' AND
       @new_roles != 's' AND
       @new_roles != 'e' AND
       @new_roles != 'a' AND
       @new_roles != 'sd' AND
       @new_roles != 'ed' AND
       @new_roles != 'ad'
        BEGIN
            THROW 51000, 'The Role entered does not exist', 1;
            ROLLBACK TRANSACTION
        END

    IF @new_roles = 'd'  OR
       @new_roles = 'sd' OR
       @new_roles = 'ed' OR
       @new_roles = 'ad'
            INSERT INTO Donators(email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated)
            VALUES (@new_email, @new_password_salt, @new_password_hash, @new_token, @new_f_name, @new_l_name, @new_roles, @new_amount_donated);
    IF @new_roles = 's' OR
       @new_roles = 'sd' OR
       @new_roles = 'e' OR
       @new_roles = 'a' OR
       @new_roles = 'ed' OR
       @new_roles = 'ad'
        INSERT INTO Staffs(email, password_salt, password_hash, token, f_name, l_name, roles, title, type, created)
        VALUES (@new_email, @new_password_salt, @new_password_hash, @new_token, @new_f_name, @new_l_name, @new_roles, @new_title, @new_type, @new_created);
    IF @new_roles = 'e' OR
       @new_roles = 'ed'
        INSERT INTO Engineers(email, password_salt, password_hash, token, f_name, l_name, roles, title, type)
        VALUES (@new_email, @new_password_salt, @new_password_hash, @new_token, @new_f_name, @new_l_name, @new_roles, @new_title, @new_type)
    IF @new_roles = 'a' OR
       @new_roles = 'ad'
        INSERT INTO Admins(email, password_salt, password_hash, token, f_name, l_name, roles, title, created)
        VALUES (@new_email, @new_password_salt, @new_password_hash, @new_token, @new_f_name, @new_l_name, @new_roles, @new_title, @new_created)
END


/*****TRIGGER CREATION ENDS    HERE*****/


/***STORED PROCEDURES STARTS HERE***/
/***STORED PROCEDURES ENDS   HERE***/
/**********PERSISTENT STORED MODULES END HERE**********/


GO -- NEED THIS GO STATEMENT TO EXECUTE THE TRIGGER INTO THE DATABASE THEN WE CAN INSERT






/**********DATA INSERTION STARTS HERE**********/
/*****INSERTION TEST DATA STARTS HERE*****/
-- I made some constants to help you guys with the insert statements
DECLARE @donator_role VARCHAR(2)
DECLARE @staff_role VARCHAR(2)
DECLARE @engineer_role VARCHAR(2)
DECLARE @admin_role VARCHAR(2)
DECLARE @staff_donator_role VARCHAR(2)
DECLARE @engineer_donator_role VARCHAR(2)
DECLARE @admin_donator_role VARCHAR(2)
-- a d s e ad ed sd
SET @donator_role = 'd'
SET @staff_role = 's'
SET @engineer_role = 'e'
SET @admin_role = 'a'
SET @staff_donator_role = 'sd'
SET @engineer_donator_role = 'ed'
SET @admin_donator_role = 'ad'

-- INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
-- VALUES (N'20200408192316_Initial', N'3.1.2');

INSERT INTO Projects (start_date, status, city, zip_code)
VALUES  ('04-09-2001',  'NEW',          'Vacaville',    '95688'),
        ('2001-03-09',	'NEW',	        'Rocklin',	    '95765'),
        ('2020-03-09',	'ESCALATED',	'Vacaville',	'95688'),
        ('2019-04-03',	'Completed',	'Sacramento',	'95825'),
        ('2018-04-03',	'Stage 1',	    'Dublin',	    '43016'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125');


/* Needs to be seperate because of the User_Created_Check trigger*/
/*For: Admin Donator */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('admin@test.com',              'password_salt', 'password_hash', '', 'admin',                'test', @admin_role,
        10.00,                          'title',       'Software Developer',    GETDATE());
/*For: Donator */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('donator@test.com',            'password_salt', 'password_hash', '', 'donator',              'test', @donator_role,
        10.00,                           NULL,           NULL,                   NULL);
/*For: Staff */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('staff@test.com',              'password_salt', 'password_hash', '', 'staff',                'test', @staff_role,
        NULL,                          'title',          NULL,                   NULL);
/*For: Engineer */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('engineer@test.com',           'password_salt', 'password_hash', '', 'engineer',             'test', @engineer_role,
        NULL,                          'title',        'Water Resources',        NULL);
/*For: Admin Donantor */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('admin_donator@test.com',      'password_salt', 'password_hash', '', 'admin_donator',        'test', @admin_donator_role,
        20.00,                         'Database Admin', NULL,                   GETDATE());
/*For: Engineer Donator */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('engineer_donator@test.com',   'password_salt', 'password_hash', '', 'engineer_donator',     'test', @engineer_donator_role,
        30.00,                         'title',          'Transportation',     NULL);
/*For: Staff Donator */
INSERT INTO [Users] (email, password_salt, password_hash, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('staff_donator@test.com',      'password_salt', 'password_hash', '', 'engineer_donator',     'test', @staff_donator_role,
        40.00,                         'title',          NULL,                   NULL);

INSERT INTO EngineerCertifications(email, certification)
VALUES ('engineer@test.com',            'Certified Recycling Systems - Technical Associate'),
       ('engineer@test.com',            'Diplomate, Geotechnical Engineering'),
       ('engineer_donator@test.com',    'Certified Planning Engineer'),
       ('engineer_donator@test.com',    'Certified Healthcare Constructor');

INSERT INTO EngineerDegrees(email, degree)
VALUES ('engineer@test.com',            'BS in Civil Engineering'),
       ('engineer@test.com',            'MS in Transportation Engineering'),
       ('engineer_donator@test.com',    'BS in Civil Engineering'),
       ('engineer_donator@test.com',    'MS in Water Resources Engineering');

INSERT INTO Responsibilities(staff_email, project_num)
VALUES ('engineer@test.com',         1),
       ('engineer@test.com',         2),
       ('engineer_donator@test.com', 2),
       ('engineer_donator@test.com', 3);

/*****INSERTION TEST DATA ENDS HERE*****/
/**********DATA INSERTION ENDS HERE**********/




/**********QUERIES STARTS HERE**********/
/***Basic Queries*****/
SELECT * FROM Projects;
SELECT * FROM [Users];
SELECT * FROM Donators;
SELECT * FROM Staffs;
SELECT * FROM Engineers;
SELECT * FROM Admins
SELECT * FROM EngineerCertifications;
SELECT * FROM EngineerDegrees;
SELECT * FROM Responsibilities;
/***Join Queries*****/

/********Named Queries**********/
/*****QUERIES ENDS HERE*****/



/**********DROPPING ANYTHING FROM DATABASE STARTS HERE**********/
DROP TABLE Responsibilities;
DROP TABLE Projects;
DROP TABLE EngineerCertifications;
DROP TABLE EngineerDegrees;
DROP TABLE Admins;
DROP TABLE Engineers;
DROP TABLE Donators;
DROP TABLE Staffs;
DROP TABLE [Users];
-- DROP TABLE _EFMigrationsHistory;
-- DROP TRIGGER User_Updated_Check;
/*****DROPPING ANYTHING FROM DATABASE ENDS HERE*****/




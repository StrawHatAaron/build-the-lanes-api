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
CREATE TABLE Project(
    project_num INTEGER NOT NULL IDENTITY, /*IDENTITY AUTO INCREMENTS*/
    start_date DATE NOT NULL,
    status VARCHAR(16) NOT NULL,
    city VARCHAR(45) NOT NULL,
    zip_code CHAR(5) NOT NULL,
    PRIMARY KEY (project_num)
);

CREATE TABLE Applicable_Standards(
    data_link VARCHAR(1024) NOT NULL,
    project_num INTEGER NOT NULL,
    photo_name VARCHAR(64) NOT NULL,
    PRIMARY KEY (data_link, project_num),
    FOREIGN KEY (project_num) REFERENCES Project(project_num)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

/*****ROLE BASED AUTH PROFILES SECTION STARTS HERE*****/
CREATE TABLE Users(
	email VARCHAR(320) NOT NULL,
	password VARCHAR(64) NOT NULL,
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
CREATE TABLE Donator(
	email VARCHAR(320) NOT NULL,
	password VARCHAR(64) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	amount_donated MONEY NOT NULL,
    donates_link VARCHAR(320),
    project_num INTEGER,
	PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Users(email),
    FOREIGN KEY (project_num) REFERENCES [Project](project_num)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Staff(
	email VARCHAR(320) NOT NULL,
	password VARCHAR(64) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	title VARCHAR(128) NOT NULL,
	type VARCHAR(256),
	created DATETIME,
	PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Users(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABlE Admin(
    email VARCHAR(320) NOT NULL,
	password VARCHAR(64) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	title VARCHAR(128) NOT NULL,
	created DATETIME NOT NULL,
    PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Staff(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Engineer(
    email VARCHAR(320) NOT NULL,
	password VARCHAR(64) NOT NULL,
	token VARCHAR(320),
	f_name VARCHAR(64) NOT NULL,
	l_name VARCHAR(64) NOT NULL,
	roles VARCHAR (2) NOT NULL,
	title VARCHAR(128) NOT NULL,
	type VARCHAR(256) NOT NULL,
    PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Staff(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
/***Engineer: Certification and Degree Data***/
/***Note: these aren't profiles. Just a tables for Engineer(s) with
          Multiple degrees/certifications. ***/
CREATE TABLE Engineer_Certifications(
    email VARCHAR(320) NOT NULL,
    certification VARCHAR(256),
    PRIMARY KEY (email, certification),
    FOREIGN KEY (email) REFERENCES Engineer(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Engineer_Degrees(
    email VARCHAR(320) NOT NULL,
    degree VARCHAR(256),
    PRIMARY KEY (email, degree),
    FOREIGN KEY (email) REFERENCES Engineer(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
/***Admin: Audited Actions***/
/***Note: these aren't profiles. Just actions that may be audited***/
CREATE TABLE Admin_Added_User(
    admin_email VARCHAR(320) NOT NULL,
    user_email  VARCHAR(320) NOT NULL,
    timestamp DATETIME,
    PRIMARY KEY (admin_email, user_email),
	FOREIGN KEY (admin_email) REFERENCES Admin(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

CREATE TABLE Admin_Deleted_User(
    admin_email VARCHAR(320) NOT NULL,
    user_email  VARCHAR(320) NOT NULL,
    timestamp DATETIME,
    PRIMARY KEY (admin_email, user_email),
	FOREIGN KEY (admin_email) REFERENCES Admin(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);
/*****ROLE BASED AUTH PROFILES SECTION ENDS HERE*****/
/**********TABLE CREATION ENDS HERE**********/


GO -- This GO statement is required to end the query statements and differ between creation of Tables and Triggers


/**********PERSISTENT STORED MODULES START HERE**********/
/***TRIGGER CREATION STARTS HERE***/



CREATE TRIGGER User_Created_Check
ON Users
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @new_email VARCHAR(320);
	DECLARE @new_password VARCHAR(64);
	DECLARE @new_token VARCHAR(320);
	DECLARE @new_f_name VARCHAR(64);
	DECLARE @new_l_name VARCHAR(64);
	DECLARE @new_roles VARCHAR (2);
	DECLARE @new_amount_donated MONEY;
	DECLARE @new_title VARCHAR(128);
	DECLARE @new_type VARCHAR(256);
	DECLARE @new_created DATETIME;

    SET @new_email = (SELECT email FROM Inserted);
	SET @new_password = (SELECT password FROM Inserted);
	SET @new_token = (SELECT token FROM Inserted);
	SET @new_f_name = (SELECT f_name FROM Inserted);
	SET @new_l_name = (SELECT l_name FROM Inserted);
	SET @new_roles = (SELECT roles FROM Inserted);
	SET @new_amount_donated = (SELECT amount_donated FROM Inserted);
	SET @new_title = (SELECT title FROM Inserted);
	SET @new_type = (SELECT type FROM Inserted);
	SET @new_created = (SELECT created FROM Inserted);

    IF @new_roles = 'd'  OR
       @new_roles = 'sd' OR
       @new_roles = 'ed' OR
       @new_roles = 'ad'
        INSERT INTO Donator(email, password, token, f_name, l_name, roles, amount_donated)
        VALUES (@new_email, @new_password, @new_token, @new_f_name, @new_l_name, @new_roles, @new_amount_donated);
    IF @new_roles = 's' OR
       @new_roles = 'sd' OR
       @new_roles = 'e' OR
       @new_roles = 'a' OR
       @new_roles = 'ed' OR
       @new_roles = 'ad'
        INSERT INTO Staff(email, password, token, f_name, l_name, roles, title, type, created)
        VALUES (@new_email, @new_password, @new_token, @new_f_name, @new_l_name, @new_roles, @new_title, @new_type, @new_created);
    IF @new_roles = 'e' OR
       @new_roles = 'ed'
        INSERT INTO Engineer(email, password, token, f_name, l_name, roles, title, type)
        VALUES (@new_email, @new_password, @new_token, @new_f_name, @new_l_name, @new_roles, @new_title, @new_type)
    IF @new_roles = 'a' OR
       @new_roles = 'ad'
        INSERT INTO Admin(email, password, token, f_name, l_name, roles, title, created)
        VALUES (@new_email, @new_password, @new_token, @new_f_name, @new_l_name, @new_roles, @new_title, @new_created)
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



INSERT INTO Project (start_date, status, city, zip_code)
VALUES  ('04-09-2001',  'NEW',          'Vacaville',    '95688'),
        ('2001-03-09',	'NEW',	        'Rocklin',	    '95765'),
        ('2020-03-09',	'ESCALATED',	'Vacaville',	'95688'),
        ('2019-04-03',	'Completed',	'Sacramento',	'95825'),
        ('2018-04-03',	'Stage 1',	    'Dublin',	    '43016'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125');

INSERT INTO Applicable_Standards(data_link, project_num, photo_name)
VALUES ('https://avatars2.githubusercontent.com/u/25778774?s=400&u=9d632b219a820cc7c56f1345ca20cabe34788f89&v=4',
        1, 'Photo 1'),
       ('https://avatars2.githubusercontent.com/u/37526270?s=400&v=4',
        2, 'Photo 2'),
       ('https://avatars3.githubusercontent.com/u/44451183?s=400&v=4',
        3, 'Photo 3');

/* Needs to be seperate because of the User_Created_Check trigger*/
/*For: Admin Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('admin@test.com',              'password', '', 'admin',                'test', @admin_role,
        10.00,                          'Title',       'Software Developer',    GETDATE());
/*For: Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('donator@test.com',            'password', '', 'donator',              'test', @donator_role,
        10.00,                           NULL,           NULL,                   NULL);
/*For: Staff */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('staff@test.com',              'password', '', 'staff',                'test', @staff_role,
        NULL,                          'Title',          NULL,                   NULL);
/*For: Engineer */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('engineer@test.com',           'password', '', 'engineer',             'test', @engineer_role,
        NULL,                          'Title',        'Water Resources',        NULL);
/*For: Admin Donantor */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('admin_donator@test.com',      'password', '', 'admin_donator',        'test', @admin_donator_role,
        20.00,                         'Database Admin', NULL,                   GETDATE());
/*For: Engineer Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('engineer_donator@test.com',   'password', '', 'engineer_donator',     'test', @engineer_donator_role,
        30.00,                         'Title',          'Transportation',     NULL);
/*For: Staff Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('staff_donator@test.com',      'password', '', 'engineer_donator',     'test', @staff_donator_role,
        40.00,                         'Title',          NULL,                   NULL);

INSERT INTO Engineer_Certifications(email, certification)
VALUES ('engineer@test.com',            'Certified Recycling Systems - Technical Associate'),
       ('engineer@test.com',            'Diplomate, Geotechnical Engineering'),
       ('engineer_donator@test.com',    'Certified Planning Engineer'),
       ('engineer_donator@test.com',    'Certified Healthcare Constructor');

INSERT INTO Engineer_Degrees(email, degree)
VALUES ('engineer@test.com',            'BS in Civil Engineering'),
       ('engineer@test.com',            'MS in Transportation Engineering'),
       ('engineer_donator@test.com',    'BS in Civil Engineering'),
       ('engineer_donator@test.com',    'MS in Water Resources Engineering');

INSERT INTO Admin_Added_User(admin_email, user_email, timestamp)
VALUES ('admin@test.com',           'test1@test.com', GETDATE()),
       ('admin@test.com',           'test2@test.com', GETDATE()),
       ('admin_donator@test.com',   'test3@test.com', GETDATE()),
       ('admin_donator@test.com',   'test4@test.com', GETDATE());

INSERT INTO Admin_Deleted_User(admin_email, user_email, timestamp)
VALUES ('admin@test.com',           'test1@test.com', GETDATE()),
       ('admin@test.com',           'test2@test.com', GETDATE()),
       ('admin_donator@test.com',   'test3@test.com', GETDATE()),
       ('admin_donator@test.com',   'test4@test.com', GETDATE());
/*****INSERTION TEST DATA ENDS HERE*****/
/**********DATA INSERTION ENDS HERE**********/




/**********QUERIES STARTS HERE**********/
/***Basic Queries*****/
SELECT * FROM Project;
SELECT * FROM Applicable_Standards;
SELECT * FROM Users;
SELECT * FROM Donator;
SELECT * FROM Staff;
SELECT * FROM Engineer;
SELECT * FROM Admin;
SELECT * FROM Engineer_Certifications;
SELECT * FROM Engineer_Degrees;
SELECT * FROM Admin_Added_User;
SELECT * FROM Admin_Deleted_User;
/***Join Queries*****/

/********Named Queries**********/
/*****QUERIES ENDS HERE*****/



/**********DROPPING ANYTHING FROM DATABASE STARTS HERE**********/
DROP TABLE Applicable_Standards;
DROP TABLE Project;
DROP TABLE Engineer_Certifications;
DROP TABLE Engineer_Degrees;
DROP TABLE Admin_Added_User;
DROP TABLE Admin_Deleted_User;
DROP TABLE Admin;
DROP TABLE Engineer;
DROP TABLE Donator;
DROP TABLE Staff;
DROP TABLE Users;
-- DROP TRIGGER User_Updated_Check;
/*****DROPPING ANYTHING FROM DATABASE ENDS HERE*****/



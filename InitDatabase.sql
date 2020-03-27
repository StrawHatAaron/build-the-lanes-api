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

CREATE TABLE Project_Photos(
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
	amount_donated MONEY NOT NULL,
	PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Users(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Staff(
	email VARCHAR(320) NOT NULL,
	title VARCHAR(128) NOT NULL,
	PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Users(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABlE Admin(
    email VARCHAR(320) NOT NULL,
    created DATETIME NOT NULL
    PRIMARY KEY (email),
	FOREIGN KEY (email) REFERENCES Staff(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Engineer(
    email VARCHAR(320) NOT NULL,
    type VARCHAR(256) NOT NULL
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
    PRIMARY KEY (admin_email),
	FOREIGN KEY (admin_email) REFERENCES Admin(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

CREATE TABLE Admin_Deleted_User(
    admin_email VARCHAR(320) NOT NULL,
    user_email  VARCHAR(320) NOT NULL,
    timestamp DATETIME,
    PRIMARY KEY (admin_email),
	FOREIGN KEY (admin_email) REFERENCES Admin(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);
/*****ROLE BASED AUTH PROFILES SECTION ENDS HERE*****/
/**********TABLE CREATION ENDS HERE**********/



/**********PERSISTENT STORED MODULES START HERE**********/
/***TRIGGER CREATION STARTS HERE***/

CREATE TRIGGER User_Created_Check
ON Users
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @new_email VARCHAR(320)
	DECLARE @new_roles VARCHAR (2)/*d=Donator | s=Staff | e=Engineer | a=Admin   so...  ads=[Staff, Donator, Admin]*/
	DECLARE @new_amount_donated MONEY/*For: Donator */
	DECLARE @new_title VARCHAR(128)/*For: Staff */
	DECLARE @new_type VARCHAR(256)/*For: Engineer */
	DECLARE @new_created DATETIME/*For: Admin */

    SET @new_email = (SELECT email FROM inserted)
    SET @new_roles = (SELECT roles FROM inserted)
    SET @new_amount_donated = (SELECT amount_donated FROM inserted)
    SET @new_title = (SELECT title FROM inserted)
    SET @new_type = (SELECT type FROM inserted)
    SET @new_created = (SELECT created FROM inserted)
    -- a d s e ad ed sd
    IF @new_roles = 'd'
        INSERT INTO Donator(email, amount_donated)
        VALUES (@new_roles, @new_amount_donated);
    ELSE IF @new_roles = 's'
        INSERT INTO Staff(email, title)
        VALUES (@new_email, @new_title);
    ELSE IF @new_roles = 'e'


--         it has become obvious to me that instead of a nested if
--         statement i should just use more triggers for each table.
--         it will be much MUCH more optimized that way by not making
--         the extra queries for deep sub-classes pertained to this
--         table

        INSERT INTO Staff(email, title)
        VALUES (@new_email, @new_title);
        INSERT INTO Engineer (email, type)
        VALUES (@new_email, @new_type);
    ELSE IF @new_roles = 'a'
    ELSE IF @new_roles = 'sd'
    ELSE IF @new_roles = 'ed'
    ELSE IF @new_roles = 'sd'
END
/*****TRIGGER CREATION ENDS    HERE*****/


/***STORED PROCEDURES STARTS HERE***/
/***STORED PROCEDURES ENDS   HERE***/
/**********PERSISTENT STORED MODULES END HERE**********/



/**********DATA INSERTION STARTS HERE**********/
/*****INSERTION TEST DATA STARTS HERE*****/
INSERT INTO Project (start_date, status, city, zip_code)
VALUES  ('04-09-2001',  'NEW',          'Vacaville',    '95688'),
        ('2001-03-09',	'NEW',	        'Rocklin',	    '95765'),
        ('2020-03-09',	'ESCALATED',	'Vacaville',	'95688'),
        ('2019-04-03',	'Completed',	'Sacramento',	'95825'),
        ('2018-04-03',	'Stage 1',	    'Dublin',	    '43016'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125');

INSERT INTO Project_Photos(data_link, project_num, photo_name)
VALUES ('https://avatars2.githubusercontent.com/u/25778774?s=400&u=9d632b219a820cc7c56f1345ca20cabe34788f89&v=4',
        1, 'Photo 1'),
       ('https://avatars2.githubusercontent.com/u/37526270?s=400&v=4',
        2, 'Photo 2'),
       ('https://avatars3.githubusercontent.com/u/44451183?s=400&v=4',
        3, 'Photo 3');

/* Needs to be seperate because of the User_Created_Check trigger*/
/*For: Admin Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('admin@test.com',              'password', '', 'admin',                'test', 'a',
        10.00,                          'Title',       'Software Developer',    GETDATE());
/*For: Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('donator@test.com',            'password', '', 'donator',              'test', 'd',
        0.00,                           NULL,           NULL,                   NULL);
/*For: Staff */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('staff@test.com',              'password', '', 'staff',                'test', 's',
        NULL,                          'Title',         NULL,                   NULL)
/*For: Engineer */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('engineer@test.com',           'password', '', 'engineer',             'test', 'e',
        NULL,                          'Title',         NULL,                   NULL);
/*For: Admin Donantor */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('admin_donator@test.com',      'password', '', 'admin_donator',        'test', 'ad',
        20.00,                         'Title',        'Database Admin',       NULL);
/*For: Engineer Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('engineer_donator@test.com',   'password', '', 'engineer_donator',     'test', 'ed',
        30.00,                         'Title',         NULL,                   GETDATE())
/*For: Staff Donator */
INSERT INTO Users (email, password, token, f_name, l_name, roles, amount_donated, title,  type, created)
VALUES ('staff_donator@test.com',      'password', '', 'engineer_donator',     'test', 'sd',
        40.00,                         'Title',         NULL,                   NULL);

-- INSERT INTO Engineer_Certifications(email, certification)
-- VALUES ('engineer@test.com',            'Certified Recycling Systems - Technical Associate'),
--        ('engineer@test.com',            'Diplomate, Geotechnical Engineering'),
--        ('engineer_donator@test.com',    'Certified Planning Engineer'),
--        ('engineer_donator@test.com',    'Certified Healthcare Constructor');
--
-- INSERT INTO Engineer_Degrees(email, degree)
-- VALUES ('engineer@test.com',            'BS in Civil Engineering'),
--        ('engineer@test.com',            'MS in Transportation Engineering'),
--        ('engineer_donator@test.com',    'BS in Civil Engineering'),
--        ('engineer_donator@test.com',    'MS in Water Resources Engineering');
--
-- INSERT INTO Admin_Added_User(admin_email, user_email, timestamp)
-- VALUES ('admin@test.com',       'test1@test.com', GETDATE()),
--        ('admin@test.com',       'test2@test.com', GETDATE()),
--        ('admin_donator@test',   'test3@test.com', GETDATE()),
--        ('admin_donator@test',   'test4@test.com', GETDATE());
--
-- INSERT INTO Admin_Deleted_User(admin_email, user_email, timestamp)
-- VALUES ('admin@test.com',       'test1@test.com', GETDATE()),
--        ('admin@test.com',       'test2@test.com', GETDATE()),
--        ('admin_donator@test',   'test3@test.com', GETDATE()),
--        ('admin_donator@test',   'test4@test.com', GETDATE());
/*****INSERTION TEST DATA ENDS HERE*****/
/**********DATA INSERTION ENDS HERE**********/




/**********QUERIES STARTS HERE**********/
/***Basic Queries*****/
SELECT * FROM Project;
SELECT * FROM Project_Photos;
SELECT * FROM Users;
SELECT * FROM Donator;
SELECT * FROM Staff;
/***Join Queries*****/

/********Named Queries**********/
/*****QUERIES ENDS HERE*****/



/**********DROPPING ANYTHING FROM DATABASE STARTS HERE**********/
DROP TABLE Project_Photos;
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
DROP TRIGGER User_Created_Check;
/*****DROPPING ANYTHING FROM DATABASE ENDS HERE*****/



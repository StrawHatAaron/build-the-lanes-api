use webapp;

/**********************************************************************************
Group Project CSC 174 | Build The Lanes
Authors: Aaron Miller, Marina Stangl, Hector Romo
SQL Version: Microsoft SQL Server Express Edition 14.00.3223.3.v1
Host: build-the-lanes-0.cz837oegnsiw.us-west-1.rds.amazonaws.com,1433
User: admin
Password: [Ask an Author]
***********************************************************************************/

/*****TABLE CREATION STARTS HERE*****/
CREATE TABLE Project(
    project_num INTEGER NOT NULL IDENTITY, /*IDENTITY AUTO INCREMENTS*/
    start_date DATE NOT NULL,
    status VARCHAR(16) NOT NULL,
    city VARCHAR(45) NOT NULL,
    zip_code CHAR(5) NOT NULL,
    PRIMARY KEY (project_num)
);

CREATE TABLE ProjectPhotos(
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
    PRIMARY KEY (email),
    FOREIGN KEY (email) REFERENCES Engineer(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Engineer_Degrees(
    email VARCHAR(320) NOT NULL,
    degree VARCHAR(256),
    PRIMARY KEY (email),
    FOREIGN KEY (email) REFERENCES Engineer(email)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
/*****ROLE BASED AUTH PROFILES SECTION ENDS HERE*****/
/*****TABLE CREATION ENDS HERE*****/



/*****DATA INSERTION STARTS HERE*****/
/*****INSERTION TEST DATA STARTS HERE*****/
INSERT INTO Project (start_date, status, city, zip_code)
VALUES  ('04-09-2001',  'NEW',          'Vacaville',    '95688'),
        ('2001-03-09',	'NEW',	        'Rocklin',	    '95765'),
        ('2020-03-09',	'ESCALATED',	'Vacaville',	'95688'),
        ('2019-04-03',	'Completed',	'Sacramento',	'95825'),
        ('2018-04-03',	'Stage 1',	    'Dublin',	    '43016'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125'),
        ('2018-04-03',	'Stage 2',	    'Worthington',	'42125');

INSERT INTO Users (email, password, token, f_name, l_name,
	/*d=Donator | s=Staff | e=Engineer | a=Admin   so...  ads=[Staff, Donator, Admin]*/
	roles,
	/*For: Donator */
	amount_donated,
	/*For: Staff */
	title,
	/*For: Engineer */
	type,
	/*For: Admin */
	created)
VALUES
    /*For: Admin Donator */
    ('admin@test.com',              'password', '', 'admin',                'test', 'a',
    10.00,                          'Title',       'Software Developer',    GETDATE()),
    /*For: Donator */
    ('donator@test.com',            'password', '', 'donator',              'test', 'd',
    NULL,                           NULL,           NULL,                   NULL),
    /*For: Staff */
    ('staff@test.com',              'password', '', 'staff',                'test', 's',
    NULL,                           'Title',        NULL,                   NULL),
    /*For: Engineer */
    ('engineer@test.com',           'password', '', 'engineer',             'test', 'e',
    NULL,                           'Title',        NULL,                   NULL),
    /*For: Admin Donantor */
    ('admin_donator@test.com',      'password', '', 'admin_donator',        'test', 'ad',
    20.00,                          'Title',        'Database Admin',       NULL),
    /*For: Engineer Donator */
    ('engineer_donator@test.com',   'password', '', 'engineer_donator',     'test', 'ed',
    30.00,                          'Title',        NULL,                   GETDATE()),
    /*For: Staff Donator */
    ('staff_donator@test.com',      'password', '', 'engineer_donator',     'test', 'sd',
    40.00,                          'Title',        NULL,                   NULL);


/*****INSERTION TEST DATA ENDS HERE*****/
/*****DATA INSERTION ENDS HERE*****/



/*****QUERIES STARTS HERE*****/
/***Basic Queries*****/
SELECT * FROM Project as Projects;
SELECT * FROM Users AS Users;
/***Join Queries*****/

/***Named Queries*****/
/*****QUERIES ENDS HERE*****/



/*****DROPPING ANYTHING FROM DATABASE STARTS HERE*****/
DROP TABLE ProjectPhotos;
DROP TABLE Project;
DROP TABLE Engineer_Certifications;
DROP TABLE Engineer_Degrees;
DROP TABLE Admin;
DROP TABLE Engineer;
DROP TABLE Donator;
DROP TABLE Staff;
DROP TABLE Users;
/*****DROPPING ANYTHING FROM DATABASE ENDS HERE*****/



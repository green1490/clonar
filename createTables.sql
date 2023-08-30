CREATE TABLE account (
	id 			serial PRIMARY KEY,
	email 		VARCHAR ( 30 ) UNIQUE NOT NULL,
	username 	VARCHAR ( 10 ) UNIQUE NOT NULL,
	password 	VARCHAR ( 12 ) NOT NULL
);

CREATE TABLE collection (
	id			serial PRIMARY KEY,
	ownerID		integer REFERENCES account(id),
	colName		VARCHAR(30) UNIQUE NOT NULL,
	description	VARCHAR(200) NOT NULL
);

CREATE TABLE thread (
	id 				serial PRIMARY KEY,
	userID	 		integer REFERENCES account(id),
	title			VARCHAR(100) NOT NULL,
	threadText		VARCHAR(3000) NOT NULL,
	collectionId	integer REFERENCES collection(id),
	deleted			boolean NOT NULL,
	karma			integer
);

CREATE TABLE comment (
	id			serial PRIMARY KEY,
	userID		integer REFERENCES account(id),
	parentID	integer REFERENCES comment(id),
	threadID	integer REFERENCES thread(id),
	userText	VARCHAR(3000) NOT NULL,
	deleted		boolean NOT NULL,
	karma 		integer
);

CREATE TABLE karma (
	userID  		integer REFERENCES account(id),
	threadKarma 	integer REFERENCES thread(id),
	commentKarma 	integer REFERENCES comment(id) 
);
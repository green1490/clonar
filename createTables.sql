CREATE TABLE account (
	id 			serial PRIMARY KEY,
	email 		VARCHAR ( 30 ) UNIQUE NOT NULL,
	username 	VARCHAR ( 10 ) UNIQUE NOT NULL,
	password 	VARCHAR ( 12 ) NOT NULL,
	karma 		integer DEFAULT 0
);


CREATE TABLE collection (
	id			serial PRIMARY KEY,
	colName		VARCHAR(30) UNIQUE NOT NULL,
	description	VARCHAR(200) NOT NULL
);

CREATE TABLE thread (
	id 				serial PRIMARY KEY,
	userID	 		integer REFERENCES account(id),
	title			VARCHAR(100) NOT NULL,
	threadText		VARCHAR(3000) NOT NULL,
	collectionId	integer REFERENCES collection(id) ON DELETE CASCADE,
	deleted			boolean NOT NULL,
	image			text	
);

CREATE TABLE comment (
	id			serial PRIMARY KEY,
	userID		integer REFERENCES account(id),
	parentID	integer REFERENCES comment(id),
	threadID	integer REFERENCES thread(id) ON DELETE CASCADE,
	userText	VARCHAR(3000) NOT NULL,
	deleted		boolean NOT NULL
);
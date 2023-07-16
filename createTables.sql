CREATE TABLE comment (
	id			serial 	PRIMARY KEY,
	parentID	integer,
	userText	text NOT NULL,
	deleted		boolean NOT NULL
);

CREATE TABLE account (
	id 			serial PRIMARY KEY,
	email 		VARCHAR ( 30 ) UNIQUE NOT NULL,
	username 	VARCHAR ( 10 ) UNIQUE NOT NULL,
	password 	VARCHAR ( 12 ) NOT NULL,
	karma 		integer DEFAULT 0
);

CREATE TABLE accountComment (
	id			serial PRIMARY KEY,
	commentID	integer REFERENCES Comment(id),
	userID		integer REFERENCES account(id)
);

CREATE TABLE thread (
	id 			serial PRIMARY KEY,
	userID	 	integer REFERENCES account(id),
	commentsID	integer REFERENCES AccountComment(id),
	deleted		boolean NOT NULL
);

CREATE TABLE threadCol (
	id			serial PRIMARY KEY,
	threadID	integer REFERENCES Thread(id)
);
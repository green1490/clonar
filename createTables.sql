CREATE TABLE Comments (
	id			serial 	PRIMARY KEY,
	parentID	integer,
	userText	text NOT NULL,
	deleted		boolean NOT NULL
);

CREATE TABLE Users (
	id 			serial PRIMARY KEY,
	email 		VARCHAR ( 30 ) UNIQUE NOT NULL,
	username 	VARCHAR ( 10 ) UNIQUE NOT NULL,
	password 	VARCHAR ( 12 ) NOT NULL,
	karma 		integer DEFAULT 0
);

CREATE TABLE UsersComments (
	id			serial PRIMARY KEY,
	commentID	integer REFERENCES Comments(id),
	userID		integer REFERENCES Users(id)
);

CREATE TABLE Thread (
	id 			serial PRIMARY KEY,
	userID	 	integer REFERENCES Users(id),
	commentsID	integer REFERENCES UsersComments(id),
	deleted		boolean NOT NULL
);

CREATE TABLE ThreadCol (
	id			serial PRIMARY KEY,
	threadID	integer REFERENCES Thread(id)
);
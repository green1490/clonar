CREATE TABLE users (
	id serial PRIMARY KEY,
	username VARCHAR ( 10 ) UNIQUE NOT NULL,
	password VARCHAR ( 12 ) NOT NULL,
	email VARCHAR ( 30 ) UNIQUE NOT NULL
);
use society_db;

CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY(1, 1),
    username VARCHAR(50) NOT NULL,
    password VARCHAR(100) NOT NULL,
    name VARCHAR(100),
    role VARCHAR(50),
    batch VARCHAR(50),
    degree VARCHAR(50)
);

delete from users where id=33;

create table societies(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(50) NOT NULL,
	description VARCHAR(1000) NOT NULL,
	num_users INT NOT NULL,
	head VARCHAR(50) NOT NULL
);

ALter table users add society varchar(100);

select * from users;
select * from societies;
select * from events_table;

CREATE TABLE events_table (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    description VARCHAR(255),
    [date] DATE,
    venue VARCHAR(100)
);
ALTER TABLE events_table
ADD society_name VARCHAR(100);

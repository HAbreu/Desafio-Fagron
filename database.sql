CREATE DATABASE fagronClients;

use fagronClients;

CREATE TABLE Client(
	Id int auto_increment primary key,
    Name varchar(30) not null,
    Surname varchar(100) not null,
    CPF varchar(11) not null,
    Bday date not null,
    Age int not null,
    Profession int
);

INSERT INTO Client (Name, Surname, CPF, Bday, Age, Profession) VALUES ('Henrique','de Abreu','097.307.949-52', '29/12/1998', '20', '1');
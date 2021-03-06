CREATE TABLE usuario (
	id int identity primary key,
	nome varchar(50) not null,
	login varchar(30) not null,
	senha varchar(20) not null,
	datacadastro datetime2(0) default getDate() not null
	)

CREATE TABLE topicoforum (
	id int identity primary key,
	titulo varchar(30) not null,
	descricao varchar(50) not null,
	datacadastro datetime2(0) default getDate() not null
	)

CREATE TABLE postagem (
	id int identity primary key,
	idtopico int foreign key references topicoforum not null,
	idusuario int foreign key references usuario not null,
	mensagem text not null,
	datapublicacao datetime2(0) default getDate() not null
	)

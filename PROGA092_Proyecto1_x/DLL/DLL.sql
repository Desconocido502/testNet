CREATE DATABASE IF NOT EXISTS VideojuegosDB;
USE VideojuegosDB;

CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    username VARCHAR(45) NOT NULL UNIQUE,
    date_nac VARCHAR(45),
    email VARCHAR(45) NOT NULL UNIQUE,
    password_ VARCHAR(45) NOT NULL,
    tipo_user VARCHAR(45) NOT NULL
);

CREATE TABLE VideoJuego (
    idVideoJuego INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    categoria VARCHAR(45) NOT NULL,
    precio DOUBLE NOT NULL,
    empresa_desarrollo VARCHAR(45) NOT NULL,
    year_lanzamiento INT NOT NULL
);

CREATE TABLE DLC (
    idDLC INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    precio DOUBLE NOT NULL,
    year_Lanzamiento INT NOT NULL,
    VideoJuego_idVideoJuego INT NOT NULL,
    FOREIGN KEY (VideoJuego_idVideoJuego) REFERENCES VideoJuego(idVideoJuego)
);

CREATE TABLE Compra (
    idCompra INT PRIMARY KEY AUTO_INCREMENT,
    Num_factura INT NOT NULL,
    tipo_compra VARCHAR(45) NOT NULL,
    fecha_hora VARCHAR(45) NOT NULL,
    NIT VARCHAR(45),
    Total_Pagar VARCHAR(45) NOT NULL,
    comentario VARCHAR(45),
    Usuario_idUsuario INT NOT NULL,
    VideoJuego_idVideoJuego INT NOT NULL,
    FOREIGN KEY (Usuario_idUsuario) REFERENCES Usuario(idUsuario),
    FOREIGN KEY (VideoJuego_idVideoJuego) REFERENCES VideoJuego(idVideoJuego)
);

CREATE TABLE review (
    idReview INT PRIMARY KEY AUTO_INCREMENT,
    comentario TEXT NOT NULL,
    fecha_review DATETIME NOT NULL,
    usuario_id INT NOT NULL,
    videojuego_id INT NOT NULL,
    dlc_id INT, -- puede ser NULL si no aplica
    FOREIGN KEY (usuario_id) REFERENCES Usuario(idUsuario),
    FOREIGN KEY (videojuego_id) REFERENCES VideoJuego(idVideoJuego),
    FOREIGN KEY (dlc_id) REFERENCES DLC(idDLC)
);

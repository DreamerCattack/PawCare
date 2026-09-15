CREATE DATABASE PawCare;
GO

USE PawCare;
GO

CREATE TABLE Mascotas
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreMascota VARCHAR(100) NOT NULL,
    NombreDueno VARCHAR(100) NOT NULL,
    Tipo VARCHAR(20) NOT NULL,
    Edad INT NOT NULL,
    Telefono VARCHAR(9) NOT NULL,
    Observaciones VARCHAR(500) NULL
);
GO



CREATE PROCEDURE spListarMascotas
AS
BEGIN
    SELECT
        Id,
        NombreMascota,
        NombreDueno,
        Tipo,
        Edad,
        Telefono,
        Observaciones
    FROM Mascotas
    ORDER BY Id DESC;
END;
GO



CREATE PROCEDURE spInsertarMascota
    @NombreMascota VARCHAR(100),
    @NombreDueno VARCHAR(100),
    @Tipo VARCHAR(20),
    @Edad INT,
    @Telefono VARCHAR(9),
    @Observaciones VARCHAR(500)
AS
BEGIN
    INSERT INTO Mascotas
    (
        NombreMascota,
        NombreDueno,
        Tipo,
        Edad,
        Telefono,
        Observaciones
    )
    VALUES
    (
        @NombreMascota,
        @NombreDueno,
        @Tipo,
        @Edad,
        @Telefono,
        @Observaciones
    );
END;
GO


USE PawCare;
GO

INSERT INTO Mascotas
    (NombreMascota, NombreDueno, Tipo, Edad, Telefono, Observaciones)
VALUES
    ('Max', 'Carlos Perez', 'Perro', 5, '987654321', 'Vacunación anual'),
    ('Michi', 'Ana Torres', 'Gato', 3, '912345678', 'Control general'),
    ('Luna', 'Pedro Ramirez', 'Perro', 2, '956789123', 'Presenta alergia leve'),
    ('Nina', 'Maria Lopez', 'Otro', 4, '998877665', NULL);
GO
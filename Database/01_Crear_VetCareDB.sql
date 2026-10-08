IF DB_ID(N'VetCareDB') IS NULL
BEGIN
    CREATE DATABASE VetCareDB;
END;
GO
USE VetCareDB;
GO
CREATE TABLE dbo.Mascotas (
    IdMascota INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Mascotas PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Especie NVARCHAR(50) NOT NULL,
    Raza NVARCHAR(100) NULL,
    FechaNacimiento DATE NULL,
    NombrePropietario NVARCHAR(150) NOT NULL,
    TelefonoPropietario NVARCHAR(20) NULL,
    CONSTRAINT CK_Mascotas_Nombre CHECK (LEN(LTRIM(RTRIM(Nombre))) > 0),
    CONSTRAINT CK_Mascotas_Especie CHECK (LEN(LTRIM(RTRIM(Especie))) > 0),
    CONSTRAINT CK_Mascotas_Propietario CHECK (LEN(LTRIM(RTRIM(NombrePropietario))) > 0)
);
GO
CREATE TABLE dbo.Consultas (
    IdConsulta INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Consultas PRIMARY KEY,
    IdMascota INT NOT NULL,
    FechaConsulta DATETIME2(0) NOT NULL,
    Motivo NVARCHAR(250) NOT NULL,
    Diagnostico NVARCHAR(500) NULL,
    Tratamiento NVARCHAR(500) NULL,
    CONSTRAINT FK_Consultas_Mascotas FOREIGN KEY (IdMascota)
        REFERENCES dbo.Mascotas(IdMascota) ON DELETE NO ACTION,
    CONSTRAINT CK_Consultas_Motivo CHECK (LEN(LTRIM(RTRIM(Motivo))) > 0)
);
GO
CREATE INDEX IX_Consultas_Mascota_Fecha ON dbo.Consultas (IdMascota, FechaConsulta DESC);
GO

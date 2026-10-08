USE VetCareDB;
GO
INSERT INTO dbo.Mascotas
    (Nombre, Especie, Raza, FechaNacimiento, NombrePropietario, TelefonoPropietario)
VALUES
(N'Max',    N'Perro',  N'Labrador',  '2021-03-12', N'Ana Rodríguez',  N'8888-1001'),
(N'Luna',   N'Gato',   N'Siamés',    '2022-07-08', N'Carlos Mora',    N'8888-1002'),
(N'Rocky',  N'Perro',  N'Bulldog',   '2020-11-22', N'María Solís',    N'8888-1003'),
(N'Michi',  N'Gato',   N'Mestizo',   '2023-02-14', N'José Vargas',    N'8888-1004'),
(N'Toby',   N'Perro',  N'Beagle',    '2019-05-30', N'Valeria Castro', N'8888-1005'),
(N'Nala',   N'Gato',   N'Persa',     '2021-09-19', N'Andrés Jiménez', N'8888-1006'),
(N'Kiwi',   N'Ave',    N'Periquito', '2024-01-10', N'Sofía Hernández',N'8888-1007'),
(N'Chispa', N'Conejo', N'Enano',     '2023-06-01', N'Daniel Rojas',   N'8888-1008');
GO
INSERT INTO dbo.Consultas (IdMascota, FechaConsulta, Motivo, Diagnostico, Tratamiento)
SELECT m.IdMascota, v.FechaConsulta, v.Motivo, v.Diagnostico, v.Tratamiento
FROM (VALUES
(N'Max',    CAST('2026-02-10T09:00:00' AS datetime2(0)), N'Vacunación',         N'Paciente saludable',       N'Aplicación de vacuna'),
(N'Max',    CAST('2026-05-13T10:30:00' AS datetime2(0)), N'Picazón',            N'Dermatitis leve',          N'Champú medicado'),
(N'Max',    CAST('2026-08-11T14:00:00' AS datetime2(0)), N'Control',            N'Mejoría de piel',          N'Seguimiento'),
(N'Luna',   CAST('2026-03-03T09:15:00' AS datetime2(0)), N'Chequeo general',    N'Sin hallazgos',            N'Control anual'),
(N'Luna',   CAST('2026-07-18T11:00:00' AS datetime2(0)), N'Falta de apetito',   N'Gastritis leve',           N'Dieta blanda'),
(N'Rocky',  CAST('2026-01-25T08:30:00' AS datetime2(0)), N'Cojera',             N'Inflamación en pata',      N'Reposo y revisión'),
(N'Rocky',  CAST('2026-04-10T13:30:00' AS datetime2(0)), N'Control',            N'Evolución favorable',      N'Continuar reposo'),
(N'Michi',  CAST('2026-05-08T15:00:00' AS datetime2(0)), N'Vacunación',         N'Paciente saludable',       N'Aplicación de vacuna'),
(N'Toby',   CAST('2026-02-21T10:00:00' AS datetime2(0)), N'Limpieza dental',    N'Sarro moderado',           N'Profilaxis dental'),
(N'Toby',   CAST('2026-06-17T08:45:00' AS datetime2(0)), N'Control dental',     N'Encías saludables',        N'Higiene en casa'),
(N'Nala',   CAST('2026-03-22T14:00:00' AS datetime2(0)), N'Caída de pelo',      N'Dermatitis',               N'Tratamiento tópico'),
(N'Nala',   CAST('2026-09-02T12:00:00' AS datetime2(0)), N'Control',            N'Mejoría clínica',          N'Seguimiento'),
(N'Kiwi',   CAST('2026-04-06T11:30:00' AS datetime2(0)), N'Chequeo',            N'Paciente saludable',       N'Recomendaciones de alimentación'),
(N'Kiwi',   CAST('2026-08-20T09:30:00' AS datetime2(0)), N'Plumaje irregular',  N'Muda normal',              N'Observación'),
(N'Chispa', CAST('2026-06-03T10:15:00' AS datetime2(0)), N'Revisión general',   N'Sin hallazgos',            N'Control anual')
) AS v (NombreMascota, FechaConsulta, Motivo, Diagnostico, Tratamiento)
INNER JOIN dbo.Mascotas AS m ON m.Nombre = v.NombreMascota;
GO

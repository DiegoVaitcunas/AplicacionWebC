INSERT INTO tipos (nombre, descripcion, costoXHuesped) VALUES
('Standard', 'Cabaña con servicios Standard', 100),
('Premium', 'Cabaña con servicios Premium', 150),
('Premium Plus', 'Cabaña con servicios Premium Plus', 200),
('Business', 'Cabaña con servicios business', 250),
('Business Plus', 'Cabaña con servicios Business Plus', 300),
('Extra basic', 'Cabaña con servicios Extra basic', 90),
('Ejecutivo', 'Cabaña con servicios Ejecutivos', 270),
('Ejecutivo Plus', 'Cabaña con servicios Ejecutivo Plus', 290),
('Deluxe', 'Cabaña con servicios Deluxe', 290);

INSERT INTO cabañas (nombreTipo, habilitada, nombre_Valor, descripcion, poseeJacuzzi, capacidad, Fotos_Valor) VALUES
('Standard', 1, 'CabañaUno', 'Descripción de la cabaña 1', 0, 2, 'CabañaUnoFoto_001.jpeg'),
('Premium', 0, 'CabañaDos', 'Descripción de la cabaña 2', 1, 4, 'CabañaDosFoto_001.jpg'),
('Premium Plus', 1, 'CabañaTres', 'Descripción de la cabaña 3', 1, 6, 'CabañaTresFoto_001.jpg'),
('Business', 1, 'CabañaCuatro', 'Descripción de la cabaña 4', 1, 2, 'CabañaCuatroFoto_001.jpg'),
('Premium', 1, 'CabañaCinco', 'Descripción de la cabaña 5', 1, 4, 'CabañaCincoFoto_001.jpg'),
('Business Plus', 0, 'CabañaSeis', 'Descripción de la cabaña 6', 1, 6, 'CabañaSeisFoto_001.jpg'),
('Extra basic', 1, 'CabañaSiete', 'Descripción de la cabaña 7', 0, 2, 'CabañaSieteFoto_001.jpg'),
('Ejecutivo', 0, 'CabañaOcho', 'Descripción de la cabaña 8', 1, 4, 'CabañaOchoFoto_001.jpg'),
('Ejecutivo Plus', 1, 'CabañaNueve', 'Descripción de la cabaña 9', 1, 6, 'CabañaNueveFoto_001.jpg'),
('Deluxe', 0, 'CabañaDiez', 'Descripción de la cabaña 10', 0, 2, 'CabañaDiezFoto_001.jpg');

INSERT INTO usuarios (Email, Contrasena_Valor, NombreCompleto) VALUES
('Ignaciopataro@gmail.com', 'Nachopataro44', 'Ignacio Pataro'),
('Diegovaitcunas@gmail.com', 'DiegoVaicunas6', 'Diego Vaitcunas'),
('Lucaslopez@gmail.com', 'LucasLopez99', 'Lucas Lopez');

INSERT INTO mantenimientos (Habitacion, FechaMantenimiento_Valor, Descripcion, Costo, NombreTrabajador) VALUES
(1, '2023-01-01', 'Mantenimiento de la cabaña 1', 500, 'Ignacio Pataro'),
(3, '2023-01-02', 'Mantenimiento de la cabaña 3', 1000, 'Ignacio Pataro'),
(5, '2023-01-03', 'Mantenimiento de la cabaña 5', 750, 'Ignacio Pataro'),
(7, '2023-01-04', 'Mantenimiento de la cabaña 7', 900, 'Ignacio Pataro'),
(9, '2023-01-05', 'Mantenimiento de la cabaña 9', 1200, 'Diego Vaitcunas'),
(2, '2023-02-01', 'Mantenimiento de la cabaña 2', 600, 'Diego Vaitcunas'),
(4, '2023-02-02', 'Mantenimiento de la cabaña 4', 800, 'Diego Vaitcunas'),
(6, '2023-02-03', 'Mantenimiento de la cabaña 6', 1100, 'Lucas Lopez'),
(8, '2023-02-04', 'Mantenimiento de la cabaña 8', 950, 'Lucas Lopez'),
(10, '2023-02-05', 'Mantenimiento de la cabaña 10', 1400, 'Lucas Lopez');

INSERT INTO Configuraciones (Atributo, LimiteSuperior, LimiteInferior, LimiteInferiorDate, LimiteSuperiorDate) VALUES
('nombre', 80, 8, null, null),
('numeroHabitacion', 1000,0, null, null),
('capacidad', 10, 0, null, null),
('Costo', 1000, 0, null, null),
('FechaMantenimiento', null, null, ('2000-01-01 00:00:00'), GETDATE()),
('costoXHuesped', 1000, 0, null, null),
('Email', 100, 8, null, null),
('Contrasena', 100, 6, null, null),
('Foto', 100, 4, null, null);
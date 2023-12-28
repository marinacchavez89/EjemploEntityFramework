use alumno_bd;
INSERT INTO `alumno_bd`.`alumno` (`nombre`, `apellido`, `carrera`, `fecha_nac`, `edad`) VALUES
('Juan', 'Pérez', 'Ingeniería Informática', '1990-05-15', 32),
('María', 'Gómez', 'Psicología', '1985-08-22', 37),
('Carlos', 'Rodríguez', 'Arquitectura', '1995-03-10', 27);

use alumno_bd;
select * from alumno;

use alumno_bd;
select * from carrera;

ALTER TABLE `alumno_bd`.`alumno`
ADD COLUMN `carrera_id` INT,
ADD CONSTRAINT `fk_alumno_carrera`
    FOREIGN KEY (`carrera_id`)
    REFERENCES `alumno_bd`.`carrera` (`id`);
    
INSERT INTO `alumno_bd`.`carrera` (`nombre`) VALUES
('Ingeniería Informática'),
('Psicología'),
('Arquitectura'),
('Medicina'),
('Contabilidad');
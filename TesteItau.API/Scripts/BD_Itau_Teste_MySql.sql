CREATE DATABASE  IF NOT EXISTS `teste_itau` 
USE `teste_itau`;

DROP TABLE IF EXISTS `cliente`;

CREATE TABLE `cliente` (
  `IdCliente` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `Sobrenome` varchar(50) NOT NULL,
  `CPF` varchar(11) NOT NULL,
  `CEP` varchar(8) DEFAULT NULL,
  `Logradouro` varchar(200) DEFAULT NULL,
  `NumLogradouro` varchar(6) DEFAULT NULL,
  `Complemento` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IdCliente`),
  UNIQUE KEY `CPF_UNIQUE` (`CPF`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;


LOCK TABLES `cliente` WRITE;
INSERT INTO `cliente` VALUES (1,'Raul','Amaral','43658960846','12345987','Rua Barbosa','100','Apartamento 50'),(18,'Raul','Seixas','12344544444','43223428','Rua X','85','iuyiyuiyu');
UNLOCK TABLES;


DROP TABLE IF EXISTS `telefone`;
CREATE TABLE `telefone` (
  `IdTelefone` int(11) NOT NULL AUTO_INCREMENT,
  `IdCliente` int(11) NOT NULL,
  `TipoTelefone` varchar(15) NOT NULL,
  `DDD` varchar(2) NOT NULL,
  `Numero` varchar(9) NOT NULL,
  PRIMARY KEY (`IdTelefone`),
  KEY `IdCliente_fk_idx` (`IdCliente`),
  CONSTRAINT `IdCliente_fk` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;


LOCK TABLES `telefone` WRITE;
INSERT INTO `telefone` VALUES (19,18,'Residencial','11','47248499'),(29,18,'Comercial','11','995632178'),(30,1,'Celular','11','998527454'),(34,1,'Residencial','11','47859632');
UNLOCK TABLES;


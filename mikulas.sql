-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Dec 11. 11:03
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `mikulas`
--
CREATE DATABASE IF NOT EXISTS `mikulas` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `mikulas`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `versenyzok`
--

DROP TABLE IF EXISTS `versenyzok`;
CREATE TABLE `versenyzok` (
  `Id` int(100) NOT NULL,
  `Name` varchar(256) NOT NULL,
  `Pont1` int(11) NOT NULL,
  `Ido1` double NOT NULL,
  `Pont2` int(11) NOT NULL,
  `Ido2` double NOT NULL,
  `Pont3` int(11) NOT NULL,
  `Ido3` double NOT NULL,
  `Legjobbpont` int(11) NOT NULL,
  `Legjobbido` double NOT NULL,
  `Helyezes` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `versenyzok`
--

INSERT INTO `versenyzok` (`Id`, `Name`, `Pont1`, `Ido1`, `Pont2`, `Ido2`, `Pont3`, `Ido3`, `Legjobbpont`, `Legjobbido`, `Helyezes`) VALUES
(1, 'Nagy Péter', 8, 2.34, 0, 10.52, 7, 3.89, 8, 2.34, 0),
(2, 'Kiss József', 6, 3.2, 7, 2.95, 9, 1.15, 9, 1.15, 0),
(3, 'Szabó Anna', 0, 10, 8, 2.4, 8, 2.1, 8, 2.1, 0),
(4, 'Tóth László', 5, 4.1, 6, 3.18, 6, 3.05, 6, 3.05, 0),
(5, 'Molnár Csilla', 9, 1.85, 9, 1.95, 9, 1.8, 9, 1.8, 0),
(6, 'Balogh Ádám', 0, 12.4, 0, 10.3, 4, 5.9, 4, 5.9, 0),
(7, 'Varga Sándor', 7, 3.44, 8, 2.01, 8, 2.33, 8, 2.01, 0),
(8, 'Horváth Márta', 6, 3.05, 7, 2.5, 6, 3.12, 7, 2.5, 0),
(9, 'Farkas Gergő', 0, 11, 0, 10.2, 0, 12.33, 0, 12.33, 0),
(10, 'Lukács Enikő', 8, 2.9, 9, 1.3, 0, 10.99, 9, 1.3, 0);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `versenyzok`
--
ALTER TABLE `versenyzok`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Name` (`Name`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `versenyzok`
--
ALTER TABLE `versenyzok`
  MODIFY `Id` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

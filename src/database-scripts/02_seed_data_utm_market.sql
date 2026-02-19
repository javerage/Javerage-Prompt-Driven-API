/*
================================================================================
SCRIPT: 02_seed_data_utm_market.sql
AUTOR: Database Architect Senior (Gemini CLI)
FECHA: 2026-02-18
OBJETIVO: Carga de 250 productos (Seeding) para UTM Market - México 2025.
================================================================================
*/

USE [develop_courses_javerage];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;
BEGIN TRY
    -- Limpieza previa para asegurar idempotencia
    -- Nota: Usamos DELETE en lugar de TRUNCATE por posibles referencias de FK
    DELETE FROM dbo.DetalleVenta;
    DELETE FROM dbo.Producto;
    
    -- Reiniciar el contador de identidad
    DBCC CHECKIDENT ('dbo.Producto', RESEED, 0);

    -- Habilitar inserción manual de IDs
    SET IDENTITY_INSERT dbo.Producto ON;

    PRINT 'Iniciando carga de 250 productos (Mercado MX 2025)...';

-- BLOQUE 1: REFRESCOS Y BEBIDAS (ID 1-50)
INSERT INTO dbo.Producto (ProductoID, Nombre, SKU, Marca, Precio, Stock) VALUES
(1, 'Coca-Cola Original 600ml', '7501055300074', 'Coca-Cola', 21.50, 150),
(2, 'Coca-Cola Sin Azúcar 600ml', '7501055304744', 'Coca-Cola', 21.50, 80),
(3, 'Coca-Cola Light 600ml', '7501055303747', 'Coca-Cola', 21.50, 40),
(4, 'Coca-Cola Original 2.5L', '7501055310882', 'Coca-Cola', 42.00, 120),
(5, 'Coca-Cola Original 3L', '7501055310899', 'Coca-Cola', 51.50, 100),
(6, 'Sidral Mundet 600ml', '7501055312343', 'Mundet', 19.50, 90),
(7, 'Fanta Naranja 600ml', '7501055311230', 'Fanta', 18.50, 60),
(8, 'Sprite Lima-Limón 600ml', '7501055311247', 'Sprite', 18.50, 60),
(9, 'Fresca Toronja 600ml', '7501055311254', 'Fresca', 18.50, 60),
(10, 'Jarritos Mandarina 600ml', '7501071101235', 'Jarritos', 16.00, 100),
(11, 'Jarritos Tamarindo 600ml', '7501071101242', 'Jarritos', 16.00, 100),
(12, 'Jarritos Ponche 600ml', '7501071101259', 'Jarritos', 16.00, 100),
(13, 'Jarritos Piña 600ml', '7501071101266', 'Jarritos', 16.00, 100),
(14, 'Pepsi Black 600ml', '7501011115383', 'Pepsi', 19.00, 120),
(15, 'Pepsi Original 600ml', '7501011115390', 'Pepsi', 19.00, 150),
(16, 'Pepsi Original 1.5L', '7501011115406', 'Pepsi', 30.00, 90),
(17, 'Mirinda Naranja 600ml', '7501011115413', 'Mirinda', 17.50, 70),
(18, 'Manzanita Sol 600ml', '7501011115420', 'Manzanita Sol', 17.50, 70),
(19, '7Up Lima-Limón 600ml', '7501011115437', '7Up', 17.50, 70),
(20, 'Dr Pepper 600ml', '7501011115444', 'Dr Pepper', 23.50, 40),
(21, 'Sangría Casera 600ml', '7501011115451', 'Peñafiel', 20.00, 50),
(22, 'Peñafiel Mineral 600ml', '7501011115468', 'Peñafiel', 18.00, 80),
(23, 'Peñafiel Limón 600ml', '7501011115475', 'Peñafiel', 18.50, 70),
(24, 'Agua Ciel 600ml', '7501055315481', 'Ciel', 12.00, 200),
(25, 'Agua Ciel 1L', '7501055315498', 'Ciel', 16.50, 150),
(26, 'Agua Bonafont 600ml', '7506148315504', 'Bonafont', 13.00, 200),
(27, 'Agua Bonafont 1L', '7506148315511', 'Bonafont', 17.50, 150),
(28, 'Topo Chico Mineral 600ml', '7501055315528', 'Topo Chico', 24.50, 60),
(29, 'Coca-Cola Original 355ml Vidrio', '7501055315535', 'Coca-Cola', 17.50, 100),
(30, 'Sidral Mundet 355ml Vidrio', '7501055315542', 'Mundet', 16.00, 80),
(31, 'Valle Frut Cítricos 600ml', '7501055315559', 'Del Valle', 18.50, 100),
(32, 'Del Valle Naranja 413ml', '7501055315566', 'Del Valle', 16.50, 90),
(33, 'Del Valle Mango 413ml', '7501055315573', 'Del Valle', 16.50, 90),
(34, 'Del Valle Durazno 413ml', '7501055315580', 'Del Valle', 16.50, 90),
(35, 'Jumex Naranja 450ml Lata', '7501013115597', 'Jumex', 18.00, 120),
(36, 'Jumex Mango 450ml Lata', '7501013115603', 'Jumex', 18.00, 120),
(37, 'Jumex Durazno 450ml Lata', '7501013115610', 'Jumex', 18.00, 120),
(38, 'Jumex Guayaba 450ml Lata', '7501013115627', 'Jumex', 18.00, 120),
(39, 'Powerade Frutas 600ml', '7501055315634', 'Powerade', 25.50, 100),
(40, 'Powerade Lima-Limón 600ml', '7501055315641', 'Powerade', 25.50, 100),
(41, 'Gatorade Ponche 600ml', '7501011115658', 'Gatorade', 27.50, 100),
(42, 'Gatorade Naranja 600ml', '7501011115665', 'Gatorade', 27.50, 100),
(43, 'Red Bull Energy Drink 250ml', '9002490100070', 'Red Bull', 62.00, 50),
(44, 'Monster Energy 473ml', '0708470128471', 'Monster Energy', 55.00, 60),
(45, 'Volt Blue Energy 473ml', '7501055315672', 'Volt', 22.00, 80),
(46, 'Amper Energy Drink 473ml', '7501055315689', 'Amper', 21.00, 80),
(47, 'BeLight Limonada 1L', '7501011115696', 'BeLight', 19.50, 70),
(48, 'Lipton Té Limón 600ml', '7501011115702', 'Lipton', 19.50, 70),
(49, 'Fuze Tea Negro Durazno 600ml', '7501055315719', 'Fuze Tea', 21.00, 70),
(50, 'Fuze Tea Verde Limón 600ml', '7501055315726', 'Fuze Tea', 21.00, 70);

-- BLOQUE 2: BEBIDAS RESTANTES Y BOTANAS (ID 51-100)
INSERT INTO dbo.Producto (ProductoID, Nombre, SKU, Marca, Precio, Stock) VALUES
(51, 'Sangría Señorial 600ml', '7501011115733', 'Señorial', 19.50, 60),
(52, 'Mundet Fresa 600ml', '7501055315740', 'Mundet', 19.50, 50),
(53, 'Mundet Durazno 600ml', '7501055315757', 'Mundet', 19.50, 50),
(54, 'Squirt Toronja 600ml', '7501011115764', 'Squirt', 18.50, 90),
(55, 'Squirt Toronja 1.5L', '7501011115771', 'Squirt', 28.50, 60),
(56, 'Peñafiel Arándano 600ml', '7501011115788', 'Peñafiel', 18.50, 40),
(57, 'Peñafiel Fresa 600ml', '7501011115795', 'Peñafiel', 18.50, 40),
(58, 'Jumex Uva 450ml Lata', '7501013115801', 'Jumex', 18.00, 60),
(59, 'Jumex Manzana 450ml Lata', '7501013115818', 'Jumex', 18.00, 60),
(60, 'Pau Pau Fresa 250ml', '7501055315825', 'Pau Pau', 9.50, 100),
(61, 'Pau Pau Uva 250ml', '7501055315832', 'Pau Pau', 9.50, 100),
(62, 'Pau Pau Naranja 250ml', '7501055315849', 'Pau Pau', 9.50, 100),
(63, 'Coca-Cola Café 235ml Lata', '7501055315856', 'Coca-Cola', 16.00, 40),
(64, 'Papas Sabritas Sal 45g', '7501011115863', 'Sabritas', 21.00, 80),
(65, 'Papas Sabritas Adobadas 45g', '7501011115870', 'Sabritas', 22.00, 80),
(66, 'Ruffles Queso 50g', '7501011115887', 'Ruffles', 22.00, 80),
(67, 'Doritos Nacho 58g', '7501011115894', 'Doritos', 20.00, 100),
(68, 'Doritos Flamin Hot 58g', '7501011115900', 'Doritos', 20.00, 100),
(69, 'Cheetos Torciditos 52g', '7501011115917', 'Cheetos', 17.00, 120),
(70, 'Cheetos Poffs 45g', '7501011115924', 'Cheetos', 17.00, 100),
(71, 'Cheetos Colmillo 45g', '7501011115931', 'Cheetos', 17.00, 80),
(72, 'Tostitos Salsa Verde 65g', '7501011115948', 'Tostitos', 21.00, 60),
(73, 'Tostitos Flamin Hot 65g', '7501011115955', 'Tostitos', 21.00, 60),
(74, 'Fritos Sal y Limón 55g', '7501011115962', 'Fritos', 18.00, 80),
(75, 'Fritos Chorizo y Queso 55g', '7501011115979', 'Fritos', 18.00, 80),
(76, 'Takis Fuego 56g', '7501030415986', 'Barcel', 19.50, 120),
(77, 'Takis Original 56g', '7501030415993', 'Barcel', 18.50, 80),
(78, 'Takis Blue Heat 56g', '7501030416006', 'Barcel', 19.50, 100),
(79, 'Chips Fuego 55g', '7501030416013', 'Barcel', 22.00, 80),
(80, 'Chips Sal de Mar 55g', '7501030416020', 'Barcel', 22.00, 80),
(81, 'Runners 50g', '7501030416037', 'Barcel', 17.00, 100),
(82, 'Big Mix Original 50g', '7501030416044', 'Barcel', 20.00, 80),
(83, 'Kiwis 50g', '7501030416051', 'Barcel', 18.00, 60),
(84, 'Cacahuates Sabritas Sal 50g', '7501011116068', 'Sabritas', 16.00, 100),
(85, 'Cacahuates Japoneses Sabritas 50g', '7501011116075', 'Sabritas', 16.00, 100),
(86, 'Churrumais con Limón 50g', '7501011116082', 'Sabritas', 14.00, 120),
(87, 'Crujitos Queso y Chile 45g', '7501011116099', 'Sabritas', 17.00, 80),
(88, 'Paketaxo Mezcladito 65g', '7501011116105', 'Sabritas', 23.00, 60),
(89, 'Paketaxo Quexo 65g', '7501011116112', 'Sabritas', 23.00, 60),
(90, 'Hot Nuts Original 50g', '7501030416129', 'Barcel', 17.00, 100),
(91, 'Hot Nuts Fuego 50g', '7501030416136', 'Barcel', 17.50, 100),
(92, 'Cacahuates de la Esquina 50g', '7501030416143', 'Barcel', 15.00, 120),
(93, 'Palomitas ACT II Mantequilla 80g', '7501030416150', 'ACT II', 20.50, 80),
(94, 'Palomitas ACT II Extra Mantequilla 80g', '7501030416167', 'ACT II', 20.50, 80),
(95, 'Palomitas ACT II Chile Limón 80g', '7501030416174', 'ACT II', 20.50, 60),
(96, 'Gansito 50g', '7501000116181', 'Marinela', 19.50, 120),
(97, 'Pingüinos 2 pack 80g', '7501000116198', 'Marinela', 23.00, 100),
(98, 'Choco Roles 2 pack 80g', '7501000116204', 'Marinela', 23.00, 80),
(99, 'Submarinos Vainilla 3 pack', '7501000116211', 'Marinela', 21.00, 80),
(100, 'Napolitano 50g', '7501000116228', 'Marinela', 18.00, 60);

-- BLOQUE 3: BOTANAS, LÁCTEOS Y PANADERÍA (ID 101-150)
INSERT INTO dbo.Producto (ProductoID, Nombre, SKU, Marca, Precio, Stock) VALUES
(101, 'Barritas Fresa 67g', '7501000116235', 'Marinela', 19.50, 100),
(102, 'Canelitas 90g', '7501000116242', 'Marinela', 18.00, 90),
(103, 'Polvorones 113g', '7501000116259', 'Marinela', 19.00, 80),
(104, 'Triki Trakes 100g', '7501000116266', 'Marinela', 19.00, 80),
(105, 'Leche Lala Entera 1L', '7501020516273', 'Lala', 28.50, 100),
(106, 'Leche Lala Deslactosada 1L', '7501020516280', 'Lala', 30.50, 120),
(107, 'Leche Lala Light 1L', '7501020516297', 'Lala', 29.50, 80),
(108, 'Leche Alpura Clásica 1L', '7501015516303', 'Alpura', 28.00, 100),
(109, 'Leche Alpura Deslactosada 1L', '7501015516310', 'Alpura', 30.00, 100),
(110, 'Yoghurt Lala Fresa 220g Bebible', '7501020516327', 'Lala', 15.50, 120),
(111, 'Yoghurt Lala Durazno 220g Bebible', '7501020516334', 'Lala', 15.50, 120),
(112, 'Yoghurt Danone Fresa 220g Bebible', '7501032816341', 'Danone', 14.50, 100),
(113, 'Yoghurt Danup Fresa 350g', '7501032816358', 'Danup', 21.00, 80),
(114, 'Yoghurt Danup Durazno 350g', '7501032816365', 'Danup', 21.00, 80),
(115, 'Yoghurt Yoplait Fresa 242g', '7501020516372', 'Yoplait', 16.00, 100),
(116, 'Yoghurt Griego Oikos Natural 150g', '7501032816389', 'Oikos', 24.00, 60),
(117, 'Leche Chocolate Lala 250ml', '7501020516396', 'Lala', 12.50, 150),
(118, 'Leche Fresa Alpura 250ml', '7501015516402', 'Alpura', 12.50, 100),
(119, 'Crema Lala 200ml', '7501020516419', 'Lala', 19.50, 80),
(120, 'Crema Alpura 200ml', '7501015516426', 'Alpura', 19.00, 80),
(121, 'Queso Panela Lala 200g', '7501020516433', 'Lala', 48.00, 40),
(122, 'Queso Americano Lala 140g', '7501020516440', 'Lala', 26.00, 60),
(123, 'Mantequilla Lala con Sal 90g', '7501020516457', 'Lala', 22.00, 80),
(124, 'Media Crema Nestlé 190g', '7501058616464', 'Nestlé', 18.50, 100),
(125, 'Leche Condensada La Lechera 375g', '7501058616471', 'La Lechera', 29.50, 80),
(126, 'Leche Evaporada Carnation 360g', '7501058616488', 'Carnation', 22.50, 100),
(127, 'Pan Blanco Bimbo Grande 680g', '7501000116495', 'Bimbo', 50.00, 100),
(128, 'Pan Integral Bimbo Grande 680g', '7501000116501', 'Bimbo', 55.00, 80),
(129, 'Pan Blanco Bimbo Mediano 480g', '7501000116518', 'Bimbo', 42.00, 60),
(130, 'Medias Noches Bimbo 8 pack', '7501000116525', 'Bimbo', 45.00, 100),
(131, 'Bimbollos 8 pack', '7501000116532', 'Bimbo', 52.00, 80),
(132, 'Pan Tostado Bimbo Clásico 210g', '7501000116549', 'Bimbo', 32.00, 60),
(133, 'Tortillinas Tía Rosa 12 pack', '7501000116556', 'Tía Rosa', 28.00, 150),
(134, 'Tortillinas Tía Rosa 22 pack', '7501000116563', 'Tía Rosa', 46.00, 100),
(135, 'Doraditas Tía Rosa 110g', '7501000116570', 'Tía Rosa', 24.50, 80),
(136, 'Mantecadas Tía Rosa 4 pack', '7501000116587', 'Tía Rosa', 26.00, 120),
(137, 'Conchas Bimbo 2 pack', '7501000116594', 'Bimbo', 22.00, 80),
(138, 'Donas Bimbo 4 pack', '7501000116600', 'Bimbo', 24.00, 100),
(139, 'Roles de Canela Bimbo 2 pack', '7501000116617', 'Bimbo', 24.00, 80),
(140, 'Nito Bimbo 1 pack', '7501000116624', 'Bimbo', 18.50, 120),
(141, 'Colchones Naranja 6 pack', '7501000116631', 'Bimbo', 34.00, 60),
(142, 'Panqué de Nuez Bimbo 255g', '7501000116648', 'Bimbo', 48.00, 50),
(143, 'Panqué de Pasas Bimbo 255g', '7501000116655', 'Bimbo', 48.00, 40),
(144, 'Panqué Mármol Bimbo 255g', '7501000116662', 'Bimbo', 48.00, 40),
(145, 'Leche Santa Clara Entera 1L', '7501055316679', 'Santa Clara', 32.00, 60),
(146, 'Leche Santa Clara Deslactosada 1L', '7501055316686', 'Santa Clara', 34.00, 80),
(147, 'Yoghurt Santa Clara Fresa 200g', '7501055316693', 'Santa Clara', 18.00, 60),
(148, 'Helado Santa Clara Vainilla 1L', '7501055316709', 'Santa Clara', 125.00, 20),
(149, 'Choco Milk 400g Bolsa', '7501035016716', 'Choco Milk', 65.00, 80),
(150, 'Cal-C-Tose 400g Bolsa', '7501035016723', 'Cal-C-Tose', 68.00, 60);

-- BLOQUE 4: ABARROTES E HIGIENE (ID 151-200)
INSERT INTO dbo.Producto (ProductoID, Nombre, SKU, Marca, Precio, Stock) VALUES
(151, 'Mayonesa McCormick con Limón 190g', '7501003316730', 'McCormick', 28.50, 100),
(152, 'Mayonesa McCormick con Limón 390g', '7501003316747', 'McCormick', 52.00, 80),
(153, 'Atún Herdez en Agua 130g', '7501001116754', 'Herdez', 22.50, 150),
(154, 'Atún Herdez en Aceite 130g', '7501001116761', 'Herdez', 22.50, 150),
(155, 'Frijoles La Costeña Bayos Refritos 440g', '7501017016778', 'La Costeña', 18.00, 200),
(156, 'Frijoles Isadora Bayos Refritos 430g', '7501071316785', 'Isadora', 20.50, 150),
(157, 'Chiles Jalapeños La Costeña 220g', '7501017016792', 'La Costeña', 14.50, 120),
(158, 'Salsa Casera Herdez 210g', '7501001116808', 'Herdez', 16.50, 100),
(159, 'Pasta Barilla Spaghetti No. 5 500g', '7501011116815', 'Barilla', 24.50, 100),
(160, 'Pasta La Moderna Spaghetti 200g', '7501018316822', 'La Moderna', 11.50, 200),
(161, 'Pasta La Moderna Codo No. 2 200g', '7501018316839', 'La Moderna', 11.50, 200),
(162, 'Arroz SOS Impecable 1kg', '7501011116846', 'SOS', 35.00, 120),
(163, 'Aceite Nutrioli 800ml', '7501052416853', 'Nutrioli', 45.00, 150),
(164, 'Aceite 1-2-3 1L', '7501006516860', '1-2-3', 42.00, 150),
(165, 'Sopa Knorr Pollo con Fideos 95g', '7501005116877', 'Knorr', 16.50, 100),
(166, 'Consomé Knorr Suiza 8 Cubos', '7501005116884', 'Knorr', 15.50, 200),
(167, 'Catsup Del Monte 320g', '7501052416891', 'Del Monte', 19.50, 100),
(168, 'Sal Sol de Mar 1kg', '7501052416907', 'Sal Sol', 18.00, 120),
(169, 'Azúcar Zulka Estándar 1kg', '7501052416914', 'Zulka', 32.00, 100),
(170, 'Café Nescafé Clásico 42g', '7501058616921', 'Nescafé', 35.00, 100),
(171, 'Café Nescafé Clásico 120g', '7501058616938', 'Nescafé', 85.00, 80),
(172, 'Café Legal con Sombrerete 180g', '7501005116945', 'Legal', 42.00, 60),
(173, 'Té McCormick Manzanilla 25 Sobres', '7501003316952', 'McCormick', 26.50, 80),
(174, 'Chocolate Abuelita 90g Hexagonal', '7501058616969', 'Nestlé', 22.00, 100),
(175, 'Corn Flakes Kellogg''s 360g', '7501008016976', 'Kellogg''s', 55.00, 60),
(176, 'Zucaritas Kellogg''s 300g', '7501008016983', 'Kellogg''s', 62.00, 80),
(177, 'Choco Krispis Kellogg''s 290g', '7501008016990', 'Kellogg''s', 62.00, 80),
(178, 'Mermelada McCormick Fresa 270g', '7501003317003', 'McCormick', 38.00, 60),
(179, 'Galletas Marias Gamesa 170g', '7501000617010', 'Gamesa', 18.50, 150),
(180, 'Galletas Saladitas Gamesa 186g', '7501000617027', 'Gamesa', 22.00, 120),
(181, 'Galletas Emperador Chocolate 101g', '7501000617034', 'Gamesa', 18.50, 100),
(182, 'Galletas Chokis Original 63g', '7501000617041', 'Gamesa', 17.50, 100),
(183, 'Galletas Oreo Original 114g', '7501011117058', 'Nabisco', 21.00, 100),
(184, 'Galletas Ritz Original 89g', '7501011117065', 'Nabisco', 16.50, 80),
(185, 'Jabón Zote Blanco 400g', '7501026017072', 'La Corona', 22.00, 200),
(186, 'Jabón Zote Rosa 400g', '7501026017089', 'La Corona', 22.00, 150),
(187, 'Detergente Roma 500g Polvo', '7501026017096', 'La Corona', 19.50, 150),
(188, 'Detergente Ariel Power Liquid 1L', '7506339317102', 'P&G', 65.00, 80),
(189, 'Suavizante Downy Libre Enjuague 800ml', '7506339317119', 'P&G', 38.00, 100),
(190, 'Limpiador Fabuloso Lavanda 1L', '7501035217126', 'Colgate-Palmolive', 28.50, 120),
(191, 'Cloro Cloralex El Rendidor 950ml', '7501022117133', 'Alen', 18.50, 150),
(192, 'Lavatrastes Salvo Limón 750ml', '7506339317140', 'P&G', 42.00, 100),
(193, 'Papel Higiénico Regio Aires de Frescura 4 rollos', '7501036617157', 'Regio', 32.00, 200),
(194, 'Papel Higiénico Cottonelle Elegance 4 rollos', '7501052117164', 'Kimberly-Clark', 38.50, 150),
(195, 'Servilletas Pétalo 100 pzas', '7501052117171', 'Kimberly-Clark', 22.00, 120),
(196, 'Toalla Femenina Saba Confort 10 pzas', '7501036617188', 'Saba', 28.00, 100),
(197, 'Pasta Dental Colgate Total 12 100ml', '7501035217195', 'Colgate', 45.00, 120),
(198, 'Cepillo Dental Colgate Triple Acción 1 pza', '7501035217201', 'Colgate', 22.50, 80),
(199, 'Shampoo Caprice Especialidades 750ml', '7501035217218', 'Caprice', 42.00, 80),
(200, 'Shampoo Head & Shoulders Limpieza Renovadora 375ml', '7506339317225', 'P&G', 75.00, 60);

-- BLOQUE 5: HIGIENE, FARMACIA Y OTROS (ID 201-250)
INSERT INTO dbo.Producto (ProductoID, Nombre, SKU, Marca, Precio, Stock) VALUES
(201, 'Jabón Palmolive Naturals Aloe 120g', '7501035217232', 'Palmolive', 18.50, 150),
(202, 'Jabón Escudo Antibacterial Blanco 150g', '7501035217249', 'Escudo', 24.00, 120),
(203, 'Jabón Dove Original 100g', '7501035217256', 'Dove', 32.00, 100),
(204, 'Desodorante Axe Black Aerosol 150ml', '7501035217263', 'Axe', 58.00, 80),
(205, 'Desodorante Lady Speed Stick 45g', '7501035217270', 'Speed Stick', 45.00, 80),
(206, 'Crema Nivea Tarro 100ml', '4005808801411', 'Nivea', 55.00, 60),
(207, 'Rastrillo Gillette Prestobarba3 1 pza', '7506339317287', 'Gillette', 38.00, 100),
(208, 'Espuma de Afeitar Gillette 200ml', '7506339317294', 'Gillette', 85.00, 40),
(209, 'Vaselina Pura 40g', '7501035217300', 'Vaseline', 26.00, 50),
(210, 'Toallitas Húmedas Huggies Cuidado Completo 80 pzas', '7501052117317', 'Huggies', 55.00, 80),
(211, 'Pañal Huggies UltraConfort Etapa 4 10 pzas', '7501052117324', 'Huggies', 68.00, 100),
(212, 'Curitas Tela Elástica 10 pzas', '7501052117331', 'Curitas', 24.50, 150),
(213, 'Algodón absorbente 50g', '7501052117348', 'Protec', 18.00, 100),
(214, 'Alcohol Etílico Desnaturalizado 250ml', '7501052117355', 'Protec', 22.00, 120),
(215, 'Gel Antibacterial 250ml', '7501052117362', 'Blumen', 32.00, 100),
(216, 'Aspirina 500mg 10 tabletas', '7501008417379', 'Bayer', 28.50, 150),
(217, 'Alka-Seltzer 12 tabletas efervescentes', '7501008417386', 'Bayer', 42.00, 120),
(218, 'Tabcin Noche 12 cápsulas', '7501008417393', 'Bayer', 65.00, 80),
(219, 'Sal de Uvas Picot 10 sobres', '7501035217409', 'Picot', 48.00, 100),
(220, 'Vick VapoRub 50g', '7501001117416', 'Vick', 78.00, 60),
(221, 'Lucecita Batería Duracell AA 4 pzas', '7501055317423', 'Duracell', 145.00, 40),
(222, 'Batería Duracell AAA 4 pzas', '7501055317430', 'Duracell', 145.00, 40),
(223, 'Encendedor Bic Maxi 1 pza', '7501055317447', 'Bic', 22.00, 100),
(224, 'Focos LED 60W Philips 1 pza', '7501055317454', 'Philips', 48.00, 60),
(225, 'Vela blanca 1 pza', '7501055317461', 'Genérico', 12.00, 200),
(226, 'Cigarrillos Marlboro Red 20s', '7501001117478', 'Marlboro', 82.00, 50),
(227, 'Cigarrillos Marlboro Gold 20s', '7501001117485', 'Marlboro', 82.00, 50),
(228, 'Chicles Canel''s Menta 4 pzas', '7501001117492', 'Canel''s', 2.50, 500),
(229, 'Chicles Trident Menta 12s', '7501011117508', 'Trident', 18.00, 200),
(230, 'Mazapán De la Rosa 28g', '7501011117515', 'De la Rosa', 6.50, 300),
(231, 'Chocolate Carlos V 18g', '7501058617522', 'Nestlé', 10.50, 200),
(232, 'Paleta Payaso 45g', '7501011117539', 'Ricolino', 19.50, 100),
(233, 'Bubulubu 35g', '7501011117546', 'Ricolino', 14.50, 150),
(234, 'Kranky 40g', '7501011117553', 'Ricolino', 15.00, 120),
(235, 'Lucas Muecas Chamoy 24g', '7501011117560', 'Lucas', 16.50, 150),
(236, 'Pelón Pelo Rico 30g', '7501011117577', 'Pelón', 12.50, 200),
(237, 'Pulparindo de la Rosa 1 pza', '7501011117584', 'De la Rosa', 4.50, 400),
(238, 'Huevo Blanco 1 pza', '7501011117591', 'Genérico', 3.50, 600),
(239, 'Sopa Maruchan Pollo 64g', '041789001221', 'Maruchan', 18.50, 200),
(240, 'Sopa Maruchan Camarón Limón 64g', '041789001238', 'Maruchan', 18.50, 200),
(241, 'Alimento para Perro Ganador 1kg', '7501055317607', 'Ganador', 45.00, 80),
(242, 'Alimento para Gato Whiskas Carne 1kg', '7501055317614', 'Whiskas', 85.00, 60),
(243, 'Cerveza Corona Extra 355ml Botella', '7501064117621', 'Corona', 22.00, 200),
(244, 'Cerveza Victoria 355ml Lata', '7501064117638', 'Victoria', 18.50, 240),
(245, 'Cerveza Modelo Especial 355ml Lata', '7501064117645', 'Modelo', 21.00, 120),
(246, 'Clamato Original 473ml', '7501011117652', 'Clamato', 28.50, 100),
(247, 'Tequila José Cuervo Especial 200ml', '7501103317669', 'José Cuervo', 95.00, 40),
(248, 'Mezcal 400 Conejos 200ml', '7501103317676', '400 Conejos', 145.00, 30),
(249, 'Hielo Bolsa 5kg', '7501103317683', 'Genérico', 35.00, 40),
(250, 'Carbón Vegetal 3kg Bolsa', '7501103317690', 'Genérico', 65.00, 30);

    -- Finalización de la carga
    SET IDENTITY_INSERT dbo.Producto OFF;
    
    -- Reseeding para asegurar que el siguiente ID sea 251
    DBCC CHECKIDENT ('dbo.Producto', RESEED, 250);

    COMMIT TRANSACTION;
    PRINT 'Carga finalizada exitosamente: 250 productos insertados.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    PRINT 'ERROR: La carga falló. Se realizó ROLLBACK.';
    THROW;
END CATCH
GO

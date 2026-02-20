<role>
Actúa como un Arquitecto Senior de Bases de Datos y experto en Ingeniería de Prompts. Tu objetivo es generar un script de datos semilla (seeding) para SQL Server 2022 Express con precisión técnica absoluta y fidelidad al mercado minorista mexicano de 2025.
</role>

<context>
- Motor: Microsoft SQL Server 2022 Express.
- Destino del archivo: "/db/scripts/02_seed_data_utm_market.sql".
- Tabla Objetivo: [dbo].[Producto]
- Esquema de Referencia:
  CREATE TABLE [structure]
</context>

<task>
Genera un script SQL que registre exactamente 250 productos de alta rotación en las "tienditas" de México para 2025.
</task>

<market_data_requirements>
1. Fuentes: Basa los datos en "Brand Footprint 2025" (Kantar) y reportes de la ANAM 2025.
2. Mix de Inventario: Refrescos (25%), Botanas (20%), Lácteos (15%), Panadería (15%), Abarrotes (15%), Higiene (10%).
3. Precisión de Precios: Usa precios reales de 2025 ajustados por IEPS e inflación.
4. Estándar SKU: Genera códigos únicos de 13 dígitos que no se repitan en el script cumpliendo el estándar EAN-13 (prefijo 750).
</market_data_requirements>

<sql_best_practices>
1. Idempotencia y Limpieza: El script debe iniciar con una fase de limpieza. Usa "TRUNCATE TABLE dbo.Producto" (o DELETE si hay FKs) para asegurar que la tabla esté vacía y el IDENTITY reiniciado antes de la carga.
2. Gestión de Identidad: Activa SET IDENTITY_INSERT dbo.Producto ON antes de las inserciones y OFF al finalizar.
3. Reseeding: Incluye DBCC CHECKIDENT ('dbo.Producto', RESEED, 250) al final para sincronizar el contador.
4. Integridad Transaccional: Envuelve todo el proceso en un bloque BEGIN TRANSACTION / COMMIT con manejo de errores mediante TRY...CATCH.
</sql_best_practices>

<cli_safety_protocol>
Debido al volumen de 250 productos (evitar truncamiento en buffers CLI):
- No generes un solo bloque masivo de texto.
- Usa la herramienta `write_file` solo para el encabezado y comandos de limpieza.
- Utiliza comandos de shell (`run_shell_command`) con "cat >>" o la herramienta `append` para escribir los productos en bloques de 50 registros.
- Al finalizar, ejecuta `grep -c "INSERT" /db/scripts/02_seed_data_utm_market.sql` para validar el conteo de registros.
</cli_safety_protocol>
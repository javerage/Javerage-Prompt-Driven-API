<role>
Actúa como un Senior Software Architect y experto en persistencia de datos con.NET 10 y C# 14. Tu objetivo es generar las clases de entidad de base de datos para el proyecto "[project_name]", asegurando compatibilidad total con Native AOT (Ahead-of-Time).
</role>

<context>
- Proyecto: [project_name] (Aplicación de consola).
- Runtime:.NET 10 (Optimizado para Trimming y bajo consumo de memoria).
- Lenguaje: C# 14 (Uso mandatorio de la palabra clave 'field' y Primary Constructors).
- Mapeo: Manual vía SqlDataReader (Sin reflexión para garantizar compatibilidad AOT).
- Ubicación: "src/Infrastructure/Models/Data/".
</context>

<task>
Genera tres clases parciales (ProductoEntity, DetalleVentaEntity, VentaEntity) que repliquen con exactitud el esquema DDL de SQL Server 2022 proporcionado.
</task>

<sql_schema_reference>
1. Tabla dbo.Producto:
   - Columnas: ProductoID (INT PK), Nombre (NVARCHAR 100), SKU (VARCHAR 20 UNIQUE), Marca (NVARCHAR 50), Precio (DECIMAL 19,4), Stock (INT).
</sql_schema_reference>

<coding_standards_aot>
- Clases Parciales: Define todas las entidades como 'public partial class' para permitir futuras extensiones estáticas de mapeo.
- C# 14 field keyword: Implementa la lógica de las restricciones CHECK de SQL en los setters usando 'field' para validación inmediata.
- Primary Constructors: Úsalos para definir los campos obligatorios (ID, SKU, Folio).
- Native AOT Readiness: Asegura que las clases sean POCOs simples, permitiendo que el compilador realice un Trimming agresivo.
</coding_standards_aot>

<cli_safety_protocol>
- Crea el directorio "src/Infrastructure/Models/Data/" si no existe.
- Genera un archivo separado por cada entidad.
- Verifica que DECIMAL(19,4) se mapee a 'decimal' en C#.
</cli_safety_protocol>

<output_format>
Devuelve un documento Markdown que incluya:
1. Código fuente completo para las tres entidades.
2. Nota técnica sobre por qué el mapeo manual es superior en rendimiento frente a ORMs dinámicos en .NET 10.
</output_format>
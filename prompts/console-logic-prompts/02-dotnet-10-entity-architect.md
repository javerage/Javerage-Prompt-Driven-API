<role>
Actúa como un Senior Software Architect experto en Domain-Driven Design (DDD),.NET 10 y C# 14. Tu objetivo es diseñar la capa de Entidades de Dominio Puras para el proyecto "[project_name]", asegurando que la lógica de negocio esté encapsulada y sea compatible con Native AOT.
</role>

<context>
- Proyecto: [project_name] (Aplicación de consola).
- Arquitectura: Clean Architecture (Capa Core/Entities).
- Restricción Técnica: Clases puras de dominio sin atributos de persistencia ni dependencias de ORM. El mapeo a base de datos se realizará en una capa distinta para Dapper.
- Ubicación: "src/Core/Entities/".
</context>

<task>
Genera tres archivos de clase (Product.cs, SaleDetail.cs, Sale.cs) que representen el núcleo de negocio, aplicando principios SOLID y las optimizaciones sintácticas de C# 14.
</task>

<domain_model_requirements>
1. Entidad "Product":
   - Propiedades: ProductID (PK), Name, SKU, Brand, Price, Stock.
   - Validaciones: El 'Price' y 'Stock' no pueden ser negativos (usar lógica en el setter).

2. Entidad "SaleDetail":
   - Asociación: Referencia obligatoria a un 'Product'.
   - Captura de Precio: 'UnitPrice' debe inicializarse desde 'Product.Price' al crearse, pero debe ser independiente para preservar el precio histórico de la venta. [1]
   - Lógica: Propiedad calculada 'TotalDetail' (UnitPrice * Quantity).

3. Entidad "Sale":
   - Propiedades: SaleID (PK), Folio (string), SaleDate (DateTime), Status (Enum).
   - Comportamiento: 'SaleDate' debe inicializarse automáticamente con 'DateTime.Now'.
   - Colección: Debe contener una lista de 'SaleDetail'.
   - Agregación: Propiedades calculadas 'TotalItems' y 'TotalSale' que sumen los detalles dinámicamente.
</domain_model_requirements>

<coding_standards_csharp14>
- Backing Field Synthesis: Utiliza la palabra clave 'field' de C# 14 para validaciones en los accessors (get/set), evitando declarar campos privados manuales.
- Inmutabilidad: Implementa Primary Constructors para los campos requeridos e identificadores únicos.
- Concisión: Prefiere miembros con cuerpo de expresión (=>) para las propiedades calculadas.
- AOT/Trimming: Asegura que las clases sean POCOs (Plain Old CLR Objects) simples, sin reflexión, para garantizar la máxima velocidad de ejecución en.NET 10.[4]
</coding_standards_csharp14>

<cli_safety_protocol>
- Verifica la existencia de la ruta "src/Core/Entities/" antes de escribir.
- Genera archivos individuales por cada entidad.
- Tras la generación, confirma que no se incluyó ninguna referencia a 'System.Data' o 'Dapper', manteniendo la pureza del dominio.
</cli_safety_protocol>

<output_format>
El resultado debe ser un documento Markdown técnico con:
1. Definición del 'Enum' de Estatus de Venta.
2. Código fuente completo y documentado para cada clase.
3. Breve explicación de cómo la palabra clave 'field' ayudó a reducir el boilerplate en las validaciones de negocio.[3]
</output_format>
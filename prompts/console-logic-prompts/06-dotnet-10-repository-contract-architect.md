<role>
Actúa como un Senior Software Architect experto en Patrones de Diseño y .NET 10. Tu objetivo es diseñar el contrato de Repositorio (Interface) para la entidad "Product" en el proyecto "[project_name]", asegurando una separación total entre el dominio y la persistencia, y garantizando la compatibilidad con Native AOT.
</role>

<context>
- Proyecto: "[project_name]" (Console App).
- Arquitectura: Clean Architecture (Capa Core/Interfaces).
- Tecnologías:.NET 10, C# 14, Dapper.
- Archivos de Referencia:
  1. Dominio: @src/Core/Entities/Product.cs
  2. Persistencia: @src/Infrastructure/Models/Data/ProductoEntity.cs
  3. Mapeador: @src/Infrastructure/Mappers/ProductMapper.cs
</context>

<task>
Crea la interfaz "IProductRepository.cs" en el directorio "src/Core/Repositories/". El contrato debe definir las operaciones necesarias para gestionar productos siguiendo los estándares de C# 14.
</task>

<interface_requirements>
1. Consultas Eficientes: 
   - 'GetAllAsync': Debe retornar 'IAsyncEnumerable&lt;Product&gt;' para permitir el streaming de datos y optimizar el uso de memoria en.NET 10.
   - 'GetByIdAsync': Consulta por ID de base de datos retornando 'Task&lt;Product?&gt;'.
2. Filtrado Moderno: 
   - 'FindAsync': En lugar de Expressions genéricas (incompatibles con AOT), define un método que acepte un objeto de criterios ('ProductFilter') o parámetros opcionales.
3. Persistencia de Dominio:
   - 'AddAsync', 'UpdateAsync': Deben recibir el objeto de dominio 'Product'.
   - 'UpdateStockAsync': Método especializado para actualización parcial (Atomic Update) recibiendo ID y la nueva cantidad.
4. Resiliencia: Todos los métodos DEBEN aceptar un 'CancellationToken' como parámetro final.[3, 5]
</interface_requirements>

<coding_standards_csharp14>
- Abstracción Pura: La interfaz no debe conocer detalles de Dapper ni de las clases 'Entity'. Solo debe trabajar con tipos del Dominio.
- Documentation: Incluye comentarios XML (///) detallados para cada método, especificando posibles excepciones o comportamientos nulos.
- Native AOT: Evita el uso de 'Task&lt;dynamic&gt;' o cualquier tipo que requiera reflexión en tiempo de ejecución.
</coding_standards_csharp14>

<cli_safety_protocol>
- Lee los archivos de dominio y persistencia mencionados en el contexto antes de proponer el contrato para asegurar que los tipos de datos (especialmente Decimal 19,4) coincidan.
- Verifica si el directorio "src/Core/Repositories/" existe; si no, créalo.
- Tras la generación, valida que el contrato sea idempotente y no dependa de librerías de infraestructura.
</cli_safety_protocol>

<output_format>
Devuelve un documento Markdown que incluya:
1. Estructura de namespaces recomendada.
2. Código fuente completo de 'IProductRepository.cs'.
3. Definición de 'ProductFilter' (si fue necesario para la búsqueda).
4. Explicación técnica de por qué 'IAsyncEnumerable' es superior para una aplicación de consola en.NET 10.[2]
</output_format>
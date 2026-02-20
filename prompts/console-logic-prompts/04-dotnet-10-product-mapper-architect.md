<role>
Actúa como un Senior Software Architect experto en Clean Architecture y optimización de rendimiento en.NET 10. Tu especialidad es diseñar puentes de datos (Mappers) ultra-eficientes y compatibles con Native AOT utilizando las últimas capacidades de C# 14.
</role>

<context>
- Proyecto: "[project_name]" (Console App).
- Arquitectura: Clean Architecture.
- Objetivo: Crear la lógica de transformación entre la entidad de persistencia (ProductoEntity) y el objeto de negocio (Product).
- Stack Técnico:.NET 10, C# 14, Dapper (Mapeo sin reflexión).
- Ubicación de Archivos de Origen:
  1. Dominio: "src/Core/Entities/Product.cs"
  2. Infraestructura: "src/Infrastructure/Models/Data/ProductoEntity.cs"
</context>

<task>
Diseña e implementa la clase "ProductMapper.cs" en el directorio "src/Infrastructure/Mappers/". Esta clase debe facilitar la conversión bidireccional entre el Dominio y la Persistencia.
</task>

<technical_requirements>
1. Ergonomía de Código: Implementa el mapeo utilizando bloques de extensión ('extension blocks') de C# 14. 
2. Firma de Métodos:
   - 'ToDomain()': Transforma 'ProductoEntity' -> 'Product'.
   - 'ToEntity()': Transforma 'Product' -> 'ProductoEntity'.
3. Compatibilidad AOT: El código generado DEBE ser 100% estático y evitar cualquier uso de reflexión o tipos dinámicos para garantizar que el 'Trimming' de.NET 10 sea efectivo.
4. Manejo de Nulos: Utiliza el operador de asignación nula condicional (?.=) y defensas de nulidad modernas de C# 14.
</technical_requirements>

<implementation_logic>
- Analiza las propiedades de ambas clases. Asegura que los tipos numéricos (decimal 19,4) mantengan su fidelidad durante la transferencia.
- Utiliza 'Primary Constructors' si decides implementar el mapeador como un servicio inyectable, aunque se prefiere el enfoque de métodos de extensión estáticos por su bajo overhead.
- Si existen discrepancias entre los nombres de las propiedades (ej. ProductoID vs ProductID), aplica la lógica de traducción correspondiente.
</implementation_logic>

<cli_safety_protocol>
- Lee el contenido de "@src/Core/Entities/Product.cs" y "@src/Infrastructure/Models/Data/ProductoEntity.cs" antes de generar el mapeador.
- Verifica que el directorio "src/Infrastructure/Mappers/" exista; si no, créalo.
- Tras la creación, notifica al equipo sobre cualquier inconsistencia de tipos o propiedades faltantes entre ambas capas.
</cli_safety_protocol>

<output_format>
Devuelve un documento Markdown que incluya:
1. Árbol de directorios actualizado.
2. Código fuente completo de 'ProductMapper.cs' con comentarios XML técnicos.
3. Ejemplo de uso dentro de un Repositorio (Snippet corto).
4. Justificación de por qué el uso de 'extension blocks' de C# 14 mejora el rendimiento y la legibilidad en "[project_name]". 
</output_format>
<role>
Actúa como un Senior Software Architect experto en Clean Architecture y optimización de alto rendimiento en.NET 10. Tu especialidad es diseñar puentes de datos (Mappers) para sistemas transaccionales, utilizando las últimas capacidades de C# 14 y garantizando compatibilidad total con Native AOT.
</role>

<context>
- Proyecto: "[project_name]" (Console App).
- Objetivo: Implementar el mapeo bidireccional entre la capa de Persistencia (VentaEntity/DetalleVentaEntity) y la capa de Dominio (Sale/SaleDetail).
- Stack:.NET 10, C# 14, Dapper.
- Ubicación de Archivos de Origen:
  1. Dominio: "src/Core/Entities/Sale.cs" (depende de SaleDetail.cs).
  2. Infraestructura: "src/Infrastructure/Models/Data/VentaEntity.cs" (depende de DetalleVentaEntity.cs).
</context>

<task>
Diseña e implementa la clase "SaleMapper.cs" en el directorio "src/Infrastructure/Mappers/". Esta clase debe orquestar la transformación profunda de la venta y su colección de detalles.
</task>

<technical_requirements>
1. Sintaxis Moderna: Implementa el mapeo utilizando bloques de extensión ('extension blocks') de C# 14 dentro de una clase estática. 
2. Mapeo Profundo (Deep Mapping):
   - 'ToDomain()': Transforma 'VentaEntity' -> 'Sale', incluyendo la conversión de la lista de 'DetalleVentaEntity' a 'SaleDetail'.
   - 'ToEntity()': Transforma 'Sale' -> 'VentaEntity', incluyendo sus detalles correspondientes.
3. Eficiencia AOT: El código DEBE ser 100% estático y evitar reflexión. Asegura que las colecciones se transformen de manera eficiente (preferiblemente usando Spans o métodos de extensión optimizados de.NET 10). 
4. Integridad de Datos: Asegura que el 'Folio', 'Estatus' y los cálculos de totales se preserven correctamente entre capas.
</technical_requirements>

<implementation_logic>
- Analiza las dependencias jerárquicas: Para mapear una Venta, primero debes saber cómo mapear sus Detalles.
- Usa constructores primarios (Primary Constructors) si el mapeador requiere inyección de lógica de redondeo o configuración, aunque se prefiere el enfoque de extensión estática. [5]
- Manejo de Nulidad: Implementa el operador de asignación nula condicional (?.=) para las colecciones de detalles si son opcionales en la entidad. [5, 6]
</implementation_logic>

<cli_safety_protocol>
- Lee el contenido de todos los archivos de entidad involucrados (@src/Core/Entities/ y @src/Infrastructure/Models/Data/) antes de proponer la implementación.
- Verifica si el directorio "src/Infrastructure/Mappers/" existe; si no, créalo.
- Tras generar el código, valida que no existan dependencias circulares entre el dominio y la infraestructura.
</cli_safety_protocol>

<output_format>
Devuelve un documento Markdown que contenga:
1. Árbol de directorios de la capa de infraestructura.
2. Código fuente completo de 'SaleMapper.cs' con comentarios técnicos detallados.
3. Ejemplo de uso: Cómo un repositorio recuperaría una 'VentaEntity' de Dapper y la devolvería como un objeto 'Sale'.
4. Nota de arquitectura sobre los beneficios de usar C# 14 Extension Members en este escenario. [1, 7]
</output_format>
<role>
Actúa como un Principal Software Architect experto en.NET 10, C# 14 y diseño de sistemas de alto rendimiento. Tu especialidad es la implementación de patrones de persistencia en Arquitecturas Limpias, optimizados para Native AOT y Dapper.
</role>

<context>
- Proyecto: "[project_name]" (Console Application).
- Objetivo: Diseñar el contrato de repositorio (Interface) para la raíz del agregado "Sale".
- Stack Técnico:.NET 10.0 (LTS), C# 14, Native AOT.
- Mapeo: Se dispone de 'SaleMapper' para la conversión entre 'VentaEntity' (Persistencia) y 'Sale' (Dominio).
- Archivos de referencia: @src/Core/Entities/SaleDetail.cs, @src/Core/Entities/Sale.cs y @src/Infrastructure/Models/Data/VentaEntity.cs, @src/Infrastructure/Models/Data/DetalleVentaEntity.cs.
</context>

<task>
Crea la interfaz "ISaleRepository.cs" en el directorio "src/Core/Repositories/". El contrato debe ser riguroso, estar en inglés y seguir los estándares de C# 14.
</task>

<interface_specifications>
1. Consulta de Flujo (Streaming):
   - 'GetAllAsync': Debe retornar 'IAsyncEnumerable&lt;Sale&gt;' para permitir el procesamiento de datos sin cargar toda la lista en memoria, aprovechando las mejoras de AsyncEnumerable en.NET 10.[2, 4]
2. Consulta por Identidad:
   - 'GetByIdAsync': Debe retornar 'Task&lt;Sale?&gt;', manejando explícitamente la nulidad con C# 14 NRT (Nullable Reference Types).
3. Filtrado Robusto (AOT Friendly):
   - 'FindAsync': Define un método que acepte un objeto de criterios ('SaleFilter'). Evita el uso de Expressions genéricas para garantizar la compatibilidad con los interceptores de Dapper.
4. Operaciones de Persistencia:
   - 'AddAsync': Debe recibir el objeto de dominio 'Sale' y retornar el objeto persistido con su identidad generada.
   - 'UpdateAsync': Debe recibir el objeto de dominio 'Sale' para actualizar el agregado completo.
5. Cancelación Cooperativa:
   - TODOS los métodos asíncronos DEBEN incluir un 'CancellationToken cancellationToken = default' como parámetro final.[7, 8]
</interface_specifications>

<coding_standards_csharp14>
- Abstracción: La interfaz debe ser pura; no debe tener dependencias de infraestructura ni de tipos de Dapper.
- C# 14 syntax: Si defines el objeto 'SaleFilter', utiliza Primary Constructors y la palabra clave 'field' para validaciones. 
- IMPORTANTE: NO declares campos privados manuales (ej. _startDate) si usas 'field', ya que el compilador genera el respaldo automáticamente.
- Documentación: Incluye comentarios XML (///) que describan las precondiciones y postcondiciones de cada operación.
</coding_standards_csharp14>

<cli_safety_protocol>
- Lee el contenido de los archivos de dominio y entidad indicados en el contexto antes de generar el código para asegurar la coincidencia de tipos (Decimal 19,4).
- Verifica si el directorio "src/core/repositories/" existe; si no, créalo.
- Notifica al equipo si detectas riesgos de consultas N+1 al recuperar los detalles de la venta ('SaleDetail') dentro de este contrato.
</cli_safety_protocol>

<output_format>
Devuelve un documento Markdown que contenga:
1. El código fuente completo de 'ISaleRepository.cs'.
2. La definición del objeto 'SaleFilter' (preferiblemente como un record inmutable).
3. Una breve sección de "Asesoría Arquitectónica" sobre el impacto de Native AOT en la futura implementación de este contrato.
</output_format>
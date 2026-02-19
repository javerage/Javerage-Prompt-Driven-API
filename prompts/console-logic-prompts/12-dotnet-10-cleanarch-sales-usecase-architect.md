<role>
  Actúa como un Arquitecto de Software Senior especializado en Arquitectura Limpia (Clean Architecture) y Diseño Orientado al Dominio (DDD). Eres experto en el ecosistema .NET 10 LTS y en el desarrollo idiomático con C# 14. Tu objetivo es generar contratos de casos de uso para ventas que sean de alto rendimiento, mantenibles y estrictamente conformes con los principios SOLID.
</role>

<project_context>
  - Tipo de Sistema: Aplicación de Consola.NET 10.0+.
  - Arquitectura: Arquitectura Limpia (Separación Core/Infrastructure).
  - Política de Idioma: Inglés para todo el código, comentarios, espacios de nombres e identificadores.
  - Estructura de Repositorio Existente:
    - Capa de Dominio (Lógica Pura): src/Core/Domain/
    - Capa de Aplicación (Casos de Uso): src/Core/UseCases/
    - Capa de Infraestructura (Persistencia): src/Infrastructure/Repositories/
  - Directorio Destino: src/Core/UseCases/
</project_context>

<technology_constraints>
  - Lenguaje: C# 14.
  - Características Prioritarias:
    - Palabra clave 'field' para la validación de estado de propiedades en objetos de dominio.
    - Bloques de extensión (extension blocks) para ayudantes semánticos a nivel de interfaz.
    - Asignación condicional nula (?. =) para actualizaciones de propiedades.
    - Expresión 'nameof' para tipos genéricos abiertos (ej. List<>).
    - Expresiones de colección () para tipos de retorno basados en listas.
</technology_constraints>

<coding_standards>
  - Adherencia estricta al Principio de Responsabilidad Única (SRP): Una interfaz por caso de uso.
  - Convención de Nomenclatura: I[NombreAccion]UseCase.cs.
  - Los métodos de los Casos de Uso deben ser asíncronos (Task o ValueTask).
  - Los contratos deben interactuar ÚNICAMENTE con objetos de Dominio (Sale, SaleFilter), nunca con entidades de persistencia (VentaEntity).
  - Asegurar la segregación de interfaces para evitar "Interfaces Dios" (ISP).
</coding_standards>

<task_objective>
  Generar 5 contratos de interfaz especializados para las siguientes acciones de negocio:
  1. FetchAllSales: Recuperar todos los registros de ventas como una colección asíncrona.
  2. FetchSaleById: Recuperar una venta específica mediante su identificador único.
  3. FetchSalesByFilter: Recuperar ventas basadas en criterios proporcionados en un objeto de dominio 'SaleFilter'.
  4. CreateSale: Orquestar la lógica para crear y persistir una nueva venta.
  5. UpdateSaleStatus: Actualizar ÚNICAMENTE la propiedad 'Estatus' de una venta existente.
</task_objective>

<execution_workflow>
  1. EXPLORAR: Usa tus herramientas (ls/cat) para leer los siguientes archivos:
     - src/Core/Entities/Sale.cs, SaleDetail.cs, SaleFilter.cs
     - src/Infrastructure/Models/Data/VentaEntity.cs, DetalleVentaEntity.cs
     - src/Infrastructure/Models/Data/SaleMapper.cs
     - src/Core/Repositories/ISaleRepository.cs
  2. ANALIZAR: Identifica cualquier "fuga de entidades" (Entity Leaks) donde detalles de infraestructura aparezcan en objetos de dominio.
  3. PLANIFICAR: Usa el pensamiento extendido (extended thinking) para diseñar las firmas de las interfaces, asegurando que los tipos de retorno sean objetos de Dominio y los métodos sean async.
  4. IMPLEMENTAR: Crea los archivos.cs en 'src/Core/UseCases/' con las declaraciones de espacio de nombres correctas.
  5. VALIDAR: Si es posible, ejecuta 'dotnet build' o 'dotnet check' para asegurar la corrección sintáctica.
  6. FEEDBACK: Resume los cambios y proporciona 3 recomendaciones arquitectónicas basadas en el análisis del código existente.
</execution_workflow>

<output_format>
  - Lista de rutas de archivos generados.
  - Código fuente completo para cada interfaz en C# 14.
  - Una sección de "Deuda Técnica y Mejoras" que cubra SOLID y mejoras de C# 14.
</output_format>

<quality_gate>
  - ¿Cada interfaz tiene una sola responsabilidad?
  - ¿Se evita el uso de entidades (Entities) en la capa de Casos de Uso?
  - ¿El código aprovecha las nuevas capacidades de C# 14 (field, collection expressions)?
  - ¿Se respeta la convención de nombres I[Action]UseCase.cs en inglés?
</quality_gate>
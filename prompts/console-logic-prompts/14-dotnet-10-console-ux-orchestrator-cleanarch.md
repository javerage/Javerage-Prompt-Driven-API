<role>
  Actúa como un Desarrollador Lead de.NET especializado en UX de Consola y Arquitectura Limpia. Eres experto en crear interfaces CLI intuitivas, robustas y altamente desacopladas, utilizando las últimas características de C# 14 y el SDK de.NET 10. Tu prioridad es la validación de entrada de usuario y la inyección de dependencias limpia.
</role>

<context>
  Estamos finalizando una aplicación de consola en.NET 10 basada en Clean Architecture. 
  Ya existen las capas de Core (UseCases) y Application (Implementaciones).
  
  Estado del Proyecto:
  - El sistema usa Inyección de Dependencias (Microsoft.Extensions.DependencyInjection).
  - Los casos de uso de Productos (Consultar todos, Consultar por ID, Registrar) ya están implementados.
  - El punto de entrada es Program.cs (utilizando Top-level statements).
</context>

<task_description>
  Debes modificar 'Program.cs' para implementar un menú interactivo y profesional que permita al usuario ejecutar las acciones de gestión de productos. El sistema debe ser resiliente a errores de entrada y seguir el principio de Inversión de Dependencias.
</task_description>

<technical_requirements>
  <rule_set name="Interfaz y Menú">
    - Implementar un menú infinito (while loop) con las opciones: 
      1. Consultar todos los productos.
      2. Consultar producto por ID (Validar: solo enteros).
      3. Registrar nuevo producto.
      4. Salir.
    - El registro de productos debe solicitar cada propiedad de forma individual (Nombre, Precio, Stock, etc.) con validación inmediata.
  </rule_set>
  
  <rule_set name="C# 14 y Limpieza">
    - Utilizar 'Raw String Literals' (triple comillas) para el arte ASCII o el diseño del menú.
    - Utilizar 'UTF-8 String Literals' (u8) si el rendimiento en consola es crítico para constantes.
    - Inyectar los Casos de Uso (Interfaces) a través de un 'IServiceScope' manual. No resolver 'Scoped Services' desde el root provider.
    - Si la lógica de registro es extensa, crear un método local o una clase de soporte en un archivo separado para mantener Program.cs legible.
  </rule_set>

  <rule_set name="Configuración de Ejecución y Secretos">
    - Configurar el archivo 'Properties/launchSettings.json' para establecer 'DOTNET_ENVIRONMENT' como 'Development'.
    - Asegurar que los 'User Secrets' se carguen explícitamente en el 'HostApplicationBuilder' si el entorno es Desarrollo.
    - Validar que la cadena de conexión real no esté 'harcodeada' en appsettings.json.
  </rule_set>

  <rule_set name="Verificación de Entorno">
    - Confirmar la existencia de la carpeta 'Properties/' y el archivo 'launchSettings.json'. Si no existen, créalos con el perfil de ejecución adecuado.
  </rule_set>
</technical_requirements>

<execution_workflow>
  1. EXPLORAR: Usa 'ls' y 'cat' para verificar la ruta 'src/Application/' y '/src/Core/UseCases/' para obtener los nombres exactos de las interfaces e implementaciones.
  2. CONFIGURAR: Crear el directorio 'Properties' y el archivo 'launchSettings.json' configurando el entorno de Desarrollo para habilitar la carga de secretos.
  3. ANALIZAR: Verificar si Program.cs ya tiene el ServiceCollection configurado. Asegurar que los servicios 'Scoped' se resuelvan dentro de un bloque 'using (var scope = host.Services.CreateScope())'.
  4. PLANIFICAR: Diseñar el flujo de captura de datos para el nuevo producto (mapeo de consola a objeto de dominio).
  5. IMPLEMENTAR: Modificar Program.cs integrando el menú, la gestión de scopes y las llamadas asíncronas a los Use Cases.
  6. VALIDAR: Ejecutar 'dotnet build' para asegurar que las referencias a los Casos de Uso son correctas y 'dotnet run' para verificar la carga de secretos.
</execution_workflow>

<output_format>
  - Código completo y actualizado de 'Program.cs'.
  - Contenido del archivo 'Properties/launchSettings.json'.
  - Código de cualquier archivo de soporte creado para la captura de datos o registro de servicios.
  - Sección <environment_report> confirmando el estado de la configuración de entorno y la carga de secretos.
  - Breve guía de uso del menú generado.
</output_format>

<quality_gate>
  - ¿Se ha creado el archivo launchSettings.json con el entorno 'Development'?
  - ¿La aplicación resuelve los servicios Scoped mediante un IServiceScope manual?
  - ¿La opción de "Consultar por ID" falla elegantemente si el usuario ingresa texto en lugar de números?
  - ¿Se están usando las interfaces de los Casos de Uso en lugar de las implementaciones concretas?
  - ¿El código es asíncrono (await) en las llamadas a la lógica de negocio?
  - ¿Se utiliza la sintaxis moderna de C# 14?
</quality_gate>
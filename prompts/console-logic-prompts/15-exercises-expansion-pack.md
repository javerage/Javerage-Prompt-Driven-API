# UTM Market - Guía de Ejercicios de Expansión (Arquitectura Limpia)

Esta guía contiene tres desafíos técnicos diseñados para expandir la funcionalidad del sistema UTM Market. El objetivo es que el estudiante diseñe sus propios prompts estructurados para resolver cada requerimiento utilizando .NET 10 y C# 14.

---

## Ejercicio 1: Gestión de Clientes (Customer Management)

**Contexto**: El negocio requiere comenzar a identificar quiénes realizan las compras para futuras estrategias de lealtad.

### Requerimientos Técnicos:
*   **Modelo de Dominio**: Crear la clase `Customer.cs` con las propiedades: `CustomerId`, `FullName`, `Email` e `IsActive`.
    *   *Regla de Negocio*: El `Email` debe validarse mediante lógica en el setter utilizando la palabra clave `field` de C# 14.
*   **Contrato de Persistencia**: Definir la interfaz `ICustomerRepository.cs` en la capa Core con soporte para:
    *   Búsqueda por email: `GetByEmailAsync(string email)`.
    *   Registro de nuevo cliente: `AddAsync(Customer customer)`.
*   **Implementación de Infraestructura**: Crear el repositorio concreto utilizando **mapeo manual** con `SqlDataReader` para asegurar compatibilidad total con Native AOT.
*   **Estándares de Codificación**: Es obligatorio el uso de **Primary Constructors** para la inyección de dependencias.

---

## Ejercicio 2: Sistema de Alertas de Inventario Crítico

**Contexto**: Para evitar la pérdida de ventas, el sistema debe ser capaz de identificar productos que estén cerca de agotarse.

### Requerimientos Técnicos:
*   **Abstracción**: Crear la interfaz `ILowStockAlertUseCase.cs` en la capa de Casos de Uso.
*   **Lógica de Aplicación**: Implementar el caso de uso `LowStockAlertUseCaseImpl.cs`.
    *   *Funcionalidad*: El método debe recibir un valor entero llamado `threshold` (umbral).
    *   *Retorno*: Debe devolver un `IAsyncEnumerable<Product>` que contenga solo aquellos productos cuyo stock sea menor o igual al umbral especificado.
*   **Inyección de Dependencias**: El nuevo servicio debe quedar registrado correctamente en el contenedor de servicios del archivo `Program.cs`.
*   **Optimización**: El flujo de datos debe ser asíncrono y eficiente en memoria.

---

## Ejercicio 3: Consulta de Historial de Ventas por Rango de Fechas

**Contexto**: El gerente de la tienda necesita ver cuánto se ha vendido en periodos específicos (ej. el fin de semana pasado).

### Requerimientos Técnicos:
*   **Interfaz de Usuario (UX)**: Integrar una nueva opción en el menú principal de la consola: "Consultar ventas por fecha".
*   **Captura de Datos**: El sistema debe solicitar al usuario una "Fecha de Inicio" y una "Fecha de Fin".
    *   *Validación*: Se debe asegurar que las entradas tengan un formato de fecha válido (`DateTime.TryParse`).
*   **Orquestación**: La UI debe consumir el caso de uso `IFetchSalesByFilterUseCase` enviando un objeto `SaleFilter` con el rango capturado.
*   **Visualización**: Los resultados deben mostrarse en una tabla formateada en consola incluyendo el Folio, la Fecha y el Monto Total de la venta.
*   **Restricción de Arquitectura**: Como es una aplicación de consola, recuerda que los servicios de aplicación deben resolverse creando un `IServiceScope` manual para cada ciclo de ejecución.

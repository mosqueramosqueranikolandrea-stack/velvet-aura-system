# 🎀 VelvetAura - Sistema de Gestión de Pedidos

![Versión](https://img.shields.io/badge/version-1.0-pink)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Castle Windsor](https://img.shields.io/badge/Castle%20Windsor-AOP-orange)

Sistema de consola en **C# .NET** que integra cuatro paradigmas de programación para una tienda de moda femenina premium.  
Desarrollado como proyecto final para la materia de Programación Avanzada.

---

## 📖 Descripción del sistema

**VelvetAura** permite gestionar:
- Catálogo de productos (maquillaje, perfumes, accesorios, cuidado facial)
- Clientes y sus pedidos
- Inventario dinámico
- Pagos con tarjeta
- Eventos de negocio (Pedido creado, Stock actualizado, Estado cambiado)

El usuario interactúa mediante un **menú de consola** intuitivo y colorido.

---

## 🧱 Paradigmas implementados

### 1. Programación Orientada a Objetos (POO)
- **Clases**: `Producto`, `Cliente`, `Pedido`, `Pago` (abstracta), `PagoTarjeta`, `Inventario`.
- **Relaciones**:
  - Herencia: `PagoTarjeta` hereda de `Pago`.
  - Composición: `Pedido` contiene una lista de `Producto`.
  - Agregación: `Cliente` contiene una lista de `Pedido`.
  - Asociación: `Inventario` usa `Producto` sin poseerlo.
- **Interfaz**: `IPagoService`.
- **Polimorfismo**: `PagoTarjeta.ProcesarPago()` sobreescribe el método abstracto.

### 2. Aspectos (AOP) con Castle DynamicProxy
- Contenedor **Castle Windsor** para inyección de dependencias.
- Servicios resueltos por **interfaz** (`IPagoService`).
- **Dos interceptores**:
  - `LoggingInterceptor`: registra automáticamente la entrada y salida de métodos (concern transversal de logging).
  - `ErrorInterceptor`: captura excepciones centralizadamente (prepara el sistema para manejo amigable de errores).
- Configuración en `WindsorContainerConfig.cs`.

### 3. Programación Funcional
- **LINQ**: uso de `Where`, `Select` y `Aggregate` (o `Sum`) para consultar datos.
  - `Where` → filtrar productos caros (>$50).
  - `Select` → extraer solo los nombres de los productos.
  - `Aggregate` → sumar el stock total del inventario.
- **Función pura**: `ConsultasVelvet.CalcularTotal()` no modifica el estado original.
- **Func<> como parámetro de alto orden**: `ConsultasVelvet.FiltrarProductos()` recibe un `Func<Producto, bool>`.
- **Tipo inmutable (record)**: `ProductoDTO` – una vez creado no se puede modificar.

### 4. Eventos (semánticamente significativos)
- **Eventos personalizados**:
  - `PedidoCreado` – se dispara cuando un cliente genera un nuevo pedido.
  - `EstadoPedidoCambiado` – cuando el pedido pasa de "Creado" a "Confirmado" o "Cancelado".
  - `StockActualizado` – cuando se reduce el inventario por una venta.
- **EventArgs propios**: `PedidoEventArgs` y `StockEventArgs` transportan mensajes descriptivos.
- Los eventos reflejan **cambios reales del dominio** (no eventos técnicos).

---

## 🛠️ Principios SOLID aplicados

| Principio | Ejemplo en el código |
|-----------|----------------------|
| **S**ingle Responsibility | Cada clase tiene una única responsabilidad (Producto solo almacena datos, Pedido gestiona su lógica). |
| **O**pen/Closed | Clase abstracta `Pago` permite agregar nuevos métodos de pago sin modificar el código existente. |
| **L**iskov Substitution | `PagoTarjeta` puede reemplazar a `Pago` sin alterar el comportamiento. |
| **I**nterface Segregation | `IPagoService` tiene un único método `ProcesarPago`. |
| **D**ependency Inversion | Los servicios dependen de interfaces (`IPagoService`) y no de implementaciones concretas (inyectado por Castle Windsor). |

---

## 🖼️ Diagrama de clases UML

![Diagrama UML]

> El diagrama muestra todas las clases, relaciones (herencia, composición, agregación, asociación) y la interfaz `IPagoService`.  
> 

---   
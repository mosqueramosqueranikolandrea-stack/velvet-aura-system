using Castle.Windsor;
using VelvetAura.Aspects;
using VelvetAura.Domain;
using VelvetAura.Functional;
using VelvetAura.Services;

// ============================================================
// 🎀 VELVET AURA - SISTEMA DE GESTIÓN DE PEDIDOS 🎀
// ============================================================
// 
// PARADIGMAS IMPLEMENTADOS:
//
// 1. POO (Programación Orientada a Objetos)
//    - Clases: Producto, Cliente, Pedido, Pago, PagoTarjeta, Inventario
//    - Relaciones: Herencia (PagoTarjeta←Pago), Composición (Pedido→Productos)
//                 Agregación (Cliente→Pedidos), Asociación (Inventario→Producto)
//    - Interfaz: IPagoService
//    - Polimorfismo: PagoTarjeta.ProcesarPago()
//
// 2. ASPECTOS (AOP) con Castle DynamicProxy
//    - Castle Windsor como contenedor DI
//    - LoggingInterceptor: log automático entrada/salida de métodos
//    - Servicios resueltos por interfaz (IPagoService)
//
// 3. PROGRAMACIÓN FUNCIONAL
//    - LINQ: Where (filtros), Select (proyecciones), Aggregate (sumas)
//    - Función pura: ConsultasVelvet.CalcularTotal()
//    - Func<> como parámetro: ConsultasVelvet.FiltrarProductos()
//    - Record inmutable: ProductoDTO
//
// 4. EVENTOS (semánticamente significativos)
//    - PedidoCreado: cuando se crea un nuevo pedido
//    - EstadoPedidoCambiado: cuando cambia el estado del pedido
//    - StockActualizado: cuando se modifica el inventario
//    - EventArgs personalizados: PedidoEventArgs, StockEventArgs
//
// ============================================================

namespace VelvetAura
{
    class Program
    {
        // Datos globales del sistema
        static List<Producto> productos = new List<Producto>();
        static List<Cliente> clientes = new List<Cliente>();
        static Inventario inventario = new Inventario();
        static IWindsorContainer container;
        static IPagoService pagoService;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // Configuración visual cute
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            // Configurar Castle Windsor (Aspectos)
            container = WindsorContainerConfig.Configure();
            pagoService = container.Resolve<IPagoService>();

            // Suscribirse a eventos globales
            inventario.StockActualizado += (s, e) => Console.WriteLine($"   🎀 [EVENTO] {e.Mensaje}");

            // Datos iniciales
            CargarDatosIniciales();

            bool ejecutando = true;
            while (ejecutando)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarCliente();
                        break;
                    case "2":
                        AgregarProductoCatalogo();
                        break;
                    case "3":
                        VerCatalogo();
                        break;
                    case "4":
                        CrearPedido();
                        break;
                    case "5":
                        VerPedidos();
                        break;
                    case "6":
                        GestionarInventario();
                        break;
                    case "7":
                        MostrarEstadisticasLINQ();
                        break;
                    case "8":
                        Console.WriteLine("\n✨ Gracias por usar VelvetAura. ¡Hasta pronto! ✨");
                        ejecutando = false;
                        break;
                    default:
                        Console.WriteLine("❌ Opción no válida. Intenta de nuevo.");
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("   💖 VELVET AURA - MENÚ PRINCIPAL 💖   ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("1. 👤 Registrar nuevo cliente");
            Console.WriteLine("2. 🛍️ Agregar producto al catálogo");
            Console.WriteLine("3. 📋 Ver catálogo de productos");
            Console.WriteLine("4. 🛒 Crear nuevo pedido");
            Console.WriteLine("5. 📦 Ver historial de pedidos");
            Console.WriteLine("6. 📊 Gestionar inventario (reducir stock)");
            Console.WriteLine("7. 📈 Ver estadísticas con LINQ");
            Console.WriteLine("8. 🚪 Salir");
            Console.Write("\n👉 Elige una opción: ");
        }

        static void CargarDatosIniciales()
        {
            // Productos iniciales
            productos.Add(new Producto(1, "✨ Perfume Aura", 120m, 5, "Perfumes"));
            productos.Add(new Producto(2, "💄 Labial Rosa", 25m, 10, "Maquillaje"));
            productos.Add(new Producto(3, "🖌️ Set de Brochas", 45m, 3, "Accesorios"));
            productos.Add(new Producto(4, "🌸 Crema Hidratante", 65m, 8, "Cuidado Facial"));
            productos.Add(new Producto(5, "💅 Esmalte Brillante", 15m, 20, "Uñas"));

            // Cargar inventario
            foreach (var p in productos)
            {
                inventario.AgregarProducto(p);
            }
        }

        static void RegistrarCliente()
        {
            Console.WriteLine("\n📝 REGISTRO DE NUEVO CLIENTE");
            Console.Write("   Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("   Email: ");
            string email = Console.ReadLine();

            int nuevoId = clientes.Count + 1;
            var cliente = new Cliente(nuevoId, nombre, email);
            clientes.Add(cliente);

            Console.WriteLine($"   ✅ Cliente {nombre} registrado con éxito! (ID: {nuevoId})");
        }

        static void AgregarProductoCatalogo()
        {
            Console.WriteLine("\n🛍️ AGREGAR NUEVO PRODUCTO");
            Console.Write("   Nombre del producto: ");
            string nombre = Console.ReadLine();
            Console.Write("   Precio: $");
            decimal precio = decimal.Parse(Console.ReadLine());
            Console.Write("   Stock inicial: ");
            int stock = int.Parse(Console.ReadLine());
            Console.Write("   Categoría (Perfumes/Maquillaje/Accesorios/Cuidado Facial/Uñas): ");
            string categoria = Console.ReadLine();

            int nuevoId = productos.Count + 1;
            var nuevoProducto = new Producto(nuevoId, nombre, precio, stock, categoria);
            productos.Add(nuevoProducto);
            inventario.AgregarProducto(nuevoProducto);

            Console.WriteLine($"   ✅ Producto '{nombre}' agregado al catálogo! (ID: {nuevoId})");
        }

        static void VerCatalogo()
        {
            Console.WriteLine("\n📋 CATÁLOGO DE PRODUCTOS");
            Console.WriteLine("   ID | Nombre | Precio | Stock | Categoría");
            Console.WriteLine("   " + new string('-', 50));

            foreach (var p in productos)
            {
                Console.WriteLine($"   {p.Id,-3} | {p.Nombre,-15} | ${p.Precio,-6} | {p.Stock,-5} | {p.Categoria}");
            }
        }

        static void CrearPedido()
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("\n❌ No hay clientes registrados. Primero registra un cliente (opción 1).");
                return;
            }

            Console.WriteLine("\n🛒 CREAR NUEVO PEDIDO");
            Console.WriteLine("   Clientes disponibles:");
            foreach (var c in clientes)
            {
                Console.WriteLine($"   {c.Id}. {c.Nombre} - {c.Email}");
            }
            Console.Write("   Selecciona el ID del cliente: ");
            int clienteId = int.Parse(Console.ReadLine());
            var cliente = clientes.FirstOrDefault(c => c.Id == clienteId);

            if (cliente == null)
            {
                Console.WriteLine("❌ Cliente no encontrado.");
                return;
            }

            var pedido = new Pedido(cliente);

            // Suscribirse a eventos del pedido
            pedido.PedidoCreado += (s, e) => Console.WriteLine($"   🎀 [EVENTO] {e.Mensaje}");
            pedido.EstadoPedidoCambiado += (s, e) => Console.WriteLine($"   🎀 [EVENTO] {e.Mensaje}");

            bool agregando = true;
            while (agregando)
            {
                VerCatalogo();
                Console.Write("\n   Ingresa el ID del producto a agregar (0 para terminar): ");
                int prodId = int.Parse(Console.ReadLine());

                if (prodId == 0) break;

                var producto = productos.FirstOrDefault(p => p.Id == prodId);
                if (producto == null)
                {
                    Console.WriteLine("   ❌ Producto no encontrado.");
                    continue;
                }

                Console.Write("   Cantidad: ");
                int cantidad = int.Parse(Console.ReadLine());

                if (cantidad > producto.Stock)
                {
                    Console.WriteLine($"   ❌ Stock insuficiente. Solo hay {producto.Stock} unidades.");
                    continue;
                }

                for (int i = 0; i < cantidad; i++)
                {
                    pedido.AgregarProducto(producto);
                }
                Console.WriteLine($"   ✅ {cantidad}x {producto.Nombre} agregado al pedido.");
            }

            Console.WriteLine($"\n   📊 Total del pedido: {pedido.Total:C}");
            Console.Write("   ¿Confirmar pedido? (s/n): ");
            if (Console.ReadLine().ToLower() == "s")
            {
                pedido.CambiarEstado("Confirmado");

                // Procesar pago (con Aspectos)
                Console.WriteLine("\n   💳 PROCESANDO PAGO (con interceptores de Castle)...");
                var pago = new PagoTarjeta { Monto = pedido.Total, NumeroTarjeta = "****1234" };
                pagoService.ProcesarPago(pago);

                // Reducir stock
                foreach (var prod in pedido.Productos)
                {
                    inventario.ReducirStock(prod.Id, 1);
                }

                cliente.Pedidos.Add(pedido);
                Console.WriteLine($"\n   ✨ Pedido #{pedido.Id} completado con éxito!");
            }
            else
            {
                pedido.CambiarEstado("Cancelado");
                Console.WriteLine("   ❌ Pedido cancelado.");
            }
        }

        static void VerPedidos()
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("\n❌ No hay clientes registrados.");
                return;
            }

            Console.WriteLine("\n📦 HISTORIAL DE PEDIDOS");
            foreach (var cliente in clientes)
            {
                if (cliente.Pedidos.Count > 0)
                {
                    Console.WriteLine($"\n   👤 {cliente.Nombre} ({cliente.Email}):");
                    foreach (var pedido in cliente.Pedidos)
                    {
                        Console.WriteLine($"      Pedido #{pedido.Id} - Estado: {pedido.Estado} - Total: {pedido.Total:C} - Fecha: {pedido.FechaCreacion:dd/MM/yyyy}");
                    }
                }
                else
                {
                    Console.WriteLine($"\n   👤 {cliente.Nombre} - No tiene pedidos aún.");
                }
            }
        }

        static void GestionarInventario()
        {
            Console.WriteLine("\n📊 GESTIÓN DE INVENTARIO");
            VerCatalogo();
            Console.Write("\n   Ingresa el ID del producto para reducir stock: ");
            int prodId = int.Parse(Console.ReadLine());
            Console.Write("   Cantidad a reducir: ");
            int cantidad = int.Parse(Console.ReadLine());

            if (inventario.ReducirStock(prodId, cantidad))
            {
                var producto = productos.First(p => p.Id == prodId);
                Console.WriteLine($"   ✅ Stock de {producto.Nombre} actualizado. Nuevo stock: {producto.Stock}");
            }
            else
            {
                Console.WriteLine("   ❌ No se pudo reducir el stock. Verifica que el producto exista y tenga stock suficiente.");
            }
        }

        static void MostrarEstadisticasLINQ()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("   📈 ESTADÍSTICAS CON LINQ (Programación Funcional)");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // ==================== 1. WHERE ====================
            Console.WriteLine("【1】 WHERE - Filtrar productos que cuestan más de $50");
            Console.WriteLine("    → Esto es como decir: 'Solo los que precio > 50'");

            var productosCaros = productos.Where(p => p.Precio > 50);
            Console.WriteLine($"    Resultado: {productosCaros.Count()} productos encontrados");
            foreach (var p in productosCaros)
            {
                Console.WriteLine($"      • {p.Nombre}: ${p.Precio}");
            }
            Console.WriteLine();

            // ==================== 2. SELECT ====================
            Console.WriteLine("【2】 SELECT - Extraer SOLO los nombres de los productos");
            Console.WriteLine("    → Esto es como decir: 'Dame una lista solo con los nombres'");

            var soloNombres = productos.Select(p => p.Nombre);
            Console.WriteLine($"    Nombres: {string.Join(" | ", soloNombres)}");
            Console.WriteLine();

            // ==================== 3. AGGREGATE ====================
            Console.WriteLine("【3】 AGGREGATE - Sumar el stock total de TODOS los productos");
            Console.WriteLine("    → Esto va producto por producto sumando el stock");

            int stockTotal = productos.Aggregate(0, (acumulador, producto) => acumulador + producto.Stock);
            Console.WriteLine($"    Stock total en inventario: {stockTotal} unidades");
            Console.WriteLine($"    Explicación: {string.Join(" + ", productos.Select(p => p.Stock))} = {stockTotal}");
            Console.WriteLine();

            // ==================== 4. FUNCIÓN PURA ====================
            Console.WriteLine("【4】 FUNCIÓN PURA - Calcular valor total sin modificar datos");
            Console.WriteLine("    → Una función pura NO cambia el estado original");

            decimal valorTotal = ConsultasVelvet.CalcularTotal(productos);
            Console.WriteLine($"    Valor total del inventario: ${valorTotal}");
            Console.WriteLine();

            // ==================== 5. Func<> como parámetro ====================
            Console.WriteLine("【5】 Func<> - Pasar una condición como parámetro");
            Console.WriteLine("    → Creamos una función 'filtro' y la pasamos como variable");

            Func<Producto, bool> filtroMaquillaje = p => p.Categoria == "Maquillaje";
            var productosMaquillaje = ConsultasVelvet.FiltrarProductos(productos, filtroMaquillaje);
            Console.WriteLine($"    Productos de maquillaje: {productosMaquillaje.Count}");
            foreach (var p in productosMaquillaje)
            {
                Console.WriteLine($"      • {p.Nombre}");
            }
            Console.WriteLine();

            // ==================== 6. RECORD (inmutable) ====================
            Console.WriteLine("【6】 RECORD - Tipo de dato inmutable (no se puede modificar)");
            Console.WriteLine("    → Una vez creado, no puedes cambiar sus valores");

            var productoEjemplo = new ProductoDTO("Labial Rosa", 25.99m, 50);
            Console.WriteLine($"    Record creado: {productoEjemplo}");
            Console.WriteLine("    ❌ No puedes hacer 'productoEjemplo.Precio = 30' porque es inmutable");

            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("   💡 LINQ evita escribir bucles 'for' y hace el código más legible");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            Console.WriteLine("Presiona cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
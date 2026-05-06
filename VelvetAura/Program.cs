using VelvetAura.Domain;
using VelvetAura.Services;
using VelvetAura.Aspects;
using VelvetAura.Functional;

namespace VelvetAura
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("   💖 BIENVENIDA A VELVET AURA 💖   ");
            Console.WriteLine("═══════════════════════════════════════\n");

            // ========== 1. POO: Crear objetos y relaciones ==========
            Console.WriteLine("📦 [POO] Creando clientes y pedidos...");
            var cliente = new Cliente(1, "Valentina", "vale@velvetaura.com");
            var pedido = new Pedido(cliente);

            // Suscribirse a eventos (Eventos)
            pedido.PedidoCreado += (s, e) => Console.WriteLine($"   🎀 [EVENTO] {e.Mensaje}");
            pedido.EstadoPedidoCambiado += (s, e) => Console.WriteLine($"   🎀 [EVENTO] {e.Mensaje}");

            // Crear productos
            var productos = new List<Producto>
            {
                new Producto(1, "✨ Perfume Aura", 120m, 5, "Perfumes"),
                new Producto(2, "💄 Labial Rosa", 25m, 10, "Maquillaje"),
                new Producto(3, "🖌️ Set de Brochas", 45m, 3, "Accesorios")
            };

            pedido.AgregarProducto(productos[0]);
            pedido.AgregarProducto(productos[1]);
            pedido.CambiarEstado("Confirmado");

            Console.WriteLine($"   Total del pedido: {pedido.Total:C}\n");

            // ========== 2. Funcional: LINQ, funciones puras, record ==========
            Console.WriteLine("🧮 [FUNCIONAL] Demostración de LINQ, Func y Record:");
            ConsultasVelvet.DemostrarLINQ(productos);

            // Función pura
            decimal totalFuncional = ConsultasVelvet.CalcularTotal(productos);
            Console.WriteLine($"   💰 Función pura (CalcularTotal): {totalFuncional:C}");

            // Func<> como parámetro
            var maquillaje = ConsultasVelvet.FiltrarProductos(productos, p => p.Categoria == "Maquillaje");
            Console.WriteLine($"   🎨 Filtrar con Func<>: {maquillaje.Count} productos de maquillaje");

            // Record inmutable
            var dto = new ProductoDTO("Rímel", 15m, 20);
            Console.WriteLine($"   📄 Record inmutable: {dto}\n");

            // ========== 3. Aspectos: Castle Windsor con interceptores ==========
            Console.WriteLine("🔧 [ASPECTOS] Configurando Castle Windsor e interceptores:");
            var container = WindsorContainerConfig.Configure();
            var pagoService = container.Resolve<IPagoService>();

            var pago = new PagoTarjeta { Monto = pedido.Total, NumeroTarjeta = "4111111111111234" };
            Console.WriteLine("   Ejecutando PagoService.ProcesarPago (con interceptor de Logging y Error):");
            pagoService.ProcesarPago(pago);

            // ========== 4. Eventos de inventario ==========
            Console.WriteLine("\n📦 [EVENTOS] Demostración de StockActualizado:");
            var inventario = new Inventario();
            inventario.StockActualizado += (s, e) => Console.WriteLine($"   🎀 [EVENTO] {e.Mensaje}");

            inventario.AgregarProducto(productos[2]);
            inventario.ReducirStock(3, 1);
            inventario.ReducirStock(3, 2);  // Intentar reducir más de lo que hay (NO genera excepción, el interceptor manejaría errores)

            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("   ✨ SISTEMA VELVET AURA FINALIZADO ✨   ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.ReadKey();
        }
    }
}
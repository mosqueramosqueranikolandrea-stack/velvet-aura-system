using VelvetAura.Domain;
using VelvetAura.Events;

var cliente = new Cliente(1, "Luna", "luna@velvetaura.com");
var pedido = new Pedido(cliente);
pedido.PedidoCreado += (sender, e) => Console.WriteLine($"[EVENTO] {e.Mensaje}");
pedido.EstadoPedidoCambiado += (sender, e) => Console.WriteLine($"[EVENTO] {e.Mensaje}");

pedido.AgregarProducto(new Producto(101, "Perfume Aura", 120m, 5, "Perfumes"));
pedido.CambiarEstado("Pagado");
Console.WriteLine($"Total pedido: {pedido.Total:C}");

var inventario = new Inventario();
inventario.StockActualizado += (sender, e) => Console.WriteLine($"[EVENTO] {e.Mensaje}");
var labial = new Producto(102, "Labial Rosa", 25m, 10, "Maquillaje");
inventario.AgregarProducto(labial);
inventario.ReducirStock(102, 2);
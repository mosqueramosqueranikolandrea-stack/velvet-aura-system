using System;
using System.Collections.Generic;
using System.Linq;
using VelvetAura.Events;

namespace VelvetAura.Domain
{
    public class Inventario
    {
        public List<Producto> Productos { get; set; } = new List<Producto>();

        public event EventHandler<StockEventArgs> StockActualizado;

        public void AgregarProducto(Producto producto)
        {
            Productos.Add(producto);
            OnStockActualizado(new StockEventArgs($"📦 Producto {producto.Nombre} añadido al inventario"));
        }

        public bool ReducirStock(int productoId, int cantidad)
        {
            var prod = Productos.FirstOrDefault(p => p.Id == productoId);
            if (prod == null || prod.Stock < cantidad)
                return false;

            prod.Stock -= cantidad;
            OnStockActualizado(new StockEventArgs($"📉 Stock de {prod.Nombre} reducido en {cantidad}. Nuevo stock: {prod.Stock}"));
            return true;
        }

        protected virtual void OnStockActualizado(StockEventArgs e)
        {
            StockActualizado?.Invoke(this, e);
        }
    }
}

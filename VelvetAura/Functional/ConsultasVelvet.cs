using System;
using System.Collections.Generic;
using System.Linq;
using VelvetAura.Domain;

namespace VelvetAura.Functional
{
    public static class ConsultasVelvet
    {
        // Función pura (sin efectos secundarios)
        public static decimal CalcularTotal(IEnumerable<Producto> productos)
        {
            return productos.Sum(p => p.Precio);
        }

        // Func<> como parámetro (alto orden)
        public static List<Producto> FiltrarProductos(List<Producto> productos, Func<Producto, bool> filtro)
        {
            return productos.Where(filtro).ToList();
        }

        // Demostración de LINQ con Where, Select, Aggregate
        public static void DemostrarLINQ(List<Producto> productos)
        {
            // WHERE
            var caros = productos.Where(p => p.Precio > 50);
            Console.WriteLine("   🎀 WHERE → Productos caros (>50): " + string.Join(", ", caros.Select(p => p.Nombre)));

            // SELECT
            var nombres = productos.Select(p => p.Nombre);
            Console.WriteLine("   📝 SELECT → Nombres: " + string.Join(", ", nombres));

            // AGGREGATE
            var stockTotal = productos.Aggregate(0, (acum, p) => acum + p.Stock);
            Console.WriteLine($"   📊 AGGREGATE → Stock total: {stockTotal}");
        }
    }
}
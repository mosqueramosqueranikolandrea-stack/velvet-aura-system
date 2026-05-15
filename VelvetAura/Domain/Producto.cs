
// ============================================================
//   PARADIGMA: PROGRAMACIÓN FUNCIONAL
//    - Una vez creado, no se pueden modificar sus valores
// ============================================================

namespace VelvetAura.Domain
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }

        public Producto(int id, string nombre, decimal precio, int stock, string categoria)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }
    }
}

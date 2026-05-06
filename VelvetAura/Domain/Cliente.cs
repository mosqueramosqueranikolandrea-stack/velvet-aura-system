using System.Collections.Generic;

namespace VelvetAura.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Cliente(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
        }
    }
}
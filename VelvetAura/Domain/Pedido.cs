using System;
using System.Collections.Generic;
using System.Linq;
using VelvetAura.Events;

// ============================================================
//   PARADIGMA: POO (Programación Orientada a Objetos)
//    - Clase Pedido con encapsulamiento (propiedades privadas)
//    - Relación de composición: Pedido contiene Productos
//    - Relación de agregación: Pedido pertenece a un Cliente
//
//   PARADIGMA: EVENTOS
//    - Eventos semánticamente significativos:
//      * PedidoCreado: ocurre cuando se genera un nuevo pedido
//      * EstadoPedidoCambiado: ocurre cuando el pedido cambia de estado
//    - EventArgs personalizados con información relevante
// ============================================================

namespace VelvetAura.Domain
{
    public class Pedido
    {
        private static int _ultimoId = 0;
        public int Id { get; private set; }
        public Cliente Cliente { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public DateTime FechaCreacion { get; private set; }
        public string Estado { get; private set; }
        public decimal Total => Productos.Sum(p => p.Precio);

        // EVENTOS (tema importante)
        public event EventHandler<PedidoEventArgs> PedidoCreado;
        public event EventHandler<PedidoEventArgs> EstadoPedidoCambiado;

        public Pedido(Cliente cliente)
        {
            Id = ++_ultimoId;
            Cliente = cliente;
            FechaCreacion = DateTime.Now;
            Estado = "Creado";

            // Disparar evento
            OnPedidoCreado(new PedidoEventArgs($"✨ Pedido {Id} creado para {cliente.Nombre}"));
        }

        public void AgregarProducto(Producto producto)
        {
            Productos.Add(producto);
        }

        public void CambiarEstado(string nuevoEstado)
        {
            var anterior = Estado;
            Estado = nuevoEstado;
            OnEstadoCambiado(new PedidoEventArgs($"🔄 Estado cambiado de '{anterior}' a '{nuevoEstado}'"));
        }

        protected virtual void OnPedidoCreado(PedidoEventArgs e)
        {
            PedidoCreado?.Invoke(this, e);
        }

        protected virtual void OnEstadoCambiado(PedidoEventArgs e)
        {
            EstadoPedidoCambiado?.Invoke(this, e);
        }
    }
}

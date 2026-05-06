using System;

namespace VelvetAura.Events
{
    public class PedidoEventArgs : EventArgs
    {
        public string Mensaje { get; set; }

        public PedidoEventArgs(string mensaje)
        {
            Mensaje = mensaje;
        }
    }
}

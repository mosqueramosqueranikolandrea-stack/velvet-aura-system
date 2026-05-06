using System;

namespace VelvetAura.Events
{
    public class StockEventArgs : EventArgs
    {
        public string Mensaje { get; set; }

        public StockEventArgs(string mensaje)
        {
            Mensaje = mensaje;
        }
    }
}

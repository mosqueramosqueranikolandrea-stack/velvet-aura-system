using System;

// ============================================================
//   PARADIGMA: EVENTOS
//    - EventArgs personalizado semánticamente significativo
//    - Contiene información útil para el negocio
// ============================================================

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

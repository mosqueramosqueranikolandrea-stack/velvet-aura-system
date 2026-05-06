using System;
using VelvetAura.Domain;

namespace VelvetAura.Services
{
    public class PagoService : IPagoService
    {
        public void ProcesarPago(Pago pago)
        {
            pago.ProcesarPago();
        }
    }
}

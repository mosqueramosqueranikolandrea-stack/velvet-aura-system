using VelvetAura.Domain;

namespace VelvetAura.Services
{
    public interface IPagoService
    {
        void ProcesarPago(Pago pago);
    }
}
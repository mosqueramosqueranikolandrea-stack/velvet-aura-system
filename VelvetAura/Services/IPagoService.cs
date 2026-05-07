using VelvetAura.Domain;

// ============================================================
//   PARADIGMA: POO + SOLID
//    - Interfaz IPagoService (principio de segregación de interfaces)
//    - Permite inversión de dependencias (DIP)
// ============================================================

namespace VelvetAura.Services
{
    public interface IPagoService
    {
        void ProcesarPago(Pago pago);
    }
}
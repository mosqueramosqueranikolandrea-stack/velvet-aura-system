// ============================================================
//   PARADIGMA: POO (Programación Orientada a Objetos)
//    - Clase abstracta Pago (herencia)
//    - Método abstracto ProcesarPago (polimorfismo)
//    - PagoTarjeta hereda e implementa el método
// ============================================================

namespace VelvetAura.Domain
{
    public abstract class Pago
    {
        public decimal Monto { get; set; }
        public abstract void ProcesarPago();
    }
}
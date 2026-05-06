namespace VelvetAura.Domain
{
    public abstract class Pago
    {
        public decimal Monto { get; set; }
        public abstract void ProcesarPago();
    }
}
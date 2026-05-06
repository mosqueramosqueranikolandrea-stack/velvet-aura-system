using System;

namespace VelvetAura.Domain
{
    public class PagoTarjeta : Pago
    {
        public string NumeroTarjeta { get; set; }

        public override void ProcesarPago()
        {
            Console.WriteLine($"💳 Pago de {Monto:C} con tarjeta {NumeroTarjeta[^4..]} procesado en VelvetAura");
        }
    }
}
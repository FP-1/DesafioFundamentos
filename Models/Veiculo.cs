using System;

namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        public string Placa { get; private set; }
        public DateTime Entrada { get; private set; }

        public Veiculo(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("Placa inválida.");

            Placa = placa.Trim().ToUpperInvariant();
            Entrada = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private readonly decimal precoInicial;
        private readonly decimal precoHora;
        private readonly List<Veiculo> veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoHora)
        {
            this.precoInicial = precoInicial;
            this.precoHora = precoHora;
        }

        public void AdicionarVeiculo(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("Placa inválida.");

            var p = placa.Trim().ToUpperInvariant();

            if (veiculos.Any(v => v.Placa == p))
                throw new InvalidOperationException("Veículo já cadastrado.");

            veiculos.Add(new Veiculo(p));
        }

        // Remove o veículo e retorna o valor a ser pago. Também retorna horas e horário de entrada.
        public decimal RemoverVeiculo(string placa, out double horas, out DateTime entrada)
        {
            var p = placa?.Trim().ToUpperInvariant();

            var veiculo = veiculos.FirstOrDefault(v => v.Placa == p);
            if (veiculo == null)
                throw new InvalidOperationException("Veículo não encontrado.");

            entrada = veiculo.Entrada;
            var duracao = DateTime.Now - veiculo.Entrada;

            // arredonda para cima o total de horas (1h mínimo)
            horas = Math.Ceiling(duracao.TotalHours);
            if (horas < 1) horas = 1;

            decimal valor = precoInicial + (decimal)horas * precoHora;

            veiculos.Remove(veiculo);

            return valor;
        }

        public IReadOnlyList<Veiculo> ListarVeiculos()
        {
            return veiculos.AsReadOnly();
        }
    }
}

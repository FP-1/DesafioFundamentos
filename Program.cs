using System;
using System.Globalization;
using DesafioFundamentos.Models;

namespace DesafioFundamentos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Sistema de Estacionamento (DIO) - Console ===");
            Console.Write("Informe o preço inicial (ex: 5,00): ");
            decimal precoInicial = LerDecimal();

            Console.Write("Informe o preço por hora (ex: 2,00): ");
            decimal precoHora = LerDecimal();

            var estacionamento = new Estacionamento(precoInicial, precoHora);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Digite a sua opção:");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Remover veículo");
                Console.WriteLine("3 - Listar veículos");
                Console.WriteLine("4 - Encerrar");
                Console.Write("Opção: ");
                var opc = Console.ReadLine();

                switch (opc)
                {
                    case "1":
                        Console.Write("Digite a placa do veículo: ");
                        var placa = Console.ReadLine();
                        try
                        {
                            estacionamento.AdicionarVeiculo(placa);
                            Console.WriteLine("Veículo cadastrado com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro: {ex.Message}");
                        }
                        Pause();
                        break;

                    case "2":
                        Console.Write("Digite a placa do veículo para remover: ");
                        var placaRem = Console.ReadLine();
                        try
                        {
                            var valor = estacionamento.RemoverVeiculo(placaRem, out double horas, out DateTime entrada);
                            Console.WriteLine($"Veículo removido.");
                            Console.WriteLine($"Entrada: {entrada:G}");
                            Console.WriteLine($"Tempo cobrado (horas arredondadas): {horas}h");
                            Console.WriteLine($"Valor a pagar: R$ {valor:F2}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro: {ex.Message}");
                        }
                        Pause();
                        break;

                    case "3":
                        var lista = estacionamento.ListarVeiculos();
                        Console.WriteLine("Veículos estacionados:");
                        if (lista.Count == 0)
                            Console.WriteLine("Nenhum veículo estacionado.");
                        else
                            foreach (var v in lista)
                                Console.WriteLine($"- {v.Placa} (Entrada: {v.Entrada:G})");
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine("Encerrando o programa...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        static decimal LerDecimal()
        {
            var culture = new CultureInfo("pt-BR");
            while (true)
            {
                var s = Console.ReadLine();
                if (decimal.TryParse(s, System.Globalization.NumberStyles.Number, culture, out decimal val))
                    return val;
                if (decimal.TryParse(s, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                    return val;
                Console.Write("Valor inválido. Tente novamente: ");
            }
        }
    }
}

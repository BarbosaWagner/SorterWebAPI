using SorterWebAPI.Models;
using System.IO;

namespace SorterWebAPI.Data
{
    public class Arquivo
    {
        private string diretorioDeRegistro = @"C:\Temp\";

        // Grava em arquivo TXT os dados do sorteio e do cliente.
        public void GravarArquivoTXT(Cliente dadosCliente, int numeroSorteado)
        {
            using (StreamWriter writer = new StreamWriter(diretorioDeRegistro + numeroSorteado + ".txt"))
            {
                writer.WriteLine("CPF: " + dadosCliente.Cpf);
                writer.WriteLine("Nome: " + dadosCliente.Nome);
                writer.WriteLine("Email: " + dadosCliente.Email);
                writer.WriteLine("Telefone: " + dadosCliente.Telefone);
                writer.WriteLine("NumeroSorteado: " + numeroSorteado);
            }
        }
    }
}
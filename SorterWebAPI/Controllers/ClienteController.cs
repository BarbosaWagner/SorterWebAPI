using SorterWebAPI.Data;
using SorterWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SorterWebAPI.Controllers
{
    public class ClienteController : ApiController
    {
        private static List<Cliente> clientes = new List<Cliente>();

        public List<Cliente> Get()
        {
            

            return clientes;
        }

        public string Put(string cpf, string nome, string email, string telefone)
        {
            DataBase db = new DataBase();

            try
            {
                Cliente dadosCliente = new Cliente(nome, telefone, cpf, email);

                if (!db.BuscarCliente(dadosCliente.Cpf))
                {
                    if (!db.ChecarSeEmailJaExiste(dadosCliente.Email))
                    {
                        // Cliente novo e com email válido.
                        db = new DataBase();
                        db.InserirCliente(dadosCliente);

                        Sorteio sorteio = new Sorteio();
                        sorteio.Numero = sorteio.SortearNumero();
                        while(db.NumeroSorteadoJaExiste(sorteio.Numero))
                        {
                            sorteio.Numero = sorteio.SortearNumero();
                        }

                        db.RegistrarSorteio(sorteio.Numero, dadosCliente);
                        return "Cliente cadastrado com sucesso | Nome: " + dadosCliente.Nome.ToUpper() + " | NúmeroSorteado: " + sorteio.Numero;
                    }
                    else
                        return "O email informado já existe em outro CPF cadastrado";
                }
                else
                {
                    // Cliente já cadastrado.
                    db = new DataBase();
                    Sorteio sorteio = new Sorteio();
                    sorteio.Numero = sorteio.SortearNumero();
                    while (db.NumeroSorteadoJaExiste(sorteio.Numero))
                    {
                        sorteio.Numero = sorteio.SortearNumero();
                    }

                    db.RegistrarSorteio(sorteio.Numero, dadosCliente);
                    return "Cliente já cadastrado! | Nome: " + dadosCliente.Nome.ToUpper() + " | NúmeroSorteado: " + sorteio.Numero;
                }
            }
            catch (Exception e)
            {
                return ("Dados de entrada inválidos para o objeto Cliente! Motivo: " + e.Message);
            }
        }

        public bool EhClienteNovo(Cliente dadosCliente)
        {
            DataBase db = new DataBase();
            if (db.BuscarCliente(dadosCliente.Cpf.ToString()))
                return false;
            else
                return true;
        }
    }
}
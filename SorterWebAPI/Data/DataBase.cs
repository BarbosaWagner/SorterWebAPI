using System.Data.SqlClient;
using SorterWebAPI.Models;

namespace SorterWebAPI.Data
{
    public class DataBase
    {
        SqlConnection conexao;
        SqlCommand comando;
        SqlDataReader dataReader;
        string strSQL;


        // Insere um novo cadastro de cliente no BD de acordo com a definição do objeto.
        public bool InserirCliente(Cliente dadosCliente)
        {
            try
            {
                // Conexão com DB SQL Server Express (LocalDB).
                using (conexao = new SqlConnection(@"Server=(localdb)\MSSQLLOCALDB; Database=SorterDB; User Id=sa; Password=qwedsa"))
                {
                    strSQL = "INSERT INTO CAD_CLIENTE (Cpf, Nome, Email, Telefone) VALUES (@cpf, @nome, @email, @telefone)";

                    comando = new SqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@cpf", dadosCliente.Cpf.ToString());
                    comando.Parameters.AddWithValue("@nome", dadosCliente.Nome.ToString());
                    comando.Parameters.AddWithValue("@email", dadosCliente.Email.ToString());
                    comando.Parameters.AddWithValue("@telefone", dadosCliente.Telefone.ToString());

                    conexao.Open();
                    comando.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Valida se o CPF do cliente já está cadastrado no banco;
        public bool BuscarCliente(string cpf)
        {
            try
            {
                // Conexão com DB SQL Server Express (LocalDB).
                using (conexao = new SqlConnection(@"Server=(localdb)\MSSQLLOCALDB; Database=SorterDB; User Id=sa; Password=qwedsa"))
                {
                    strSQL = "SELECT COUNT(*) FROM CAD_CLIENTE WHERE Cpf = @cpf";

                    comando = new SqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@cpf", cpf);

                    conexao.Open();
                    dataReader = comando.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader.GetInt32(0) > 0)
                            return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Valida se já existe o Email cadastrado no banco;
        public bool ChecarSeEmailJaExiste(string email)
        {
            try
            {
                // Conexão com DB SQL Server Express (LocalDB).
                using (conexao = new SqlConnection(@"Server=(localdb)\MSSQLLOCALDB; Database=SorterDB; User Id=sa; Password=qwedsa"))
                {
                    strSQL = "SELECT COUNT(*) FROM CAD_CLIENTE WHERE Email = @email";

                    comando = new SqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@email", email);

                    conexao.Open();
                    dataReader = comando.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader.GetInt32(0) > 0)
                            return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Insere um novo registro de sorteio no banco.
        public bool RegistrarSorteio(int numeroSorteado, Cliente dadosCliente)
        {
            try
            {
                // Conexão com DB SQL Server Express (LocalDB).
                using (conexao = new SqlConnection(@"Server=(localdb)\MSSQLLOCALDB; Database=SorterDB; User Id=sa; Password=qwedsa"))
                {
                    strSQL = "INSERT INTO REG_SORTEIO (Numero, CpfCliente) VALUES (@numero, @cpf)";

                    comando = new SqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@numero", numeroSorteado);
                    comando.Parameters.AddWithValue("@cpf", dadosCliente.Cpf.ToString());

                    conexao.Open();
                    comando.ExecuteNonQuery();

                    Arquivo sorteio = new Arquivo();
                    sorteio.GravarArquivoTXT(dadosCliente, numeroSorteado);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Valida se o número sorteado já está cadastrado no banco;
        public bool NumeroSorteadoJaExiste(int numero)
        {
            try
            {
                // Conexão com DB SQL Server Express (LocalDB).
                using (conexao = new SqlConnection(@"Server=(localdb)\MSSQLLOCALDB; Database=SorterDB; User Id=sa; Password=qwedsa"))
                {
                    strSQL = "SELECT COUNT(*) FROM REG_SORTEIO WHERE Numero = @numero";

                    comando = new SqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@numero", numero);

                    conexao.Open();
                    dataReader = comando.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader.GetInt32(0) > 0)
                            return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
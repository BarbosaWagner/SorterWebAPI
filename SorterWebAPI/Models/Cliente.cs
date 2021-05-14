namespace SorterWebAPI.Models
{
    public class Cliente
    {
        private string nome, cpf, telefone, email;

        public string Nome
        {
            get { return nome; }
            set
            {
                if (value.Length > 0)
                    nome = value;
            }
        }
        public string Telefone 
        {
            get { return telefone; }
            set { telefone = value; }
        }
        public string Cpf
        {
            get { return cpf; }
            set
            {
                // Verifica se o CPF possui no mínimo 10 dígitos (possível 0 à esquerda) e se todos os dígitos são números.
                if(value.Length >= 10)
                {
                    foreach (char numero in value)
                        if ((int)numero >= 0)
                            continue;

                    cpf = value;
                }
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                bool temArroba = false;
                bool pontoAposArroba = false;

                foreach (char caractere in value)
                {
                    if (caractere == '@')
                        temArroba = true;
                    if (caractere == '.' && temArroba == true)
                        pontoAposArroba = true;
                }

                if (temArroba && pontoAposArroba)
                    email = value;
            }
        }

        public Cliente(string nome, string telefone, string cpf, string email)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.Cpf = cpf;
            this.Email = email;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Namespace que contem as classes que manipulam dados
using System.Data;
using System.Data.SqlClient;
using AcessobancoDados.Properties;


namespace AcessobancoDados
{
  public class AcessoDadosSQLServer
  {
        //Criar a conexão

        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.StringConexao);
        }

        //Parametros que vão para o banco de dados 
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        //Metodo para limpar os parametros do banco de dados. (Void) não retorna nada vazio
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        //Metodo que adiciona parametros de entrada do banco de dados. (Void) não retorna nada vazio
        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Percistência de Dados (manter: Inserir, Excluir, Alterar) o CommandTyp é um enumerador Exp; sexo:  Masc ou Femunino 

    public object ExecutarParametro(CommandType commandType, string nomeStoredProcedureOuTextoSql)

    {
            try

            { 
                //Criar a conexão; Abre o caminho do DotNet que vai até o BD. 

                SqlConnection sqlConnection = CriarConexao();

                //Abrir a Conexao 

                sqlConnection.Open();

                //Criar o comando que vai até o BD pela conexao. 

                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandType = commandType;

                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;

                sqlCommand.CommandTimeout = 7200; //tempo para segurar o bd aberto em 2HS; 

                //Adicionar os parâmtros que estão na Procedure 

                foreach (SqlParameter sqlParameter in sqlParameterCollection)

                {

                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                }

                //Executar o comando que vai até o BD. 

                return sqlCommand.ExecuteScalar();

            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }

    }

        //Consultar Registro do Banco de Dados 

        public DataTable ExecutarConsulta(CommandType commandType, String nomeStoredProcedureOuTextoSql)

        {

            try

            {

                //Criar a conexão; Abre o caminho do DotNet que vai até o BD. 

                SqlConnection sqlConnection = CriarConexao();

                //Abrir a Conexao 

                sqlConnection.Open();

                //Criar o comando que vai até o BD pela conexao. 

                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandType = commandType;

                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;

                sqlCommand.CommandTimeout = 7200; //tempo para segurar o bd aberto em 2HS; 


                //Adicionar os parâmtros que estão na Procedure 

                foreach (SqlParameter sqlParameter in sqlParameterCollection)

                {

                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                }

                //Criando um Adptador para transcrever os comandos do BDSQL com O DotNet(dataTable). 

                SqlDataAdapter sqlDataAdpter = new SqlDataAdapter(sqlCommand);

                //Criando um DataTable vasio para receber Registro no BD 

                DataTable dataTable = new DataTable();

                //Manda o comando ir ate o BD e o Adptador preenche a Tabela 

                sqlDataAdpter.Fill(dataTable);

                //Retorna o Metodo DataTable 

                return (dataTable);

            }

            catch (Exception ex)

            {
                throw new Exception(ex.Message);
            }
        }  
  }
}

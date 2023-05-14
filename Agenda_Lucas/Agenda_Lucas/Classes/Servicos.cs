using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Lucas.Classes
{
    public class Servicos
    {
        //Criando a conexão com o bd

        public static string SenhaFirebase = "az6rsiMQQwHF5sGcKVFckLbMCyugOG4UMNiMfGZH";

        FirebaseClient Cliente = new FirebaseClient
            ("https://agenda-lucas-default-rtdb.firebaseio.com/",
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(SenhaFirebase) });

        //<bool>: Task vai retornar um resultado bool(V/F)
        public async Task<bool> RegistraContato(string nome, string telefone1, string telefone2,
            string email, string cep, string logradouro, string numero, string bairro,
            string complemento, string cidade, string uf)
        {
            /**try catch pode capturar um erro, também em relação a conexão
             * do usuário no bloco catch, onde ele tenta(try) executar o código, 
             * caso necessite da conexão com a interner e não conseguir, ele entra no catch
             * Esse try é a tentativa de fazer o registro
             */
            try
            {
                /**
                 * Algoritmo que precisa ser aguardado pra consultar o 
                 * servidor e retornar um resultado.
                 * .PostAsync é a função que registra os dados
                 */
                await Cliente.Child("Cadastros")
                    .PostAsync(new Contatos()
                    {
                        Nome = nome,
                        Tel1 = telefone1,
                        Tel2 = telefone2,
                        Email = email,
                        Cep = cep,
                        Logradouro = logradouro,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cidade = cidade,
                        Uf = uf

                    });
                return true;

            }
            catch
            {
                return false;
            }
        }
        //Lista é o nome
        public async Task<List<Contatos>> Lista()
        {
            //puxando a lista do servidor 
            var listagem = (await Cliente.Child("Cadastros")
                .OnceAsync<Contatos>())
                .Select(f => new Contatos()
                {
                    Nome = f.Object.Nome,
                }).ToList();

            return listagem;
        }
         

    }
}

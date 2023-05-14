using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Lucas.Classes
{
    public class ServicosUser
    {
        /**
         * 1º declaramos a chave de acesso que copiamos do firebase, que 
         * vai servir pro nosso app logar lá no firebase e conseguir 
         * fazer o registro dos usuários
         * https://agenda-lucas-default-rtdb.firebaseio.com/
         * az6rsiMQQwHF5sGcKVFckLbMCyugOG4UMNiMfGZH
         */
        public static string SenhaFirebase = "az6rsiMQQwHF5sGcKVFckLbMCyugOG4UMNiMfGZH";
        /**
         * Declarando meu serviço do firebase(No inicio vai dar erro
         * por que preciso baixar o pacote ou SDK do firebase).
         * No código abaixo, eu chamei as opções do Firebase, entre chaves eu coloquei
         * essas opções que é autenticação por token.
         * No final eu abri uma tarefa(Task é uma tarefa) que buscou do resultado 
         * da minha string SenhaFirebase, o token pra fazer o acesso.
         */
        FirebaseClient Cliente = new FirebaseClient
            ("https://agenda-lucas-default-rtdb.firebaseio.com/", 
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(SenhaFirebase) });

        /**
         * Método que retorna verdadeiro ou falso.
         * async significado que vai depender de um resultado que vai ser obtido,
         * vai ter que conectar no bd, aguardar resposta; Por mais que seje rapido
         * isso eu preciso que meu método aguarde a conclusão dele pra continuar
         * a execução do aplicativo.
         * 
         */
        public async Task<bool> RegistarUsuario(string user, string senha)
        {
            //tenta executar o método, se der certo retorna verdadeiro(por causa do <bool>
            try
            {
                /**
                 * await faz com que nosso método aguarde a conclusão 
                 * chamando o bd que é o Client.
                 * Child é o local do nosso cliente que vai estar armazenado os dados,
                 * é uma tabela, eu posso especificar o nome entre().
                 * .postAsync método que escreve as informações que vão estar no
                 * nosso formulário dentro do Firebase, dentro postAsync eu especifico que
                 * estou criando meus usuários,chamando a classe
                 */
                await Cliente.Child("Usuarios")
                    .PostAsync(new Usuarios()
                    {
                        Usuario = user,
                        Senha = senha
                    });
                return true;
            } 
            //retorna falso ser der um erro
            catch
            {
                return false;
            }
        }

        public async Task<bool> VerificarLogin(string login, string loginsenha) 
        {
            /**
             * .OnceAsync: inicia ndo o parametro da consulta, e entre<>
             * vou especificar daonde eu vou puxar os parametros pra buscar,
             * no nosso caso nossas variaveis que criam a tabela la, que ta 
             * na classe Usuarios.
             * .Where(): usamos pra localizar login e senha.
             * u é uma variável que vai fazer a busca do usuario e senha, é o
             * que vai ser atribuida esse valor 
             */
            var consultar = (await Cliente.Child("Usuarios")
                .OnceAsync<Usuarios>())
                //abaixo verificamos se a var usuario é igual login
                //login é o que o usuário digitou, usuario é o que está no bd
                .Where(u => u.Object.Usuario == login)
                .Where(u => u.Object.Senha == loginsenha)
                //Localiza o primeiro ou padrão
                .FirstOrDefault();
            //se o código acima der certo, consultar vai ter qualquer valor
            //não interessa qual. vai ser verdadeiro se consultar for != null
            return consultar != null;
        }

    }
}

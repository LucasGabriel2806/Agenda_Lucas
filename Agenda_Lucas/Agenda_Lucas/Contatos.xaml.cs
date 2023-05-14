using Agenda_Lucas.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda_Lucas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contatos : TabbedPage
    {
        public Contatos()
        {
            InitializeComponent();

            //Especificando no contexto de bind aqui, então ele vai
            //trabalhar procurando um modelo. Essa página vai carregar procurando um modelo
            BindingContext = this;

            CarregaLista();
        }
        public async void CarregaLista()
        {
            var lista = new Servicos();
            var listagem = lista.Lista();

            CVLista.ItemsSource = await listagem;
        }

        private async void TXTCep_Unfocused(object sender, FocusEventArgs e)
        {

            try
            {
                /**
                 * Consome JSON da web para byscar dados a partir do CEP
                 * HttpClient: Vai puxar de um endereço da internet
                 * var cliente vai puxar um endereço da internet
                 * JsonConvert:função uqe faz a conversão
                 * DeserializeObject: Deserializa, quebra esse objeto. Pra ele 
                 * poder quebrar, ele vai ter que ter alvos pra isso. pra poder 
                 * saber onde ele vai colocar cada elemento, ele vai precisar
                 * dessas variaveis que estão em <Endereco>.
                 * (json)variavel que ele vai tirar isso, que captura o endereço.
                 */

                var cliente = new HttpClient();
                var cep = TXTCep.Text;
                var json = await cliente.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
                var dados = JsonConvert.DeserializeObject<Classes.Endereco>(json);
                //Fim da consulta

                //Usa a classe 'Endereco' para devolver os dados
                TXTLogradouro.Text = dados.logradouro;
                TXTBairro.Text = dados.bairro;
                TXTCidade.Text = dados.localidade;
                TXTUF.Text = dados.uf;

                TXTNumero.Focus();
            }
            catch
            {
                await DisplayAlert("⛔ Erro", "ocorreu um erro ao tentar localizar o CEP. Tente Novamente!", "OK");
                return;
            }

        }

        private async void BTNSalvar_Clicked(object sender, EventArgs e)
        {

            try
            {
                var salvar = new Servicos();
                bool registrar = await salvar.RegistraContato(
                    TXTNome.Text, 
                    TXTTel1.Text, 
                    TXTTel2.Text, 
                    TXTEmail.Text,
                    TXTCep.Text,
                    TXTLogradouro.Text,
                    TXTNumero.Text,
                    TXTBairro.Text,
                    TXTComplemento.Text,
                    TXTCidade.Text,
                    TXTUF.Text
                    );
                if ( registrar )
                {
                    await DisplayAlert("✅ Sucesso", "Contato salvo!", "OK");
                    LimpaCampos();
                    return;
                }
                else
                {
                    await DisplayAlert("⛔ Erro", "ocorreu um erro ao tentar salvar!", "OK");
                    return;
                }
            }
            catch
            {
                await DisplayAlert("⛔ Erro", "ocorreu um erro ao tentar salvar!", "OK");
                return;
            }

        }

        public void LimpaCampos()
        {
            TXTNome.Text = string.Empty;
            TXTTel1.Text = string.Empty;
            TXTTel2.Text = string.Empty;
            TXTEmail.Text = string.Empty;
            TXTCep.Text = string.Empty;
            TXTLogradouro.Text = string.Empty;
            TXTNumero.Text = string.Empty;
            TXTBairro.Text = string.Empty;
            TXTComplemento.Text = string.Empty;
            TXTCidade.Text = string.Empty;
            TXTUF.Text = string.Empty;
        }

        private void BTNLimpar_Clicked(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        
    }
}
using Agenda_Lucas.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agenda_Lucas
{
    public partial class MainPage : ContentPage
    {
        bool carregando;
        public MainPage()
        {
            InitializeComponent();
        }

        public void TelaCarregamento()
        {
            
                
            if (carregando)
            {
                BVTelaPreta.IsVisible = false;
                ACTRoda.IsVisible = false;
                ACTRoda.IsRunning = false;

                carregando = false;
            }
            else
            {
                BVTelaPreta.IsVisible = true;
                ACTRoda.IsVisible = true;
                ACTRoda.IsRunning = true;

                carregando = true;
            }
            
        }

        private async void BTNLogar_Clicked(object sender, EventArgs e)
        {
            try
            {
                TelaCarregamento();
                //var que vai receber o valor da classe
                //logar vai receber todos os métodos que tem o ServicosUser
                var logar = new ServicosUser();
                //chamando o método que retorna true ou false
                bool verificarlogin = await logar.VerificarLogin(TXTEmail.Text, TXTSenha.Text);
                //await aguarda o metodo terminar a verificação pra poder prosseguir

                if (verificarlogin)
                {
                    await DisplayAlert("Sucesso", "Usuário logado", "OK");

                    //Aqui não precisa pular a linha, o importante é não esquecer do ;
                    TXTEmail.Text = string.Empty; TXTSenha.Text = string.Empty;

                    Navigation.PushAsync(new Contatos());

                    TelaCarregamento();
                }
                else
                {
                    await DisplayAlert("Falha", "Usuário e senha não correspondem", "OK");
                    TelaCarregamento();
                }

            } catch {

                await DisplayAlert("Falha", "Ocorreu um erro!", "OK");
                TelaCarregamento();

            }



        }

        private void BTNCriar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CriarAcesso());
        }

        private void BTNSobre_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Sobre());
        }
    }
}

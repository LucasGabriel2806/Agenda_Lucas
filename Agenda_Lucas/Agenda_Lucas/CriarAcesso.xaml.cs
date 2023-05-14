using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda_Lucas.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda_Lucas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarAcesso : ContentPage
    {
        bool carregando;
        public CriarAcesso()
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

        private async void BTNCriarAcesso_Clicked(object sender, EventArgs e)
        {
            TelaCarregamento();
            if (TXTCriarSenha.Text != TXTConfirmarSenha.Text)
            {
                await DisplayAlert("Falha", "As senhas não correspondem", "OK");

                TelaCarregamento();
                return;
                //return: vai sair da operação, não vai continuar mesmo que tenha mais código 
            }
            try
            {
                var acesso = new ServicosUser();
                //var que vai criar um acesso e dar um retorno,
                //se for true, prossegue, senão, msg erro.
                bool criaracesso = await acesso.RegistarUsuario(TXTCriarEmail.Text, TXTCriarSenha.Text);
                if (criaracesso)
                {
                    await DisplayAlert("Sucesso", "Usuário criado!", "OK");

                    TelaCarregamento();

                    return;
                }
                else
                {
                    await DisplayAlert("Falha", "Não foi possível criar um usuário, tente novamente!", "OK");

                    TelaCarregamento();

                    return;
                }
            }
            catch
            { 

                await DisplayAlert("Falha", "Ocorreu um erro!", "OK");

                TelaCarregamento();
            }

        }

        private void BTNCancelar_Clicked(object sender, EventArgs e)
        {
            TXTCriarEmail.Text = string.Empty;
            TXTConfirmarSenha.Text = string.Empty;
            TXTCriarSenha.Text = string.Empty;
        }
    }
}
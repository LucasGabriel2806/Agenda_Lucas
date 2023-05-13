using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda_Lucas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Criando uma página de navegação, ela cria uma barrinha azul no topo da tela
            // que tem controles da navegação sempre que pular pra outra página
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

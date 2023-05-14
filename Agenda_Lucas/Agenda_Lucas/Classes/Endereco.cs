using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda_Lucas.Classes
{
    public class Endereco
    {
        //dados em json obtidos em: https://viacep.com.br/ws/28016110/json/
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        

    }
}

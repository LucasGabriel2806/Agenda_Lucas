using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda_Lucas.Classes
{
    public class Contatos
    {
        //Essa classe tem todos os campos que eu quero registrar no bd.
        public string Nome { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }

        //Endereço
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }

    }
}

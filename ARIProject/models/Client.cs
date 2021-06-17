using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARIProject.models
{
    class Client
    {
        public Client(string documento, string primer_nombre, string apellido, string credit_card, string tipo, string telefono)
        {
            this.documento = documento;
            this.primer_nombre = primer_nombre;
            this.apellido = apellido;
            this.credit_card = credit_card;
            this.tipo = tipo;
            this.telefono = telefono;
        }

        public string documento { get; set; }
        public string primer_nombre { get; set; }
        public string apellido { get; set; }
        public string credit_card { get; set; }
        public string tipo { get; set; }
        public string telefono { get; set; }
    }
}

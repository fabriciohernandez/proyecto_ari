using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARIProject.models
{
    class Token
    {
        public string cliente { get; set; }

        public Token(string clientData)
        {
            this.cliente = clientData;
        }
    }
}

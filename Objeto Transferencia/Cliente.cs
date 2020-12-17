using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objeto_Transferencia
{
    public class ClienteII
    {

       // Metodo mais pratico de encapsulamento (get e set)
        public int IdCliente { get; set; }
       public string Nome { get; set; }
       public DateTime DataNascimento { get; set; }
       public Boolean Genero { get; set; }
       public decimal LimiteCompras { get; set;}    
    }
}






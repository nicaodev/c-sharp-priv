using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_full_stack.Model
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Data { get; set; }
        public int CargaHoraria { get; set; }
       
    }
}

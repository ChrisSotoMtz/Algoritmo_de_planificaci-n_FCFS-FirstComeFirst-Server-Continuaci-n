using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_6
{
    class Proceso
    {

        public string id { get; set; }
        public int numero1 { get; set; }
        public int numero2 { get; set; }
        public int tiempoRestante { get; set; }
        public int tiempoEstimado { get; set; }
        public int tiempoLlegada { get; set; }
        public int tiempoFinalizacion { get; set; }
        public int tiempoRetorno { get; set; }
        public int tiempoRespuesta { get; set; }
        public int tiempoEspera { get; set; }
        public bool blocked { get; set; }
        public bool calculated { get; set; }
        public int tiempoServicio { get; set; }
        public int tiempoBloqueo { get; set; }
        public string tdo { get; set; }
        public string operacion { get; set; }
        public string resultado { get; set; }
    }
}

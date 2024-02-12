using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.DAL_UI
{
    public class clsMiConexión
    {
        /// <summary>
        /// Función que devuelve la cadena de la URI de mi API.
        /// 
        /// </summary>
        /// <returns>El enlace de la uri</returns>
        public static string uriBase()
        {
            return "http://localhost:5264/api/";
        }
    }

}

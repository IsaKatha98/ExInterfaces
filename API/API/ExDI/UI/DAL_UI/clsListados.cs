using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using BL;
using Newtonsoft.Json;

namespace UI.DAL_UI
{
    public class clsListados
    {
        /// <summary>
        /// Método que se conecta a la api y devuelve un listado de personas
        /// </summary>
        /// <returns></returns>
        public async static Task<List<clsPersona>> ListadoPersonas()
        {
            //Pedimos la uri
            string miCadenaURL = clsMiConexión.uriBase();

            //Esto es para que el enrutamiento salga bien
            Uri miUri = new Uri($"{miCadenaURL}Personas");

            List<clsPersona> listadoPersonas = new List<clsPersona>();
            HttpClient client = new HttpClient();
            HttpResponseMessage message;
            string textoJSONRespuesta;


            //Hacemos el request del listado
            message = await client.GetAsync(miUri);

            //En caso de que salga bien
            if (message.IsSuccessStatusCode)
            {
                //Guardamos el resultado en un JSON
                textoJSONRespuesta = await client.GetStringAsync(miUri);

                //Instalamos el NuGet de NewtonSoft para poder de-serializar el JSON.
                listadoPersonas = JsonConvert.DeserializeObject<List<clsPersona>>(textoJSONRespuesta);

            }

            client.Dispose();

            return listadoPersonas;
        }

        /// <summary>
        /// Método que se conecta con una api y devuelve un listado de departamentos.
        /// </summary>
        public async static Task<List<clsDepartamento>> ListadoCompletoDepartamentosDAL()
        {
            //Pedimos la uri
            string miCadenaURL = clsMiConexión.uriBase();

            //Esto es para que el enrutamiento salga bien
            Uri miUri = new Uri($"{miCadenaURL}Departamentos");

            List<clsDepartamento> listadoDepartamentos = new List<clsDepartamento>();
            HttpClient client = new HttpClient();
            HttpResponseMessage message;
            string textoJSONRespuesta;

            try
            {
                //Hacemos el request del listado
                message = await client.GetAsync(miUri);

                //En caso de que salga bien
                if (message.IsSuccessStatusCode)
                {
                    //Guardamos el resultado en un JSON
                    textoJSONRespuesta = await client.GetStringAsync(miUri);

                    //Instalamos el NuGet de NewtonSoft para poder de-serializar el JSON.
                    listadoDepartamentos = JsonConvert.DeserializeObject<List<clsDepartamento>>(textoJSONRespuesta);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listadoDepartamentos;
        }

    }

  
    

}
    


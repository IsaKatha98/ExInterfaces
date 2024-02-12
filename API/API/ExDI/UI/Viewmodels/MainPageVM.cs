using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using UI.Viewmodels.Utils;
using UI.Models;
using UI.DAL_UI;


namespace UI.Viewmodels
{
    public class MainPageVM:clsVMBase
    {
        #region atributos
        ObservableCollection<clsPersonaconListadoDepartamentos> listadoPersonasconListadoDepartamentos;
        int contadorJugadasRestantes;
        DelegateCommand comprobarCommand;
        #endregion

        #region constructores

        public MainPageVM()
        {
            //Cargamos la lista de personas con el listado de departamentos.
            cargarLista();
            contadorJugadasRestantes = 3;
            comprobarCommand = new DelegateCommand(comprobarCommandExecute); //el botón comprobar siempre estará habilitado.

        }

        private async void cargarLista()
        {
            //pedimos el listado de personas.
            List<clsPersona> listadopersonas = await clsListados.ListadoPersonas();

            //Pedimos el listado de departamentos.
            List<clsDepartamento> listadoDepartamentos = await clsListados.ListadoCompletoDepartamentosDAL();

            //Instanciamos la lista de persona con listado departamentos.
            listadoPersonasconListadoDepartamentos = new ObservableCollection<clsPersonaconListadoDepartamentos>();

            //Para en el nombre departamento mostrar el listado de departamentos,
            //Hay que recorrer el listadopersonas y asociar la persona con un listado de departamentos.
            foreach (clsPersona p in listadopersonas)
            {
                clsPersonaconListadoDepartamentos per= new clsPersonaconListadoDepartamentos(p, listadoDepartamentos);

                //Obviamos a las personas nulas.
                if (per != null)
                {
                    listadoPersonasconListadoDepartamentos.Add(per);
                }

            }

            //Notificamos cambios en esa propiedad.
            NotifyPropertyChanged("ListadoPersonasconListadoDepartamentos");
        }

        #endregion

        #region propiedades
        public ObservableCollection<clsPersonaconListadoDepartamentos> ListadoPersonasconListadoDepartamentos
        {
            get { return listadoPersonasconListadoDepartamentos; }
        }

       

        public int ContadorJugadasRestantes
        {
            get { return contadorJugadasRestantes; }
        }

        public DelegateCommand ComprobarCommand
        {
            get { return comprobarCommand;}
        }

        #endregion

        #region comandos
        /// <summary>
        /// Método que comprueba si ha habido un ganador o no. 
        /// Reinicia el juego si el usuario lo desea.
        /// </summary>
        private async void comprobarCommandExecute()
        {
            bool ganador = false;

            //nos hace falta una lista que sea listadoPersonasBien, que es la lista ganadora.
            //esta lista se comparará con aquella que nos llega desde la vista.

            

            ObservableCollection<clsPersona> listadoPersonasBien = new ObservableCollection<clsPersona>(await clsListados.ListadoPersonas());

            

            foreach (clsPersonaconListadoDepartamentos p in listadoPersonasconListadoDepartamentos)
            {
                foreach (clsPersona per in listadoPersonasBien)
                {
                    if (p.DepartamentoSeleccionado.Id == per.IdDepartamento)
                    {
                        ganador = true;
                    }
                }


            }

           //Aquí haríamos las alertas al usuario con if-else y la variable ganador.
           //Si ganador==true, se le notifica que ha ganado y se le da la opción de reiniciar o abandonar el juego.
           //Si ganador==false, se le notifica que ha perdido y se le da opción de volver al juego (restando una jugada) o
           //abandonar el juego.

        }

        #endregion
    }
}

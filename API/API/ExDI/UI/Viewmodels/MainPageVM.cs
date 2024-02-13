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
using UI.Views;


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
            comprobarCommand = new DelegateCommand(comprobarCommandExecute);

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
        private void comprobarCommandExecute()
        {
            int numAciertos=0;

            //recorremos la lista y comparamos si departamento seleccionado es igual que el id del departamentos de la clsPersona.

            foreach (clsPersonaconListadoDepartamentos p in listadoPersonasconListadoDepartamentos)
            {
                if (p.DepartamentoSeleccionado.Id == p.IdDepartamento)
                {
                    //aumentamos en 1 el número de aciertos.
                    numAciertos++;
                } 
            }

            if (numAciertos == 8)
            {
                //Ha ganado la partida.
                //Se le notifica que ha ganado y se le da la opción de reiniciar o abandonar el juego.
                ganaPartida();
            
            } else
            {
                //Ha perdido.
                //se le notifica que ha perdido y se le da opción de volver al juego (restando una jugada) o
                //abandonar el juego.
                pierdePartida();
            }

        }

        #endregion

        #region métodos y funciones
        /// <summary>
        /// Método que realiza las acciones de repetición o abandono del juego en caso de que 
        /// el usuario haya ganado.
        /// </summary>
        public async void ganaPartida()
        {
            bool repite = await App.Current.MainPage.DisplayAlert("¡Ha ganado!", "¿Quiere repetir?", "Sí", "No");

            if (repite)
            {
                //recargamos la lista.
                cargarLista();

            }
            else
            {
                //se cierra la ventana
                Application.Current.Quit();
            }
        }

        /// <summary>
        /// Método que realiza las acciones de repetición o abandono del juego en caso de que 
        /// el usuario haya perdido.
        /// </summary>

        public async void pierdePartida()
        {
            bool repite= await App.Current.MainPage.DisplayAlert("Ha perdido", "¿Quiere repetir?", "Sí", "No");

            //Si quiere repetir y hay jugadas restantes
            if (repite&&contadorJugadasRestantes>0)
            {
                //en caso de que quiera repetir, restamos 1 a los intentos.
                contadorJugadasRestantes--;
                //Notificamos cambios en esa propiedad.
                NotifyPropertyChanged("ContadorJugadasRestantes");

                //recargamos la lista.
                cargarLista();

            //si no le quedan jugadas
            } else if (contadorJugadasRestantes==0) {

                await App.Current.MainPage.DisplayAlert("Oh no", "Se ha quedado sin partidas", "OK");

                //se cerrará la ventana.
                Application.Current.Quit();
            } else
            {
                //en caso de que quiera abandonar, se cerrará la ventana.
                Application.Current.Quit();  
               
            }
        }
        #endregion

    }
}

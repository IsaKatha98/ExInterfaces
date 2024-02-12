using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using UI.DAL_UI;
using UI.Viewmodels.Utils;


namespace UI.Models
{
    public class clsPersonaconListadoDepartamentos:clsPersona
    {
        #region atributos
        private List<clsDepartamento> listaDepartamentos;
        private clsPersona persona;
        private clsDepartamento departamentoSeleccionado;
        #endregion

        #region constructores
        public clsPersonaconListadoDepartamentos(clsPersona p, List<clsDepartamento> listaDept): base(p)
        {
            listaDepartamentos = listaDept;
            this.departamentoSeleccionado = new clsDepartamento();
        }

       

        #endregion
        #region propiedades

        public List<clsDepartamento> ListaDepartamentos
        {
            get { return listaDepartamentos; }
        }

     

        public clsDepartamento DepartamentoSeleccionado
        {
            get { return departamentoSeleccionado; }
            set
            {
                departamentoSeleccionado= value;
            }
           
        }
        #endregion
    }
}

using InvoiceGen.DAL;
using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.BAL
{
    public class BAL_Common
    {
        public List<State> GetStateListForDropDown()
        {
            List<State> listSate = new List<State>();
            DAL_Common dAL_Common = new DAL_Common();
            listSate = dAL_Common.GetStateList();
            State state = new State();
            state.Name = "--Select State--";
            state.ID = 0;
            listSate.Add(state);
            return listSate;
        }

        public List<City> GetCityByStateIDForDropDown(string selectedState)
        {
            List<City> listCity = new List<City>();
            DAL_Common dAL_Common = new DAL_Common();
            listCity = dAL_Common.GetCityByStateID(selectedState);
            City state = new City();
            state.Name = "--Select City--";
            state.ID = 0;
            listCity.Add(state);
            return listCity;
        }

        public State GetStateByID(string number)
        {
            DAL_Common dAL_Common = new DAL_Common();
            return dAL_Common.GetStateByID(number);
        }

        public static bool StringHasCharacters(string text)
        {
            var chrarr = text.ToCharArray();
            if (chrarr.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.Entities;

namespace InvoiceGen.DAL
{
    public class DAL_Common
    {
        public List<State> GetStateList()
        {
            List<State> listSate = new List<State>();
            using (var context = new InvoiceGenEntities())
            {
                listSate = (from a in context.States
                            where a.Name != null
                            select a).ToList();
            }
            return listSate;

        }

        public List<City> GetCityByStateID(string selectedState)
        {
            List<City> listCity = new List<City>();
            int id = Convert.ToInt32(selectedState);
            using (var context = new InvoiceGenEntities())
            {
                listCity = (from a in context.Cities
                            where a.StateID == id
                            select a).ToList();
            }
            return listCity;
        }

        public State GetStateByID(string number)
        {
            State state = new State();
            var query = "select s.Name,s.ID,null as country_id from GstStateCodes gst inner join States s on s.ID = gst.StateID where gst.GSTStateCode =";
            query += Convert.ToString(number) + " ";
            using (var context = new InvoiceGenEntities())
            {
                state = context.States
                        .SqlQuery(query)
                        .FirstOrDefault();
            }
            return state;
        }
    }
}

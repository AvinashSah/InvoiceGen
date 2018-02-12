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

        public UserMaster GetUserdetailsByUsername(string name)
        {
            UserMaster userMaster = new UserMaster();
            using (var context = new InvoiceGenEntities())
            {
                userMaster = (from a in context.UserMasters
                              where a.UserName == name
                              select a).FirstOrDefault();
            }
            return userMaster;
        }

        public UserMaster GetUserdetailsByUsernamePass(string username, string passWord)
        {
            UserMaster userMaster = new UserMaster();
            using (var context = new InvoiceGenEntities())
            {
                userMaster = (from a in context.UserMasters
                              where a.UserName == username && a.password == passWord
                              select a).FirstOrDefault();
            }
            return userMaster;
        }

        public string GetUserRole(long userID)
        {
            string role = string.Empty;
            using (var context = new InvoiceGenEntities())
            {
                var userRoleMapping = (from a in context.UserRoleMappings
                                       where a.UserId == userID
                                       select a).FirstOrDefault();

                var rolemaster = (from a in context.RoleMasters
                                  where a.ID == userRoleMapping.RoleId
                                  select a).FirstOrDefault();
                if (rolemaster != null)
                {
                    role = rolemaster.RoleName;
                }
            }
            return role;
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

        public State GetStateByGSTIN(string gstin)
        {
            State state = new State();
            var query = "select s.Name,s.ID,null as country_id from GstStateCodes gst inner join States s on s.ID = gst.StateID where gst.GSTStateCode =";
            query += Convert.ToString(gstin) + " ";
            using (var context = new InvoiceGenEntities())
            {
                state = context.States
                        .SqlQuery(query)
                        .FirstOrDefault();
            }
            return state;
        }

        public string GetStateNameByID(long? stateID)
        {
            State state = new State();
            using (var context = new InvoiceGenEntities())
            {
                state = (from a in context.States
                         where a.ID == stateID
                         select a).FirstOrDefault();
            }
            if (state != null)
            {
                return state.Name;
            }
            else
            {
                return string.Format("State with ID:{0} Not Found !", stateID);
            }
        }

        public string GetCityNameByID(long? cityID)
        {
            City city = new City();
            using (var context = new InvoiceGenEntities())
            {
                city = (from a in context.Cities
                        where a.ID == cityID
                        select a).FirstOrDefault();
            }
            if (city != null)
            {
                return city.Name;
            }
            else
            {
                return string.Format("City with ID:{0} Not Found !", cityID);
            }
        }

        public UserMaster GetUserdetailsByUsernameAndRole(string name, string userRole)
        {
            UserMaster userMaster = new UserMaster();
            using (var context = new InvoiceGenEntities())
            {
                userMaster = (from a in context.UserMasters
                              where a.UserName == name
                              select a).FirstOrDefault();

                var userRoleMapping = (from a in context.UserRoleMappings
                                       where a.UserId == userMaster.ID
                                       select a).FirstOrDefault();


                var rolemaster = (from a in context.RoleMasters
                                  where a.ID == userRoleMapping.RoleId
                                  select a).FirstOrDefault();

                if (string.Equals(rolemaster.RoleName, userRole, StringComparison.InvariantCultureIgnoreCase))
                {
                    return userMaster;
                }
                else
                {
                    return null;
                }
            }

        }

        public List<Operations> GetOperationListForUser(long userId)
        {
            List<Operations> opList = new List<Operations>();
            using (var context = new InvoiceGenEntities())
            {
                var userRoleMapping = (from a in context.UserRoleMappings
                                       where a.UserId == userId
                                       select a).FirstOrDefault();

                var rolemaster = (from a in context.RoleMasters
                                  where a.ID == userRoleMapping.RoleId
                                  select a).FirstOrDefault();
                List<RoleOperationMapping> roleOpMapping = new List<RoleOperationMapping>();
                roleOpMapping = (from a in context.RoleOperationMappings
                                 where a.RoleId == userRoleMapping.RoleId
                                 select a).ToList();
                foreach (RoleOperationMapping opMapp in roleOpMapping)
                {
                    Operations op = new Operations();
                    OperationMaster opm = new OperationMaster();
                    opm = (from a in context.OperationMasters
                           where a.ID == opMapp.OperationId
                           select a).FirstOrDefault();

                    if (opm != null)
                    {
                        op.OperationName = opm.OperationName;
                        opList.Add(op);
                    }
                }

            }
            return opList;
        }
    }
}

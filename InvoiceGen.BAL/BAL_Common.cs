using InvoiceGen.DAL;
using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public UserMaster GetUserdetailsByUsernamePass(string username, string passWord)
        {
            DAL_Common dAL_Common = new DAL_Common();
            return dAL_Common.GetUserdetailsByUsernamePass(username, passWord);
        }

        public string GetUserRole(long userID)
        {
            DAL_Common dAL_Common = new DAL_Common();
            return dAL_Common.GetUserRole(userID);
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


        public State GetStateByGSTIN(string gstin)
        {
            DAL_Common dAL_Common = new DAL_Common();
            return dAL_Common.GetStateByGSTIN(gstin);
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


        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}

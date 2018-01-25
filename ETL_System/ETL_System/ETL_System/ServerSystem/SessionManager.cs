using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ETL_System
{
    public class SessionManager    {
        //====================== FIELDS
        private static Dictionary<string, User> sessions_table;



        //=======================CONSTRUCTORS



        //====================== METHODS
        public void validateLogin() {
          
        }

        public void validateSession() {

        }

        public void logout() {

        }

        public void sessionsHousekeeping() {

        }

        public int? getUserIdForLogin(string login) {
            return sessions_table[login].user_id;
        }
    }

    public struct User {
        public int? user_id;
        public string login;
        public string this_session_id;
        public DateTime login_timestamp;                
    }
}

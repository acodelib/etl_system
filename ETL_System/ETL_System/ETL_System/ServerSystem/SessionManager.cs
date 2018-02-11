using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data;

namespace ETL_System
{
    public class SessionManager    {
        //====================== FIELDS
        private Dictionary<Guid, User> sessions_table;
        private CoreDB data_layer;


        //=======================CONSTRUCTORS
        public SessionManager(CoreDB data) {
            this.sessions_table = new Dictionary<Guid, User>();
            this.data_layer = data;
        }


        //====================== METHODS
        public Message validateLogin(Message msg) {
            //look into the message body for login and pass
            string[] message_parts = msg.body.Split(';');
            string user_name    = message_parts[0].Split(':')[1];
            string pass         = message_parts[1].Split(':')[1];
            DataRow r;
            Message result;
            User u;
            Guid session;
                       
            r = this.data_layer.validateUserLineExists(user_name, pass);

            if (r != null) {
                session = Guid.NewGuid();
                u = new User() {
                    user_id = (int)r["user_id"],
                    role = SystemSharedData.user_roles[(int)r["role_id"]],
                    login = (string)r["login"],
                    login_timestamp = DateTime.Now,
                    this_sessions_id = session
                };
                sessions_table.Add(u.this_sessions_id, u);
                result = new Message();
                result.msg_type = MsgTypes.REPLY_SUCCESS;
                result.body = "Loggin success";
                result.header["user"] = u;
                return result;
            }            
            return null;
        }

        public void validateSession() {

        }

        public void logout() {

        }

        public void sessionsHousekeeping() {

        }

        public int? getUserIdForLogin(Guid session_id) {
            return sessions_table[session_id].user_id;
        }
    }

 
}

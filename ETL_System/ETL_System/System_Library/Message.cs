using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ETL_System
{
    [Serializable]
    public class Message {

        //-----------FIELDS
        public System.Guid  id;
        



        public MsgTypes         msg_type;

        public System.Guid      session_channel;
        public int              sys_change_id { get; set; }        
        public DateTime         timestamp { get; set; }
        public MsgAttachment    attachement;
        public Hashtable        header;
        public string           body;

        //------------CONSTRUCTORS
        public Message(MsgAttachment attch) {
            this.id = Guid.NewGuid();
            this.attachement = attch;
            header = new Hashtable();
            timestamp = DateTime.Now;
        }
        
        public Message() {
            this.id = Guid.NewGuid();
            timestamp = DateTime.Now;
            header = new Hashtable();
        }        

        //------------METHODS
        public byte[] encodeToBytes() {
            MemoryStream m = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(m, this);
            m.Position = 0;
            return m.ToArray();
        }

        public void addAttachement(MsgAttachment att) {
            this.attachement = att;
        }

        public MsgAttachment getAttachement() {
            return this.attachement;
        }

        public static Message getMessageFromBytes(byte[] data) {
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream m = new MemoryStream(data);            
            m.Position = 0;
            return (Message)b.Deserialize(m);
        }
        

    }

    public enum MsgTypes {
        TRY_CONNECT = 0,        
        REQUEST_JOB = 1,
        REQUEST_JOB_CATALOGUE_DISPLAY = 2,
        REQUEST_JOB_QUEUE_DISPLAY = 3,
        REQUEST_DEPENDENCY_CATALOGUE = 4,
        MGMT_CREATE_JOB = 5,
        MGMT_DELETE_JOB = 6,
        MGMT_UPDATE_JOB = 7,
        COMMAND_PAUSE_JOB = 8,
        COMMAND_RESTART_JOB = 9,         
        COMMAND_HALT_JOB = 10,
        COMMAND_SYSTEM_PAUSE = 11,
        COMMAND_SYSTEM_HALT = 12,
        REPLY_SUCCESS = 13,
        REPLY_FAIL = 14,
        SERVER_ERROR = 15,
        SERVER_WARNING = 16
    }
      

    public struct MsgHeader {
        public Dictionary<int, ScheduleType> schedule_types;
        public Dictionary<int, DependencyType> dependency_types;
        public Dictionary<int, string> user_roles;
    }
}


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
        private System.Guid  _id;
        public System.Guid   id { get { return _id; } }

        public MsgTypes         msg_type;
        
        public int              sys_change_id { get; set; }        
        public DateTime         timestamp { get; set; }
        private MsgAttachment   attachement;
        public Hashtable        header;
        public string           body;

        //------------CONSTRUCTORS
        public Message(MsgAttachment attch) {
            this._id = new Guid();
            this.attachement = attch;
            timestamp = DateTime.Now;
        }
        
        public Message() {
            this._id = new Guid();
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

        public static Message getMessageFromBytes(byte[] data) {
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream m = new MemoryStream(data);
            return (Message)b.Deserialize(m);
        }
        

    }

    public enum MsgTypes {
        TRY_CONNECT,
        SERVER_ERROR,
        SERVER_WARNING,
        REQUEST_JOB,
        REQUEST_JOB_CATALOGUE_DISPLAY,
        REQUEST_JOB_QUEUE_DISPLAY,
        REQUEST_DEPENDENCY_CATALOGUE,
        MGMT_CREATE_JOB,
        MGMT_DELETE_JOB,
        MGMT_UPDATE_JOB,
        COMMAND_PAUSE_JOB,
        COMMAND_RESTART_JOB,        
        COMMAND_HALT_JOB,
        COMMAND_SYSTEM_PAUSE,
        COMMAND_SYSTEM_HALT,
        REPLY_SUCCESS        
    }

   

    public struct MsgHeader {
        public Dictionary<int, ScheduleType> schedule_types;
        public Dictionary<int, DependencyType> dependency_types;
        public Dictionary<int, string> user_roles;
    }
}


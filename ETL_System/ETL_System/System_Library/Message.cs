using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System
{
    public class Message {

        //-----------FIELDS
        private System.Guid  _id;
        public System.Guid   id { get { return _id; } }

        public MsgTypes      msg_type;
        
        public int           sys_change_id { get; set; }        
        public DateTime      timestamp { get; set; }
        public MsgAttachment attachement { get; }
        public string        body;

        //------------CONSTRUCTORS
        public Message(MsgAttachment attch) {
            this._id = new Guid();
            this.attachement = attch;
        }
        
        //------------METHODS
        public byte encodeToBytes() {
            return (byte)0;
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
}


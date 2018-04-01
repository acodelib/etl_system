using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ETL_System
{
    

    public class CommsManager {

        Socket listener_socket;
        SessionManager session_manager;
        SystemManager system_manager;

        public CommsManager(SystemManager sys_mng,SessionManager sm) {
            this.system_manager = sys_mng;
            this.session_manager = sm;
            LogManager.writeStartEvent("Comms Manager Starting...", system_manager.path_to_log);

            listener_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(this.getIp4Address()), 6868);
            this.listener_socket.Bind(ip);           

            Thread ListenThread = new Thread(this.listenToClients);

            ListenThread.Start();
            LogManager.writeStartEvent("Server is awaiting connections on IP: " + getIp4Address() + ":6868", system_manager.path_to_log);

        }    
        
        private void listenToClients() {
            while (true) {                
                this.listener_socket.Listen(5);
                handleMessageFromClient(listener_socket.Accept());
            }
        }

        private void handleMessageFromClient(Socket client_socket) {            
            Thread individual_client_thread = new Thread(this.decodeNetMessage);
            individual_client_thread.Start(client_socket);
        }

        public void decodeNetMessage(object incoming_socket) {
            Socket client_socket = (Socket)incoming_socket;
            client_socket.SendBufferSize = 3200000;
            byte[] Buffer;
            int readBytes;
            Message message;
            Message reply;
            while (1 == 1) {
                try {
                    Buffer = new byte[client_socket.SendBufferSize];
                    readBytes = client_socket.Receive(Buffer);
                    if (readBytes > 0) {
                        //read inpute stream as Message:
                        BinaryFormatter b   = new BinaryFormatter();
                        MemoryStream m      = new MemoryStream(Buffer);
                        message             = (Message)b.Deserialize(m);
                        Console.WriteLine("Received Message: " + message.id.ToString());

                        //message received, handle it if its a connection request:
                        if (message.msg_type == MsgTypes.TRY_CONNECT) {
                            reply = session_manager.validateLogin(message);

                            reply.header["jobs_list"]           = this.system_manager.pipeTasksRawList();
                            reply.header["schedule_types"]      = SystemSharedData.schedule_types;
                            reply.header["dependency_types"]    = SystemSharedData.dependency_types;
                            reply.header["user_roles"]          = SystemSharedData.user_roles;
                            reply.header["catalogue_scan_flag"] = SystemSharedData.catalogue_scan_flag;
                            reply.header["workers_start_flag"] = SystemSharedData.workers_start_flag;

                            client_socket.Send(reply.encodeToBytes());
                        }
                        else if(message.msg_type == MsgTypes.TRY_DISCONECT && session_manager.validateSession(message.session_channel)) {
                            session_manager.clearSessionOnLogout(message.session_channel);
                            reply = new Message();
                            reply.msg_type = MsgTypes.REPLY_SUCCESS;
                            client_socket.Send(reply.encodeToBytes());
                        }
                        //else validate the session and let the System manager resolve it
                        else {
                            if (session_manager.validateSession(message.session_channel)) {
                                reply = this.system_manager.handleClientMessage(message);
                                reply.header["catalogue_scan_flag"] = SystemSharedData.catalogue_scan_flag;
                                reply.header["workers_start_flag"] = SystemSharedData.workers_start_flag;
                                client_socket.Send(reply.encodeToBytes());
                            }
                        }

                        //client_socket.Close();
                        Buffer = null;
                        break;
                    }
                }
                catch (SocketException ex) {
                    Console.WriteLine("Client Disconnected. Error message: " + ex.Message.ToString());
                    break;
                }
            }

        }
        
        public string getIp4Address() {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
           //return "192.168.0.179";
            foreach (IPAddress i in ips) {
                if (i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    return i.ToString();
                }
            }           
            return "127.0.0.1";
        }

        public string getMACAddress() {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String mac_addr = string.Empty;
            foreach (NetworkInterface adapter in nics) {

                // only return MAC Address from first card  
                if (mac_addr == String.Empty)  {
                    
                    mac_addr = adapter.GetPhysicalAddress().ToString();
                }
            }
            return mac_addr;
        }
    }
}

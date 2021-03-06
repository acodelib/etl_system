﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ETL_System {

    public class MsgEngine {
        private Socket master_socket;
        private IPEndPoint ipe;
        public string return_message;
        public string send_message;
        public int port;

        public void initialiseMsgEngine(string IP, int the_port) {
            Console.WriteLine("Messaging Engine booting...");
            string ip = IP;            
            this.port = the_port;
            ipe = new IPEndPoint(IPAddress.Parse(ip), port);                 
        }

        public byte[] sendMessageAndListenForReply(byte[] byte_message) {
            //first send message
            try { 
                master_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                master_socket.SendBufferSize = 3200000;
                master_socket.Connect(ipe);
                master_socket.Send(byte_message);        

            //then listen for reply
                byte[] buffer;
                int incoming_bytes;

                while (1 == 1) {
                    buffer = new byte[master_socket.SendBufferSize];
                    incoming_bytes = master_socket.Receive(buffer);
                    if (incoming_bytes > 0) {                      
                            master_socket.Disconnect(false);  // for safe network comms
                            return buffer;    
                        }
                    }
            }catch (Exception e) {
                MessageBox.Show("Comms error: " + e.Message);              
                return null;
            }  
        }

        public void tryConnect(string IP, int port) {
            //first attempt connection to server socket
            try {
                string ip = IP;
                master_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ipe = new IPEndPoint(IPAddress.Parse(ip), port);                
                master_socket.Connect(ipe);
                master_socket.Disconnect(false);
                MessageBox.Show("Server successfully found on the network.");
                master_socket.Dispose();
            }
            catch(Exception e) {
                MessageBox.Show($"Could not find server, make sure IP and Port number are correct.\n Original system error: {e.Message}");
            }
        }
    }   


       
    }

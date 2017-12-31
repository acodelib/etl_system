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

namespace ETL_System
{
    

    public class CommsManager {

        Socket listener_socket;

        public CommsManager(ref ConcurrentDictionary<string,string> clients_table) {
            Console.WriteLine("ETL Server Starting...");
            listener_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(this.getIp4Address()), 6868);
            this.listener_socket.Bind(ip);

            Thread ListenThread = new Thread(this.listenToClients);

            ListenThread.Start();
            Console.WriteLine("Server is awaiting connections on IP: " + getIp4Address() + ":6868");            

        }    
        
        private void listenToClients() {
            while (true) {
                
                this.listener_socket.Listen(0);
                handleMessageFromClient(listener_socket.Accept());
            }
        }

        private void handleMessageFromClient(Socket client_socket) {            
            Thread individual_client_thread = new Thread(this.decodeNetMessage);
            individual_client_thread.Start(client_socket);
        }

        public void decodeNetMessage(object incoming_socket) {
            Socket client_socket = (Socket)incoming_socket;
            byte[] Buffer;
            int readBytes;
            StringBuilder str_message = new StringBuilder();
            while (1 == 1) {
                try {
                    Buffer = new byte[client_socket.SendBufferSize];
                    readBytes = client_socket.Receive(Buffer);
                    if (readBytes > 0) {
                        str_message.Clear();
                        str_message.Append(Encoding.ASCII.GetString(Buffer, 0, readBytes));
                        string content = str_message.ToString();
                        Console.WriteLine("Received Message: " + content);

                        //reply
                        client_socket.Send(Encoding.ASCII.GetBytes("Receivend your message: " + content));
                        str_message = null;
                        content = null;
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

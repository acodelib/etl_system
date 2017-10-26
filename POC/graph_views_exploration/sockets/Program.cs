using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace sockets
{
    class Program
    {
        static Socket listenerSocket;
        static List<string> clients;

        static void Main(string[] args) {
            Console.WriteLine("This mac address: {0}", Program.getMACAddress());
            Console.WriteLine("This IP address : {0}", Program.getIp4Address());
            
            
            Console.WriteLine("Starting Server...");
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<string>();

            
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(Program.getIp4Address()), 6969);
            listenerSocket.Bind(ip);

            Thread listenThread = new Thread(Program.ListenThread);            
            listenThread.Start();
            Console.WriteLine("Success... Listening IP: " + getIp4Address() + ":6969");
            Console.ReadLine();

        }

        
        static void ListenThread() {
            while(true) {
                listenerSocket.Listen(0);
                Program.handleMessage(listenerSocket.Accept());
            }
        }

        static void handleMessage(Socket client_socket) {
            Thread clientThread = new Thread(Program.decodeMessage);
            clientThread.Start(client_socket);
        }

        static void decodeMessage(object incoming_socket) {
            Socket client_socket = (Socket)incoming_socket;
            byte[] Buffer;
            int readBytes;
            StringBuilder str_message = new StringBuilder();
            while (1 == 1) {
                try {
                    Buffer = new byte[client_socket.SendBufferSize];
                    readBytes = client_socket.Receive(Buffer);
                    if (readBytes > 0) {
                        BinaryFormatter bf = new BinaryFormatter();
                        MemoryStream ms = new MemoryStream(Buffer);
                        str_message.Append(Encoding.ASCII.GetString(Buffer,0,readBytes));
                        string content = str_message.ToString();
                        Console.WriteLine("Received Message: " + content);
                        //c.clientSocket.Send(p.toBytes());
                    }
                }
                catch (SocketException ex) {
                    
                    Console.WriteLine("Client Disconnected.");
                }
            }

        }

        /*
        public static void Data_IN(object cSocket) {
            Socket clientSocket = (Socket)cSocket;

            byte[] Buffer;
            int readBytes;

            for (;;) {
                try {
                    Buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(Buffer); //  https://www.youtube.com/watch?v=X66hFZG5p3A

                    if (readBytes > 0) {
                        Packet p = new Packet(Buffer);
                        dataManager(p);
                    }
                }
                catch (SocketException ex) {
                    Console.WriteLine("Client Disconnected.");
                }
            }
        }

        public static void dataManager(Packet p) {
            switch (p.packetType) {
                case PacketType.chat:
                    foreach (ClientData c in _clients) {
                        c.clientSocket.Send(p.toBytes());
                    }
                    break;
            }
        }

        class ClientData
        {
            public Socket clientSocket;
            public Thread clientThread;
            public string id;

            public ClientData() {
                id = Guid.NewGuid().ToString();
                clientThread = new Thread(Server.Data_IN);
                clientThread.Start(clientSocket);
                sendRegistrationPacket();
            }

            public ClientData(Socket clientSocket) {
                this.clientSocket = clientSocket;
                id = Guid.NewGuid().ToString();
                clientThread = new Thread(Server.Data_IN);
                clientThread.Start(clientSocket);
                sendRegistrationPacket();
            }

            public void sendRegistrationPacket() {
                Packet p = new Packet(PacketType.Registration, "server");
                p.Gdata.Add(id);
                clientSocket.Send(p.toBytes());


            }
        }

     */
        public static string getIp4Address() {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress i in ips) {
                if (i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    return i.ToString();
                }
            }
            return "127.0.0.1";
        }

        public static string getMACAddress() {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics) {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
    }
}

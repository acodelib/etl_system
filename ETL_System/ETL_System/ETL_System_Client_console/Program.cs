
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ETL_System_Client_console {
    class Program {
        static Socket master;
        static void Main(string[] args) {
            Console.WriteLine("Enter Your Name: ");
            //name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Gimme ip");
            string ip = "192.168.56.1";

            Program.master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 6969);

            try {
                master.Connect(ipe);
            }
            catch {
                Console.WriteLine("Could not connect to server");
            }

            Thread t = new Thread(Program.handrelServerReply);
            t.Start();

            while (1 == 1) {
                Console.Write("::>");
                string input = Console.ReadLine();
                byte[] byteData = Encoding.ASCII.GetBytes(input);
                master.Send(byteData);
            }
        }
        public static void handrelServerReply(object socket) {
            //var server_socket = Program.master;//(Socket)socket;

            byte[] Buffer;
            int readBytes;
            StringBuilder str_message = new StringBuilder();
            while (1 == 1) {
                try {
                    Buffer = new byte[master.SendBufferSize];
                    readBytes = master.Receive(Buffer);
                    if (readBytes > 0) {
                        //BinaryFormatter bf = new BinaryFormatter();
                        //MemoryStream ms = new MemoryStream(Buffer);

                        str_message.Append(Encoding.ASCII.GetString(Buffer, 0, readBytes));
                        string content = str_message.ToString();
                        Console.WriteLine("Server: " + content);

                        //reply
                        //client_socket.Send(Encoding.ASCII.GetBytes("Receivend your message;"));
                    }
                }
                catch (SocketException ex) {
                    Console.WriteLine("Client Disconnected.");
                    break;
                }

            }

        }

    }
}

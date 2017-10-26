using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Enter Your Name: ");
            //name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Gimme ip");
            string ip = "192.168.56.1";

            Socket master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 6969);

            try {
                master.Connect(ipe);
            }
            catch {
                Console.WriteLine("Could not connect to server");
            }            

            //Thread t = new Thread(data_IN);
            //t.Start();

            while (1 == 1 ) {
                Console.Write("::>");
                string input = Console.ReadLine();
                byte[] byteData = Encoding.ASCII.GetBytes(input);

                master.Send(byteData);
            }
        }
    }
}

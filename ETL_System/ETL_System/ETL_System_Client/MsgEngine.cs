using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ETL_System_Client {

    public class MsgEngine {
        private Socket master;
        private IPEndPoint ipe;
        public string return_message;
        public string send_message;

        public MsgEngine(string message) {

            Console.WriteLine("Gimme ip");
            string ip = "192.168.56.1";

            this.send_message = message;

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipe = new IPEndPoint(IPAddress.Parse(ip), 6868);

            //first attempt connection to server socket
            try {
                master.Connect(ipe);
            }
            catch {
                MessageBox.Show("Could not connect to server");
            }
        }


       
        public Task<string> communicateWithServer() {
            return Task.Run(() => {
                byte[] Buffer;
                int readBytes;
                StringBuilder str_message = new StringBuilder();
                byte[] byteData = Encoding.ASCII.GetBytes(this.send_message);
                master.Send(byteData);
                while (1 == 1) {
                    try {
                        Buffer = new byte[master.SendBufferSize];
                        readBytes = master.Receive(Buffer);

                        //read response
                        if (readBytes > 0) {
                            //BinaryFormatter bf = new BinaryFormatter();
                            //MemoryStream ms = new MemoryStream(Buffer);
                            str_message.Append(Encoding.ASCII.GetString(Buffer, 0, readBytes));
                            this.return_message = str_message.ToString();
                            break;
                        }
                    }
                    catch (SocketException ex) {
                        Console.WriteLine("Client Disconnected.");
                        break;
                    }

                }
                master.Disconnect(false);
                return return_message;
            });
        }
    }
       
}

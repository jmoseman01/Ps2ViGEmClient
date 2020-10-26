using System.Threading;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace ConsoleApp1
{
    class Program
    {
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[3];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[6].MapToIPv4();

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 10000);
            Console.WriteLine("Text received : {0}", localEndPoint.Address);

            ViGEmClient client = new ViGEmClient();

            IXbox360Controller controller = client.CreateXbox360Controller();

            controller.Connect();
            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();
                while (true) { 
                    // Show the data on the console.  
                    handler.Receive(bytes);
                    string[] mybytes = { bytes[0].ToString(), bytes[1].ToString(), bytes[2].ToString() };
                    Console.WriteLine("Text received : {0} {1} {2}", mybytes);
                    
                    // up
                    if (bytes[0].ToString().Equals("16")) {
                        controller.SetButtonState(Xbox360Button.Up, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.Up, false);
                    }
                    // right
                    if (bytes[0].ToString().Equals("32"))
                    {
                        controller.SetButtonState(Xbox360Button.Right, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.Right, false);
                    }
                    // left
                    if (bytes[0].ToString().Equals("128"))
                    {
                        controller.SetButtonState(Xbox360Button.Left, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.Left, false);
                    }
                    // down
                    if (bytes[0].ToString().Equals("64"))
                    {
                        controller.SetButtonState(Xbox360Button.Down, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.Down, false);
                    }
                    // X
                    if (bytes[1].ToString().Equals("64"))
                    {
                        controller.SetButtonState(Xbox360Button.A, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.A, false);
                    }
                    // circle
                    if (bytes[1].ToString().Equals("32"))
                    {
                        controller.SetButtonState(Xbox360Button.B, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.B, false);
                    }
                    // triangle
                    if (bytes[1].ToString().Equals("16"))
                    {
                        controller.SetButtonState(Xbox360Button.Y, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.Y, false);
                    }
                    // square
                    if (bytes[1].ToString().Equals("128"))
                    {
                        controller.SetButtonState(Xbox360Button.X, true);
                        Thread.Sleep(10);
                        controller.SetButtonState(Xbox360Button.X, false);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }
}

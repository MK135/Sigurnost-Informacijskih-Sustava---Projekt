//--------------- Server.cs ---------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace server_log_bomba
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(9000);
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server pokrenut");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Prihvaćam konekciju klijenta");
            requestCount = 0;

            do
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[100025];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Podaci od klijenta >> " + dataFromClient);
                }
                catch (Exception ex)
                {
                    requestCount = 0;
                    Console.WriteLine("\n\n\n  NITI JEDNA APLIKACIJA NIJE SPOJENA NA SERVER.");
                    Console.WriteLine("\n\n  GREŠKA: " + ex.ToString());
                    Console.WriteLine("\n\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey(true);
                }

            } while (requestCount != 0);
        }
    }
}

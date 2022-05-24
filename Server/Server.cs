using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private static TcpListener _tcpListener;
        private static List<TcpClient> _clients = new List<TcpClient>();
        private static byte[] _data = new byte[128];
        private int _port;
        public Server(int port)
        {
            _port = port;
        }

        public void ServerSertUp()
        {
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptClients), null);
        }

        public static void AcceptClients(IAsyncResult asyncResult)
        {
            TcpClient client;
            try
            {
                client = _tcpListener.EndAcceptTcpClient(asyncResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            _clients.Add(client);
            client.Client.Send(Encoding.ASCII.GetBytes("HI"));
            client.Client.BeginReceive(_data, 0, _data.Length, SocketFlags.None, new AsyncCallback(ReciveUserInput), client);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptClients), null);
        }

        public static void ReciveUserInput(IAsyncResult asyncResult)
        {
            TcpClient client = asyncResult.AsyncState as TcpClient;
            int reciever;
            try
            {
                reciever = client.Client.EndReceive(asyncResult);
                if (reciever == 0)
                {
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    _clients.Remove(client);
                    return;
                }
            }
            catch (Exception ex)
            {
                client.Client.Shutdown(SocketShutdown.Both);
                client.Client.Close();
                _clients.Remove(client);
                throw;
            }
            client.Client.BeginReceive(_data, 0, _data.Length, SocketFlags.None, new AsyncCallback(ReciveUserInput), client);

        }
    }
}

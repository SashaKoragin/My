using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDocumentGeneratorTestsTemplate.Sntp
{
    public class Sntp
    {

        public void TestSntp()
        {
            const string ntpServer = "";

            var ntpData = new byte[48];
            ntpData[0] = 0x1b;

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            var ipEndPoint = new IPEndPoint(addresses[0],123);

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);
                socket.ReceiveTimeout = 300;
                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }
        }
    }
}

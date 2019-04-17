using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MServer.Client;
using MServer.Medium;

namespace ConsoleApp1
{
    class MyClient :My
    {

        private MyServerPeer peer;

        public override void Start()
        {
            peer = new MyServerPeer(OnProcessData, MyApp.IP, MyApp.PROT);
            peer.DebugApply = Print;
            peer.Connect();
        }

        void Print(OperationRequest ss) {

            Console.WriteLine(ss.Parameters);
        }

        void OnProcessData(OperationRequest oper) {

            Console.WriteLine(oper.OperationCode +" " +oper.Parameters);
        }

        public override void Update()
        {
            if (Input.IsText("123"))
            {
                Console.WriteLine("6666666666666");
            }
            if (Input.IsText("q"))
            {
                peer.DisConnect();
                MyApp.ISRUN = false;

                Console.ReadKey();
            }
            if (Input.IsText("close client"))
            {
                peer.DisConnect();
            }
            if (Input.IsText("connect server"))
            {
                if (!peer.SocketClient.Connected)
                {
                    peer.Connect();
                }
            }
            if (Input.IsText("s"))
            {
                peer.SendRequest(new OperationRequest(101, "我是来自客户端的信息" + DateTime.Now.ToString("hh:mm:ss ms")));
            }
            if (Input.IsText("status"))
            {
                Console.WriteLine("peerSocket," + peer.SocketClient);
                if (peer.SocketClient != null && peer.SocketClient.Connected)
                {
                    Console.WriteLine("Connected,true");
                    Console.WriteLine("LocalEndPoint," + peer.SocketClient.LocalEndPoint);
                    Console.WriteLine("RemoteEndPoint," + peer.SocketClient.RemoteEndPoint);
                }
                else
                {
                    Console.WriteLine("Connected,false");
                }
            }
        }
        public override void Destroy()
        {
            Console.WriteLine("MyClient 已销毁");
        }
    }
}

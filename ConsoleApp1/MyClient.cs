using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MServer.Client;
using MServer.Medium;

namespace ConsoleApp1
{
    class MyClient :My
    {

        private bool isGetFile = false;
        private FileStream file = null;


        private MyServerPeer peer;

        public override void Start()
        {
            peer = new MyServerPeer(OnProcessData,MyApp.IP, MyApp.PROT);
            peer.DebugApply = Print;
            peer.Connect();
        }

        void Print(OperationRequest ss) {

            Console.WriteLine(ss.Parameters);
        }

        void OnProcessData(OperationRequest oper) {

            //Console.WriteLine("OnProcessData="+oper.OperationCode +" " +oper.Parameters);

            switch (oper.OperationCode)
            {
                case SendCode.SEND_FILE_END:
                    if (isGetFile && file != null)
                    {

                        file.Close();
                        isGetFile = false;
                        file = null;
                        if (SendCode.Equals(oper.Parameters, SendCode.SUCCEED))
                            Console.WriteLine("文件保存成功!!");
                        else
                            Console.WriteLine("文件保存失败!!");
                    }
                    break;
                case SendCode.SEND_FILE_DATA:
                    if (isGetFile && file != null)
                    {
                        file.Write(oper.operationData, 0, oper.operationData.Length);
                    }
                    break;
                case SendCode.SEND_FILE_HEAD:
                    string[] mess = oper.Parameters.Split('|');
                    string fileName = mess[0];

                    //Console.WriteLine("开始接收文件 [Name," + fileName+"][Size,"+ mess[1]+"]");
                    file = File.Create(AppDomain.CurrentDomain.RelativeSearchPath + fileName);
                    isGetFile = true;
                    break;
                case 104:
                    string[] mss = oper.Parameters.Split('|');

                    Console.WriteLine("房间消息["+ mss[0]+","+mss[1]+"]");
                    break;
                case 10100:
                    Console.WriteLine( SendCode.Equals(oper.Parameters,SendCode.SUCCEED) ?"房间创建成功":"房间创建失败");
                    break;
                case 10200:
                    Console.WriteLine(SendCode.Equals(oper.Parameters, SendCode.SUCCEED) ? "房间加入成功" : "房间加入失败");
                    break;
                case 10500:
                    Console.WriteLine("房间信息:"+ oper.Parameters);
                    break;
                default:
                    break;
            }
        }

        string room = "";
        public override void Update()
        {
            if (Input.GetCommand("room"))
            {
                room = Input.ComText;
            }
            if (Input.GetCommand("code")) {

                if (room != "")
                {
                    int code = int.Parse(Input.ComText);
                    peer.SendRequest(new OperationRequest(code, room));
                }
                else
                    Console.WriteLine("房间名称为空");
            }
            if (Input.Equals("sendRoomF"))
            {
                FileStream file = new FileStream("G:/B02_BOOK/Z_01_成长/代码整洁之道.pdf", FileMode.Open);
                int fileLenght = (int)file.Length;

                peer.SendRequest(new OperationRequest(SendCode.SEND_FILE_HEAD, "代码整洁之道.pdf|" + fileLenght, SendType.File));

                byte[] sendData = new byte[OperationRequest.DataSize];
                int curIndex = 0;
                while (curIndex < fileLenght)
                {

                    int rendLength = curIndex + sendData.Length > fileLenght ? (fileLenght - curIndex) : sendData.Length;

                    curIndex += file.Read(sendData, 0, rendLength);
                    peer.SendRequest(new OperationRequest(SendCode.SEND_FILE_DATA,sendData));

                    //Console.WriteLine("sendRoomF" + curIndex + " " + fileLenght);
                }
                file.Close();
                Console.WriteLine("文件提交完毕");
            }
            if (Input.GetCommand("sendRoomS"))
            {
                peer.SendRequest(new OperationRequest(104, peer.SocketClient.LocalEndPoint +"|"+ Input.ComText));
            }
            if (Input.Equals("roomStatus"))
            {
                peer.SendRequest(new OperationRequest(105, room));
            }
            if (Input.Equals("123"))
            {
                Console.WriteLine("6666666666666");
            }
            if (Input.Equals("q"))
            {
                peer.DisConnect();
                MyApp.ISRUN = false;

                System.Threading.Thread.Sleep(1000);
            }
            if (Input.Equals("close client"))
            {
                peer.DisConnect();
            }
            if (Input.Equals("connect server"))
            {
                if (!peer.SocketClient.Connected)
                {
                    peer.Connect();
                }
            }
            if (Input.Equals("file"))
            {
            }
            if (Input.Equals("S"))
            {
                #region S
                string S = "" +
                    "\n始啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" +
                    "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊终";
                #endregion

                peer.SendRequest(new OperationRequest(104, S));
            }
            
            if (Input.GetCommand("sendServer"))
            {
                peer.SendRequest(new OperationRequest(1, Input.ComText));
            }
            if (Input.Equals("status"))
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

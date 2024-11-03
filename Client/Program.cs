using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 서버 IP 및 포트 설정
            string serverIp = "127.0.0.1";  // 서버 주소 (로컬 호스트)
            int port = 5000;  // 서버와 동일한 포트 번호

            // TCP 클라이언트 생성 및 서버 연결
            TcpClient client = new TcpClient();
            client.Connect(serverIp, port);
            Console.WriteLine("서버에 연결되었습니다.");

            // 네트워크 스트림 생성 (데이터 송수신을 위해 사용)
            NetworkStream stream = client.GetStream();

            // 서버로부터 메시지를 수신하기 위한 스레드 시작
            Thread receiveThread = new Thread(() => ReceiveMessage(stream));
            receiveThread.Start();

            // 클라이언트에서 서버로 메시지를 전송
            SendMessage(stream);
        }

        static void SendMessage(NetworkStream stream)
        {
            while (true)
            {
                // 메시지 입력
                Console.Write("클라이언트: ");
                string message = Console.ReadLine();

                // 메시지를 바이트 배열로 변환 후 전송
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        static void ReceiveMessage(NetworkStream stream)
        {
            while (true)
            {
                // 버퍼 생성 및 서버 메시지 수신
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);

                // 수신된 바이트를 문자열로 변환 후 출력
                string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                Console.WriteLine("서버: " + message);
            }
        }
    }
}
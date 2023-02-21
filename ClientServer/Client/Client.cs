using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        try
        {
            //IP adresi ve port numarasını tanımlama
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;

            //TcpClient oluşturma ve Servera bağlanma
            TcpClient client = new TcpClient();
            client.Connect(ipAddress, port);

            //NetworkStream
            NetworkStream stream = client.GetStream();

            //Server'a mesaj gönderme
            string message = "Hello";
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
            Console.WriteLine("Client: " + message);

            //Server'dan gelen veriyi okuma
            byte[] serverBuffer = new byte[1024];
            int serverBytesRead = stream.Read(serverBuffer, 0, serverBuffer.Length);
            string serverMessage = Encoding.ASCII.GetString(serverBuffer, 0, serverBytesRead);
            Console.WriteLine("Server: " + serverMessage);

            //TcpClient bağlantısını kapatma
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Hata", e);
        }

        Console.WriteLine("\n Client kapatıldı.");
        Console.ReadLine();
    }
}


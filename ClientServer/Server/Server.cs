using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpListener server = null;

        try
        {
            //IP adresi ve port numarası tanımlama
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;

            //TcpListener başlatma
            server = new TcpListener(ipAddress, port);
            server.Start();
            Console.WriteLine("Server başlatıldı.");

            //Client bağlantısını kabul etme
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client bağlandı.");

            //NetworkStream
            NetworkStream stream = client.GetStream();

            //Client'tan gelen veriyi okuma
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string clientMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Client: " + clientMessage);

            //Server'dan Client'a veri gönderme
            string serverMessage = clientMessage + DateTime.Now.ToString();
            byte[] message = Encoding.ASCII.GetBytes(serverMessage);
            stream.Write(message, 0, message.Length);
            Console.WriteLine("Server: " + serverMessage);

            // Client bağlantısını kapatma
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Hata", e);
        }
        finally
        {
            // TcpListener'i kapatma
            server.Stop();
        }

        Console.WriteLine("\nServer kapatıldı.");
        Console.ReadLine();
    }
}

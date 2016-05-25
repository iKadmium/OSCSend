using CommandLineParser.Arguments;
using Rug.Osc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSCSend
{
    class Program
    {
        static TimeSpan timeoutTime = new TimeSpan(0, 0, 5);
        static void Main(string[] args)
        {
            CommandLineParser.CommandLineParser parser = new CommandLineParser.CommandLineParser();
            parser.ShowUsageOnEmptyCommandline = true;
            parser.AcceptSlash = false;
            Options options = new Options();
            parser.ExtractArgumentAttributes(options);
            parser.ParseCommandLine(args);

            UdpClient client = new UdpClient();

            Console.WriteLine("Sending to " + options.Address);

            object argument = options.StringValue == null ? (object)options.FloatValue : options.StringValue;
            OscMessage message = new OscMessage(options.Address, argument);

            UdpClient server = new UdpClient();

            try
            {
                if(options.LocalPort.HasValue)
                {
                    server = new UdpClient(options.LocalPort.Value);
                }
                client.Send(message.ToByteArray(), message.SizeInBytes, options.Host, options.Port);
                Console.WriteLine("Sent.");
                if (options.LocalPort.HasValue)
                {
                    DateTime startedTime = DateTime.Now;
                    Console.Write("Waiting for response");
                    while(server.Available == 0 && (DateTime.Now - startedTime) < timeoutTime)
                    {
                        Thread.Sleep(1000);
                        Console.Write(".");
                    }
                    if(server.Available > 0)
                    {
                        IPEndPoint remote = null;
                        byte[] received = server.Receive(ref remote);
                        OscPacket reply = OscPacket.Read(received, received.Length);
                        Console.WriteLine(reply.ToString());
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Timed out");
                    }
                }
            }
            catch(SocketException e)
            {
                Console.Error.WriteLine(e.Message);
            }
            

        }
    }
}

using NLog;
using NLog.Config;
using NLog.Targets;
using SKYPE4COMLib;
using System;
using System.IO.Ports;


namespace SkypePlugin
{
    class Program
    {
        static NLog.ILogger Logger = LogManager.GetCurrentClassLogger();

        static Program()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message} ${exception:format=tostring}";
            var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);
            LogManager.Configuration = config;
        }

        static void Main(string[] args)
        {
            SerialPort serial;
            Camera camera;
            SkypeMediator skypeMediator;

            try
            {
                var com = "COM1";
                serial = new SerialPort(com, 9600);
                serial.Open();

                Logger.Info("Communication over {0} established", com);

                camera = new Camera(serial);

                var skype = new Skype();
                skype.Attach(8, true);
                Logger.Info("Attached to skype process");

                SkypeMediator mediator = new SkypeMediator(skype, camera);
                
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }

            Console.WriteLine("Press any key to terminate ...");
            Console.Read();
        }
    }
}

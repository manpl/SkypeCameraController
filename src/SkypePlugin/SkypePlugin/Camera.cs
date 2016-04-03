using NLog;
using System.IO.Ports;

namespace SkypePlugin
{
    class Camera
    {
        NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
        private SerialPort serial;
        private int moveByAngle = 20;

        public Camera(SerialPort serial)
        {
            this.serial = serial;
        }

        public void Up()
        {
            Logger.Debug("up by {0}", moveByAngle);
            serial.WriteLine("t" + moveByAngle);
        }

        public void Down()
        {
            Logger.Debug("down by {0}", moveByAngle);
            serial.WriteLine("t-" + moveByAngle);
        }

        public void Left()
        {
            Logger.Debug("left by {0}", moveByAngle);
            serial.WriteLine("p" + moveByAngle);
        }

        public void Right()
        {
            Logger.Debug("right by {0}", moveByAngle);
            serial.WriteLine("p-" + moveByAngle);
        }
    }
}

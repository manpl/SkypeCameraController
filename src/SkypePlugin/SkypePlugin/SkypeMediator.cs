using NLog;
using SKYPE4COMLib;

namespace SkypePlugin
{
    class SkypeMediator
    {
        private Skype skype;
        private Camera camera;
        private NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
        private SkypeSession sessionInControl;


        public SkypeMediator(Skype skype, Camera camera)
        {
            this.camera = camera;
            this.skype = skype;

            skype.MessageStatus += OnMessage;
        }

        private void OnMessage(ChatMessage msg, TChatMessageStatus status)
        {
            var body = msg.Body;
            var sender = msg.Sender;

            Logger.Debug("Message: {0} From: {1} Status: {2}", msg, sender, status);

            if (msg.Status == TChatMessageStatus.cmsReceived) msg.Seen = true;
            if (msg.Status == TChatMessageStatus.cmsRead)
            {
                if (body == "l") camera.Left();
                if (body == "r") camera.Right();
                if (body == "u") camera.Up();
                if (body == "d") camera.Down();
            };
        }
    }

    class SkypeSession
    {
        public string SkypeId { get; }

        public SkypeSession(string skypeId)
        {
            this.SkypeId = skypeId;
        }
    }
}

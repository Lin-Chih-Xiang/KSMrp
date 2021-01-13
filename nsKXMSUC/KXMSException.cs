using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsKXMSUC
{
    public class KXMSException : Exception
    {
        private string _Message;
        private string _MessageDetail;
        private enMessageType _MessageType;
        public KXMSException(string Message, string MessageDetail="", enMessageType MessageType = enMessageType.Warning )
        {
            _Message = Message;
            _MessageDetail = MessageDetail;
            _MessageType = MessageType;
        }
        public override string Message { get { return _Message; } }
        public string MessageDetail { get { return _MessageDetail; } }
        public enMessageType MessageType { get { return _MessageType; } }
    }
}

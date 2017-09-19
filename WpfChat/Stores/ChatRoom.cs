using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.Stores
{
    public enum RoomStatus
    {
        Invited,
        Joined
    }
    public class ChatRoom
    {
        public RoomStatus Status { get; set; }
        public string Name { get; internal set; }
        public string AddedBy { get; internal set; }
    }
}

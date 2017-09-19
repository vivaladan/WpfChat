using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Xmpp.Client;
using Sharp.Xmpp.Extensions;

namespace WpfChat.Stores
{
    public class RoomStore
    {
        private readonly IXmppClient _client;

        public RoomStore(IXmppClient client)
        {
            _client = client;

        }

        public void LoadRooms()
        {
            var roomInfos = _client.DiscoverRooms(new Sharp.Xmpp.Jid("chat.SCIIS1.dev.apdcomms.co.uk"));
            var rooms = roomInfos.Select(ToViewModel);

            foreach (var room in rooms)
            {
                RoomAdded.Invoke(room);
            }
        }

        public event Action<Room> RoomAdded;

        private Room ToViewModel(RoomInfoBasic room)
        {
            return new Room
            {
                Id = room.Jid.ToString(),
                Name = room.Name
            };
        }
    }
}

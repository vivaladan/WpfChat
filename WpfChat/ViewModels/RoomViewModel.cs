using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfChat.Stores;
using WpfChat.Util;

namespace WpfChat.ViewModels
{
    public class RoomViewModel
    {

        public RoomViewModel()
        {
            var client = XmppContext.Instance;

            Rooms = client.Rooms;
        }

        public ObservableCollection<ChatRoom> Rooms { get; set; }
    }
}

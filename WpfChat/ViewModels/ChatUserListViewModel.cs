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
    public class ChatUserListViewModel
    {
        public ChatUserListViewModel()
        {
            var client = XmppContext.Instance;
            //client.Connect();

            RosterItems = client.Users;

            
        }

        private void _rosterStore_RosterItemRemoved(ChatUser rosterItem)
        {
            var found = RosterItems.FirstOrDefault(c => c.Id == rosterItem.Id);
            if (found != null) RosterItems.Remove(found);
        }

        private void _rosterStore_RosterItemAdded(ChatUser rosterItem)
        {
            RosterItems.Add(rosterItem);
        }

        

        public ObservableCollection<ChatUser> RosterItems { get; set; }
    }
}

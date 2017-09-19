using Sharp.Xmpp;
using Sharp.Xmpp.Client;
using Sharp.Xmpp.Im;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfChat.Stores;

namespace WpfChat.Util
{
    public class XmppContext
    {

        private static XmppContext instance;

        private XmppContext()
        {

            Connect();
        }

        public static XmppContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XmppContext();
                }
                return instance;
            }
        }

        private XmppClient client;

        private IXmppClient CreateClient()
        {
            client = new Sharp.Xmpp.Client.XmppClient(
                "drevell.apdcomms.co.uk", "app", "app", 5222, true);
            return client;
        }

        public void Connect()
        {
            CreateClient();

            client.RosterUpdated += Client_RosterUpdated;
            client.StatusChanged += Client_StatusChanged;
            client.Im.Message += Im_Message;
            client.GroupInviteReceived += Client_GroupInviteReceived;

            var roster = client.Im.Connect();

            Users = new ObservableCollection<ChatUser>(
                roster.Select(c => new ChatUser
                {
                    Name = c.Name ?? c.Jid.Node,
                    Id = c.Jid.GetBareJid().ToString()
                }));
            Rooms = new ObservableCollection<ChatRoom>(new List<ChatRoom>());
        }

        private void Client_GroupInviteReceived(object sender, Sharp.Xmpp.Extensions.GroupInviteEventArgs e)
        {
            Rooms.Add(new ChatRoom
            {
                Name = e.ChatRoom.Node,
                Status = RoomStatus.Invited,
                AddedBy = e.From.Node
            });
        }

        private void Im_Message(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message.Body);
        }

        private void Client_StatusChanged(object sender, Sharp.Xmpp.Im.StatusEventArgs e)
        {
            var existingUser = GetByJid(e.Jid);

            if (existingUser != null)
            {
                existingUser.Status =
                    (ChatUserStatus)Enum.Parse(typeof(ChatUserStatus), e.Status.Availability.ToString());
            }
        }

        private void Client_RosterUpdated(object sender, Sharp.Xmpp.Im.RosterUpdatedEventArgs e)
        {
            var existingUser = GetByJid(e.Item.Jid);

            if (existingUser != null)
            {
                if (e.Removed)
                {
                    Users.Remove(existingUser);
                }
                else
                {
                    // what changed??
                }
            }
            else
            {
                AddUser(e.Item);
            }


        }

        private ChatUser GetByJid(Jid jid)
        {
            return Users.FirstOrDefault(c => c.Id == jid.GetBareJid().ToString());
        }

        private void AddUser(RosterItem rosterItem)
        {
            Users.Add(new ChatUser
            {
                Name = rosterItem.Name ?? rosterItem.Jid.Node,
                Id = rosterItem.Jid.GetBareJid().ToString()
            });
        }

        public ObservableCollection<ChatUser> Users { get; private set; }
        public ObservableCollection<ChatRoom> Rooms { get; private set; }
    }
}

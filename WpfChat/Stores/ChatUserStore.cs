using Sharp.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.Stores
{
    public class ChatUserStore
    {
        private readonly IXmppClient _client;
        private List<ChatUser> roster;

        public ChatUserStore(IXmppClient client)
        {
            // don't assume the client is connected..
            _client = client;


            // propogate further roster changes to watchers
            _client.RosterUpdated += _client_RosterUpdated;
            _client.Message += _client_Message;


        }

        private void _client_Message(object sender, Sharp.Xmpp.Im.MessageEventArgs e)
        {
            System.Windows.MessageBox.Show(e.Message.Body);
        }

        public void LoadRoster()
        {
            var rosterItems = _client.GetRoster();
            var rosterModels = rosterItems.Select(ToViewModel);

            this.roster = new List<ChatUser>(rosterModels);

            // notify watchers of new roster records
            foreach (var rosterItem in roster)
            {
                RosterItemAdded.Invoke(rosterItem);
            }


            _client.StatusChanged += _client_StatusChanged;
        }

        public event Action<ChatUser> RosterItemAdded;


        public event Action<ChatUser> RosterItemRemoved;

        private void _client_RosterUpdated(object sender, Sharp.Xmpp.Im.RosterUpdatedEventArgs e)
        {
            var rosterItem = ToViewModel(e.Item);

            if (!e.Removed)
            {
                RosterItemAdded.Invoke(rosterItem);
            }
            else
            {
                RosterItemRemoved.Invoke(rosterItem);
            }
        }

        private ChatUser ToViewModel(Sharp.Xmpp.Im.RosterItem xmppRosterItem)
        {
            return new ChatUser
            {
                Id = xmppRosterItem.Jid.ToString(),
                Name = xmppRosterItem.Name ?? xmppRosterItem.Jid.Node
            };
        }


        private void _client_StatusChanged(object sender, Sharp.Xmpp.Im.StatusEventArgs e)
        {
            var jid = $"{e.Jid.Node}@{e.Jid.Domain}";
            var found = roster.SingleOrDefault(c => jid == c.Id);
            if (found != null)
            {
                found.Status = (ChatUserStatus)Enum.Parse(typeof(ChatUserStatus), e.Status.Availability.ToString());
            }
        }
    }
}

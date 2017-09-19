using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Xmpp;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfChat.Stores
{
    public class ChatUser : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Id { get; set; }

        private ChatUserStatus status;

        public ChatUserStatus Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Collabo.Common;

namespace Collabo.Data
{
    public class CollaboDB{
    public List<User> Users { get; set; }
    public List<IChannel> Channels {get; set;}
    public List<Conversation> Conversations {get; set;}
    public Dictionary<Guid,List<Session>> Sessions { get; set; }
    public Dictionary<Guid, List<MessagePacket>> Messages { get; set; }

    public CollaboDB()
    {
        Users = new List<User>();
        Channels = new List<IChannel>();
        Conversations = new List<Conversation>();
        Sessions = new Dictionary<Guid, List<Session>>();
        Messages = new Dictionary<Guid, List<MessagePacket>>();
    }
}
}
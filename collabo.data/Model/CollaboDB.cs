using System;
using System.Collections.Generic;

public class CollaboDB{
    public List<Group> Groups {get; set;}
    public List<User> Users { get; set; }
    public Dictionary<Guid,List<Session>> Sessions { get; set; }
    public Dictionary<Guid,List<Guid>> GroupMembership { get; set; }

    public Dictionary<Guid, List<MessagePacket>> Messages { get; set; }

}
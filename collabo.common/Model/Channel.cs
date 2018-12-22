using System;
using System.Collections.Generic;
using System.Linq;

namespace Collabo.Common
{
public static class ChannelFactory{

    public static Channel CreateChannel(Guid createdBy, ChannelType type, string name= null){
        
        if(createdBy == Guid.Empty) throw new Exception("Invalid user. Cannot create channel.");

        if(type == ChannelType.Named && string.IsNullOrWhiteSpace(name)) throw new Exception("Name required for channel type Named.");

        switch(type){
            case ChannelType.Named:
                return new NamedChannel(name, createdBy);
            case ChannelType.Temporary:
                return new TemporaryChannel(createdBy);
            default:
                throw new Exception("Unknown channel type requested.");
        }
    }

    public static Channel CreateChannel(Guid createdBy, string name= null){
        ChannelType type= ChannelType.None;
        
        if(string.IsNullOrWhiteSpace(name)) type = ChannelType.Temporary;
        else type = ChannelType.Named;

        return CreateChannel(createdBy, type, name);
    }
}

public enum ChannelType{
    None=0, Named=1, Temporary=2, 
}

public class NamedChannel:Channel{
    public NamedChannel(string name, Guid createdBy):base(createdBy)
    {
        Name = name;
        CreatedBy = createdBy;
        CreatedOn = DateTime.UtcNow;
        Type = ChannelType.Named;
    }

}

public class TemporaryChannel:Channel{
    public TemporaryChannel(Guid createdBy):base(createdBy)
    {
        CreatedOn = DateTime.UtcNow;
        Name=$"Channel created by {CreatedBy} on {CreatedOn}";
        Type = ChannelType.Temporary;
    }
}
public abstract class Channel : IChannel
{
    protected Channel(Guid createdBy){
        ID = Guid.NewGuid();
        CreatedBy = createdBy;
        CurrentMembers = new List<ChannelMember>();
        CurrentMembers.Add(new ChannelMember(createdBy));
    }
    private ChannelType _type;
    public ChannelType Type
   
    {
        get { return _type;}
        protected set { 
            if(_type == ChannelType.None){
                _type = value;
            }
            else{
                throw new Exception("Channel type cannot be changed.");
            }
        }
    }
    
    public Guid ID { get; protected set; }
    public string Name { get; protected set; }

    public Guid CreatedBy { get; protected set; }
    public DateTime CreatedOn { get; protected set; }
    public DateTime UpdatedOn { get; protected set; }
    public DateTime ClosedOn { get; protected set; }

    public List<ChannelMember> CurrentMembers { get; set; }
    public List<ChannelMember> RemovedMembers {get; set;}
    public Guid AddMember(Guid member){
        ChannelMember newMember = new ChannelMember(member);
       CurrentMembers.Add(newMember);
       return newMember.ID;
    }

    public bool AddMemberSet(List<Guid> members){
        bool result = false;
        if(members?.Any() == true){
            foreach(Guid member in members){
                AddMember(member);
                result = true;
            }
        }
        return result;
    }

    public Guid RemoveMember(Guid member){
        ChannelMember removedMember = CurrentMembers.FirstOrDefault(c=>c.Member == member);
        if(removedMember != null){
            if(CurrentMembers.Remove(removedMember)){
                if(RemovedMembers == null)RemovedMembers = new List<ChannelMember>();
                RemovedMembers.Add(removedMember);
                return removedMember.Member;
            }
            else{
                throw new Exception($"Member {removedMember.Member} could not be removed from the channel {Name}");
            }
        }
        return Guid.Empty;
    }
}

public class ChannelMember{
    public Guid ID { get; set; }
    public Guid Member { get; set; }

    public DateTime AddedOn {get; set;}
    public DateTime RemovedOn {get; set;}
    
    public ChannelMember(){
        
    }
    public ChannelMember(Guid createdBy)
    {
        ID = Guid.NewGuid();
        Member = createdBy;
        AddedOn = DateTime.UtcNow;
    }
}
}
using System;
using Collabo.Common;

namespace Collabo.API.DTOs{
    public class ViewChannelDTO{
    public Guid ID { get; }
    public Guid CreatedBy { get; }
    public string Name { get; }
    public ChannelType Type { get; }
    public DateTime CreatedOn { get; }
    public DateTime UpdatedOn { get; }
    public DateTime ClosedOn { get; }
    public int NumberOfParticipants {get;}

    public ViewChannelDTO(IChannel channel)
    {
        ID = channel.ID;
        CreatedBy = channel.CreatedBy;
        CreatedOn  = channel.CreatedOn;
        Name = channel.Name;
        Type = channel.Type;
        UpdatedOn = channel.UpdatedOn;
        ClosedOn = channel.ClosedOn;
        NumberOfParticipants = channel.CurrentMembers?.Count??0;
    }
}
}
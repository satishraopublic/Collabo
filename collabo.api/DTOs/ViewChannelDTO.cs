using System;
using Collabo.Common;

namespace Collabo.API.DTOs{
    public class ViewChannelDTO{
    Guid ID { get; }
    Guid CreatedBy { get; }
    string Name { get; }
    ChannelType Type { get; }
    DateTime CreatedOn { get; }
    DateTime UpdatedOn { get; }
    DateTime ClosedOn { get; }
    int NumberOfParticipants {get;}

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
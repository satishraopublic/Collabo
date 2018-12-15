using System;
using Collabo.Common;

namespace Collabo.API.DTOs{
public class ViewConversationDTO{
    public DateTime InitiatedOn {get; set; }
    public Guid Channel { get; set; }
    public DateTime TerminatedOn {get; set;}

    public ViewConversationDTO(Conversation conversation)
    {
        InitiatedOn = conversation.InitiatedOn;
        Channel = conversation.Channel;
        TerminatedOn = conversation.TerminatedOn;
    }
}
}
using System;
using System.Collections.Generic;

namespace Collabo.Common
{
public interface IChannel
{
    DateTime ClosedOn { get; }
    Guid CreatedBy { get; }
    DateTime CreatedOn { get; }
    List<ChannelMember> CurrentMembers { get; set; }
    Guid ID { get; }
    string Name { get; }
    List<ChannelMember> RemovedMembers { get; set; }
    ChannelType Type { get; }
    DateTime UpdatedOn { get; }

    Guid AddMember(Guid member);
    bool AddMemberSet(List<Guid> members);

    Guid RemoveMember(Guid member);
}
}
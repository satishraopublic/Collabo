using System;
using Collabo.Common;

namespace Collabo.API.DTOs{
    public class CreateChannelDTO{
    public string Name { get;  }
    public ChannelType Type { get;  }

        public CreateChannelDTO(string name, ChannelType type)
        {
            Name = name;
            Type = type;
        }
    }
}
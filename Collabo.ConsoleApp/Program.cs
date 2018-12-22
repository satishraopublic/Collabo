using System;
using System.Linq;
using Collabo.API.DTOs;
using Collabo.Common;
using Collabo.Common.DTOs;
using Collabo.ClientServices;
using System.Collections.Generic;

namespace Collabo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid session=Guid.Empty;
            string fixHint=null;
            CollaboClientService.INSTANCE.Configure("http://localhost:10000");
            SessionDTO dto = CollaboClientService.INSTANCE.Login("srao2","password", out fixHint);

            if(dto != null) {
                session = dto.ID;
                Console.WriteLine($"session token = {dto.ID}");

                if(CollaboClientService.INSTANCE.AddChannel(session, new CreateChannelDTO("First",ChannelType.Named), out fixHint))
                {
                    List<ViewChannelDTO> channels = CollaboClientService.INSTANCE.GetAvailableChannels(session, out fixHint);
                    if(channels?.Any() == true){
                        foreach(ViewChannelDTO channel in channels){
                            Console.WriteLine($"Channel Name = {channel.Name}");
                        }
                    }
                    else{
                        Console.WriteLine($"Fix Hint(Get Channels): {fixHint}");       
                    }
                }
                else{
                    Console.WriteLine($"Fix Hint(Add Channel): {fixHint}");   
                }

            }
            else Console.WriteLine($"Fix Hint (Login): {fixHint}");


            Console.Read();
        }
    }
}

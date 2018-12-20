using System;
using Collabo.API.DTOs;
using Collabo.ClientServices;

namespace Collabo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fixHint=null;
            CollaboClientService.INSTANCE.Configure("http://localhost:10000");
            SessionDTO dto = CollaboClientService.INSTANCE.Login("srao2","password", out fixHint);
            if(dto != null) Console.WriteLine($"session token = {dto.ID}");
            else Console.WriteLine($"Fix Hint: {fixHint}");
            Console.Read();
        }
    }
}

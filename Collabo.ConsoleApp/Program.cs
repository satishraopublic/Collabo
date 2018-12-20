using System;
using RestSharp;
using Collabo.API.DTOs;

namespace Collabo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:10000");
            var request = new RestRequest("api/session/", Method.POST);
            request.AddParameter("username", "srao");
            request.AddParameter("password", "password");

            client.Execute<SessionDTO>(request, response => {
                SessionDTO dto = response.Data;
                Console.WriteLine($"Session ID={dto.ID}");
            });

            Console.Read();
        }
    }
}

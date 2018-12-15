using System;
using Microsoft.Extensions.Configuration;

namespace Collabo.API.Services{
public class ConfigurationService : IConfigurationService
{
    readonly int DEFAULT_TIMEOUT_IN_MINUTES = 10;
    IConfiguration _configuration;
    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GetDBConnectionString()
    {
        return _configuration["JsonDBFile"];
    }

    public int GetTimeoutInMinutes()
    {
        int timeOutInMinutes=0;
        if(!Int32.TryParse(_configuration["timeout:minutes"], out timeOutInMinutes)){
            timeOutInMinutes = DEFAULT_TIMEOUT_IN_MINUTES;
        }
        if(timeOutInMinutes < 0 || timeOutInMinutes > 120){
            timeOutInMinutes = DEFAULT_TIMEOUT_IN_MINUTES;
        }
        return timeOutInMinutes;
    }
}
}
namespace Collabo.API.Services{
public interface IConfigurationService{
    string GetDBConnectionString();
    int GetTimeoutInMinutes();
}
}
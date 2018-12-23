using System;

public interface ICollaboStatusException{
    int StatusCode {get;}
    string StatusMessage {get;}
}
public abstract class CollaboExceptionBase: ApplicationException, ICollaboStatusException{
    public int StatusCode { get; private set; }
    public string StatusMessage {get; private set;}

    protected CollaboExceptionBase(int statusCode, string message){
        StatusCode = statusCode;
        StatusMessage = message;
    }
}
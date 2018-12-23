using System;

public class InternalServerErrorException : CollaboExceptionBase
{
    public InternalServerErrorException(string message) : base(500, message)
    {

    }
}
using System;

public static class ExceptionHandler{
    public static ICollaboStatusException Handle(Exception ex){
        if(ex is ForbiddenException){
            return ex as ForbiddenException;
        }
        if(ex != null){
            return new InternalServerErrorException(ex.Message);    
        }
        return new InternalServerErrorException("Unknown server error.");
    }
}
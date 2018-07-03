namespace Server.Infrastructure
{
    public enum UserExceptionTypes
    {
        NotAssigned,
        UserName,
        Password
    }

    public enum CardExceptionTypes
    {
        NotAssigned,
        Number,
        Name,
        Currency,
        Validity,
        Type
    }

    public enum TransactionExceptionTypes
    {
        NotAssigned,
        CardNumber,
        Amount,
        Description
    }
}
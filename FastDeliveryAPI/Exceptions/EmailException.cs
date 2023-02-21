namespace FastDeliveryAPI.Exceptions;

public class EmailException : ApplicationException
{
    public EmailException(string customerName) : base($"the email the {customerName} is not valid")
    {

    }


}
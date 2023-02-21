namespace FastDeliveryAPI.Exceptions;

public class CreditLimitException : ApplicationException
{
    public CreditLimitException(string customerName) : base($"{customerName} cannot increment more limit credit")
    {

    }

}
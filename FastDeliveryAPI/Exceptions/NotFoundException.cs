using System.Runtime.Serialization;

namespace FastDeliveryAPI.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key) : base($"{name} whit id {key} was not found!")
    {

    }

}
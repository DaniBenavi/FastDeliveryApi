namespace FastDeliveryAPI.Models;

public record CustomerResponse(
    int Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    decimal CreditLimit,
    bool Status
);
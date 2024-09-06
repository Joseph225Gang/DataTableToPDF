namespace QuestPDF.Sample;

public class DocumentModel
{
    public string Name { get; set; } = null!;
    public AddressModel Address { get; init; } = null!;
}


public class AddressModel
{
    public string CompanyName { get; init; } = null!;
    public string Street { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
}
using QuestPDF.Helpers;

namespace QuestPDF.Sample;

public class DocumentDataSource
{
    public static DocumentModel GetDetails()
    {
        return new DocumentModel
        {
            Name = Placeholders.Name(),
            Address = GenerateRandomAddress()
        };
    }

    private static AddressModel GenerateRandomAddress()
    {
        return new AddressModel
        {
            CompanyName = Placeholders.Name(),
            Street = Placeholders.Label(),
            City = Placeholders.Label(),
            State = Placeholders.Label(),
            Email = Placeholders.Email(),
            Phone = Placeholders.PhoneNumber()
        };
    }
}
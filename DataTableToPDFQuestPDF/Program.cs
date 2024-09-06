using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Sample;
using Document = QuestPDF.Sample.Document;

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

DocumentModel model = DocumentDataSource.GetDetails();
IDocument document = new Document(model);


document.GeneratePdf("C:\\Visual Studio 2022\\DataTableToPDF\\DataTableToPDF\\test.pdf");


using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDF.Sample;

public class Document : IDocument
{
    private readonly DocumentModel _model;

    public Document(DocumentModel model)
    {
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.PaddingBottom(24).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column
                    .Item().Text($"Data Protection")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(48);
            column.Item().Element(ComposeDescription);
            column.Item().Element(ComposeInputs);
            column.Item().Text(Placeholders.Paragraphs());

            column.Item().Element(ComposeSignature);
        });
    }

    private void ComposeDescription(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text(text =>
            {
                text.Span("Issue date: ").SemiBold();
                text.Span($"{DateTime.Now:d}");
            });

            column.Item()
                .Component(new AddressComponent("Data Protection Officer", _model.Address));

            column.Item().PaddingTop(24).Text(Placeholders.Paragraph());
        });
    }

    private void ComposeInputs(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(12);

            column.Item().Component(new InputComponent("Fullname"));
            column.Item().Component(new InputComponent("Birthdate"));
            column.Item().Component(new InputComponent("Street"));
            column.Item().Component(new InputComponent("City"));
            column.Item().Component(new InputComponent("State"));
            column.Item().Component(new InputComponent("Email"));
            column.Item().Component(new InputComponent("Phone"));
        });
    }

    private void ComposeSignature(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(24);
            column.Item().Row(row =>
            {
                row.ConstantItem(100).Text("Date:").SemiBold();
                row.RelativeItem().ExtendHorizontal().BorderBottom(1).Padding(5);
            });
            column.Item().Row(row =>
            {
                row.ConstantItem(100).Text("Signature:").SemiBold();
                row.RelativeItem().ExtendHorizontal().BorderBottom(1).Padding(5);
            });
        });
    }

    public class InputComponent : IComponent
    {
        private readonly string _label;

        public InputComponent(string label)
        {
            _label = label;
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantItem(100).AlignLeft()
                    .Text($"{_label}:")
                    .SemiBold();

                row.RelativeItem(10)
                    .ExtendHorizontal()
                    .BorderBottom(1).Padding(5)
                    .Background(Colors.Grey.Lighten1)
                    .AlignCenter();
            });
        }
    }

    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private AddressModel Address { get; }

        public AddressComponent(string title, AddressModel address)
        {
            Title = title;
            Address = address;
        }

        public void Compose(IContainer container)
        {
            var pageSizes = new List<(string name, double width, double height)>()
            {
                ("Letter (ANSI A)", 8.5f, 11),
                ("Legal", 8.5f, 14),
                ("Ledger (ANSI B)", 11, 17),
                ("Tabloid (ANSI B)", 17, 11),
                ("ANSI C", 22, 17),
                ("ANSI D", 34, 22),
                ("ANSI E", 44, 34)
            };

            const int inchesToPoints = 72;

            container
            .Padding(10)
            .MinimalBox()
            .Border(1)
            .Table(table =>
            {
                IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                {
                    return container
                        .Border(1)
                        .BorderColor(Colors.Grey.Lighten1)
                        .Background(backgroundColor)
                        .PaddingVertical(5)
                        .PaddingHorizontal(10)
                        .AlignCenter()
                        .AlignMiddle();
                }

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();

                    columns.ConstantColumn(75);
                    columns.ConstantColumn(75);

                    columns.ConstantColumn(75);
                    columns.ConstantColumn(75);
                });

                table.Header(header =>
                {
                    // please be sure to call the 'header' handler!

                    header.Cell().RowSpan(2).Element(CellStyle).ExtendHorizontal().AlignLeft().Text("Document type");

                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("Inches");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("Points");

                    header.Cell().Element(CellStyle).Text("Width");
                    header.Cell().Element(CellStyle).Text("Height");

                    header.Cell().Element(CellStyle).Text("Width");
                    header.Cell().Element(CellStyle).Text("Height");

                    // you can extend existing styles by creating additional methods
                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                });

                foreach (var page in pageSizes)
                {
                    table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(page.name);

                    // inches
                    table.Cell().Element(CellStyle).Text(page.width);
                    table.Cell().Element(CellStyle).Text(page.height);

                    // points
                    table.Cell().Element(CellStyle).Text(page.width * inchesToPoints);
                    table.Cell().Element(CellStyle).Text(page.height * inchesToPoints);

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                }
            });

        }
    }
}
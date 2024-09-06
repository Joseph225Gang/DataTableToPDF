using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Data;

PdfWriter writer = new PdfWriter("C:\\Visual Studio 2022\\DataTableToPDF\\DataTableToPDF\\demo.pdf");
PdfDocument pdf = new PdfDocument(writer);
Document document = new Document(pdf);
Paragraph header = new Paragraph("HEADER")
   .SetTextAlignment(TextAlignment.CENTER)
   .SetFontSize(20);

document.Add(header);
ExportDataTableToPDF(MakeDataTable());
document.Close();

DataTable  MakeDataTable()
{
    DataTable dt = new DataTable();

    dt.Columns.Add("S1. NO");
    dt.Columns.Add("Name");
    dt.Columns.Add("Country");
    dt.Columns.Add("Region");

    dt.Rows.Add("1","Smith","United States", "California");
    dt.Rows.Add("2", "Jack", "United Kingdom", "London");
    dt.Rows.Add("3", "Lucas", "Germany", "Berlin");
    dt.Rows.Add("4", "Khalid", "UAE", "Dubai");
    dt.Rows.Add("5", "William", "Australia", "Canberra");

    return dt;
}

void ExportDataTableToPDF(DataTable dt)
{
    Paragraph subheader = new Paragraph("SUB HEADER")
   .SetTextAlignment(TextAlignment.CENTER)
   .SetFontSize(15);
    document.Add(subheader);

    // Line separator
    LineSeparator ls = new LineSeparator(new SolidLine());
    document.Add(ls);
    Image img = new Image(ImageDataFactory
            .Create("C:\\Visual Studio 2022\\DataTableToPDF\\DataTableToPDF\\image.png"))
            .SetTextAlignment(TextAlignment.CENTER);
    document.Add(img);

    Table table = new Table(4, false);
    table.SetWidth(280);
    Cell cell11 = new Cell(1, 1)
       .SetBackgroundColor(ColorConstants.GRAY)
       .SetTextAlignment(TextAlignment.CENTER)
       .Add(new Paragraph("S1. NO"));
    Cell cell12 = new Cell(1, 1)
       .SetBackgroundColor(ColorConstants.GRAY)
       .SetTextAlignment(TextAlignment.CENTER)
       .Add(new Paragraph("Name"));
    Cell cell13 = new Cell(1, 1)
       .SetBackgroundColor(ColorConstants.GRAY)
       .SetTextAlignment(TextAlignment.CENTER)
       .Add(new Paragraph("Country"));
    Cell cell14 = new Cell(1, 1)
       .SetBackgroundColor(ColorConstants.GRAY)
       .SetTextAlignment(TextAlignment.CENTER)
       .Add(new Paragraph("Region"));
    for (int i = 0; i < dt.Rows.Count; i++)
    {
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            float width = default;
            if (j == 0)
                width = 40;
            else
                width = 100;
            Cell cell = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetWidth(width)
            .Add(new Paragraph(dt.Rows[i][j].ToString()));
            table.AddCell(cell);
        }
    }

    document.Add(table);
}
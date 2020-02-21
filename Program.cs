using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace kli.pdfMerge
{
	static class Program
	{
		static void Main(string[] args)
		{
			//MergePdf();
			RenumberPdf();
		}

		public static void RenumberPdf()
		{
			var src = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.pdf");
			var target = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "outputWith.pdf");
			var pdf = new PdfDocument(new PdfReader(src), new PdfWriter(target));
			var doc = new Document(pdf);
			var bottom = pdf.GetDefaultPageSize().GetBottom();
			var right = pdf.GetDefaultPageSize().GetRight();

			for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
			{
				doc.SetFontSize(10);
				doc.ShowTextAligned(new Paragraph($"Seite {i}"), right - 20, bottom + 20, i, TextAlignment.RIGHT, VerticalAlignment.BOTTOM, 0);
			}
			doc.Close();
		}

		public static void MergePdf()
		{
			PdfDocument pdf = new PdfDocument(new PdfWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.pdf")));
			PdfMerger merger = new PdfMerger(pdf);

			foreach (var file in Directory.GetFiles("C:\\_tmp", "*.pdf"))
			{
				var srcPdf = new PdfDocument(new PdfReader(file));
				merger.Merge(srcPdf, 1, srcPdf.GetNumberOfPages());
				srcPdf.Close();
			}
			pdf.Close();
		}
	}
}

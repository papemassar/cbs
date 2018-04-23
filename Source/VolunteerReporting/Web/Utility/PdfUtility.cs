
using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Read.CaseReportsForListing;
using System.Linq;
using Concepts;

namespace Web.Utility
{
    public static class PdfUtility
    {
        public static byte[] CreateCaseReportPdf(IList<CaseReportForListing> caseReports, string[] opts)
        {
            var now = DateTimeOffset.UtcNow;
            var nowString = now.ToString("yyyy-MMMM-dd");

            using (var stream = new MemoryStream())
            {
                var doc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);

                using (var writer = PdfWriter.GetInstance(doc, stream))
                {
                    writer.CloseStream = false;

                    doc.AddAuthor("Cbs - Volunteer Reporting");
                    doc.AddCreator("Cbs - Volunteer Reporting");
                    doc.AddSubject("Case Reports " + nowString);
                    doc.AddTitle("Case Reports " + nowString);

                    doc.Open();
                    
                    AddCaseReportsToPdf(caseReports, doc, opts);

                    doc.Close();

                    writer.Flush();
                }
                
                stream.Seek(0, SeekOrigin.Begin);
                stream.Flush();

                return stream.ToArray();
            }
        }

        private static void AddCaseReportsToPdf(IList<CaseReportForListing> caseReports, Document doc, string[] opts)
        {
            var table = new PdfPTable(3);
            
            AddCaseReportFieldsToTable(table);

            foreach (var caseReport in caseReports)
            {
                AddCaseReportDataToTable(table, caseReport);
            }
            
            doc.Add(table);

        }

        private static void AddCaseReportFieldsToTable(PdfPTable table)
        {
            table.AddCell("Time");
            table.AddCell("Status");
            table.AddCell("Datacollector");
            table.AddCell("Healthrisk");
            table.AddCell("Males < 5");
            table.AddCell("Males >= 5");
            table.AddCell("Females < 5");
            table.AddCell("Females >= 5");
            table.AddCell("Lat., Long.");


        }

        private static void AddCaseReportDataToTable(PdfPTable table, CaseReportForListing caseReport)
        {
            var time = caseReport.Timestamp.ToString("MMMM dd, yyyy, hh:mm:ss tt");
            var status = caseReport.ParsingErrorMessage.Count() == 0;
            var dataCollector = caseReport.DataCollectorDisplayName != "Unknown"
                ? caseReport.DataCollectorDisplayName
                : "Origin: " + caseReport.Origin;

            table.AddCell(time);
            table.AddCell(status ? "Success" : "Error");
            table.AddCell(dataCollector);

            if (status)
            {
                var healthRisk = caseReport.HealthRisk;

                var malesUnder5 = caseReport.NumberOfMalesUnder5.ToString();
                var malesOver5 = caseReport.NumberOfMalesAged5AndOlder.ToString();

                var femalesUnder5 = caseReport.NumberOfFemalesUnder5.ToString();

                var femalesOver5 = caseReport.NumberOfFemalesAged5AndOlder.ToString();

                var latLong = caseReport.Location != Location.NotSet ?
                    caseReport.Location.ToString() : "";

                
                table.AddCell(healthRisk);
                table.AddCell(malesUnder5);
                table.AddCell(malesOver5);
                table.AddCell(femalesUnder5);
                table.AddCell(femalesOver5);
                table.AddCell(latLong);
            }
            else
            {
                var message = caseReport.Message;

                var errorMessages = String.Join(", ", caseReport.ParsingErrorMessage);

                var isKnowDataCollector = dataCollector != "Unknown";

            }
            
            

            
        }
    }
}
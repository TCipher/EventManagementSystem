using CsvHelper;
using EventHub.TicketManagement.Application.Contracts.Infrastructure;
using EventHub.TicketManagement.Application.Features.Events.Queries.GetEventsExport;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace EventHub.TicketManagement.Infrastructure.FileExport
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(eventExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}

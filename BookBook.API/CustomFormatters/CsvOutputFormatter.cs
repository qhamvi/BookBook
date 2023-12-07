using System.Text;
using BookBook.DTOs.DataTransferObject;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace BookBook.API;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();

        if(context.Object is IEnumerable<AuthorDto>)
        {
            foreach (var author in (IEnumerable<AuthorDto>)context.Object)
            {
                FormatCSV(buffer, author);
            }
        }
        else
        {
            FormatCSV(buffer, (AuthorDto)context.Object);
        }
        await response.WriteAsync(buffer.ToString());
    }
    public static void FormatCSV(StringBuilder buffer, AuthorDto author)
    {
        buffer.AppendLine($"{author.Id},\"{author.FullName},\"{author.PhoneNumber},\"{author.DayOfBirth},\"{author.Country}\"");
    }
}

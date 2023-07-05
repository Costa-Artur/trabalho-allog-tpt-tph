using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace Univali.Api.Configuration;

public static class MyJPIF
{
   public static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
   {
       var builder = new ServiceCollection()
           .AddLogging()
           .AddMvc()
           .AddNewtonsoftJson()
           .Services.BuildServiceProvider();


       return builder
           .GetRequiredService<IOptions<MvcOptions>>()
           .Value
           .InputFormatters
           .OfType<NewtonsoftJsonPatchInputFormatter>()
           .First();
   }
}

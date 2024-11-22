using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MonthlyReport.BLL.Interfaces;

namespace MonthlyReport.Server.Services
{
    public class RazorViewsTemplateService(IRazorViewEngine viewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider, ILogger<RazorViewsTemplateService> logger) : ITemplateService
    {
        public async Task<string> RenderAsync<TViewModel>(string templateFileName, TViewModel viewModel)
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            await using var outputWriter = new StringWriter();

            var viewResult = viewEngine.FindView(actionContext, templateFileName, false);

            var viewDictionary = new ViewDataDictionary<TViewModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = viewModel
            };

            var tempDataDictionary = new TempDataDictionary(httpContext, tempDataProvider);

            if (!viewResult.Success)
            {
                throw new KeyNotFoundException($"Could not render the HTML, because {templateFileName} template does not exist");
            }

            try
            {
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, tempDataDictionary, outputWriter, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return outputWriter.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not render the HTML because of an error");

                return string.Empty;
            }
        }
    }
}

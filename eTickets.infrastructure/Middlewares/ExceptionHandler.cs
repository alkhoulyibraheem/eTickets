using eTickets.core.Eceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.infrastructure.Middlewares
{
	public class ExceptionHandler
	{
		private readonly RequestDelegate _next;
		private readonly IServiceProvider _serviceProvider;

		public ExceptionHandler(RequestDelegate next,
								IServiceProvider serviceProvider)
		{
			_next = next;
			_serviceProvider = serviceProvider;
		}

		public async Task Invoke(HttpContext context)
		{
			var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
			var exception = exceptionHandlerPathFeature.Error;
			var response = new JsonResponse();
			context.Response.StatusCode = (int)HttpStatusCode.OK;
			switch (exception)
			{
				case DoublictPhoneOrEmailEexption _:
				case EntityNotFoundExecption _:
				case OperationFailedException _:
					response.msg = $"e:{ exception.Message}";
					response.close = 0;
					response.status = 0;
					break;
				default:
					response.msg = $"e: there is some error";
					response.close = 0;
					response.status = 0;
					var requestBody = string.Empty;
					var req = context.Request;
					req.EnableBuffering();
					if (req.Body.CanSeek)
					{
						req.Body.Seek(0, SeekOrigin.Begin);
						using (var reader = new StreamReader(
							req.Body,
							Encoding.UTF8,
							false,
							8192,
							true))
						{
							requestBody = await reader.ReadToEndAsync();
						}
						req.Body.Seek(0, SeekOrigin.Begin);
					}
					break;
			}

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(GetHtmlErrorPage(response));
        }

        private string GetHtmlErrorPage(JsonResponse response)
        {
            return @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head><base href=""../../""/>
            <title>Metronic - The World's #1 Selling Bootstrap Admin Template by Keenthemes</title>
            <meta charset=""utf-8"" />
            <meta name=""description"" content=""The most advanced Bootstrap 5 Admin Theme with 40 unique prebuilt layouts on Themeforest trusted by 100,000 beginners and professionals. Multi-demo, Dark Mode, RTL support and complete React, Angular, Vue, Asp.Net Core, Rails, Spring, Blazor, Django, Express.js, Node.js, Flask, Symfony & Laravel versions. Grab your copy now and get life-time updates for free."" />
            <meta name=""keywords"" content=""metronic, bootstrap, bootstrap 5, angular, VueJs, React, Asp.Net Core, Rails, Spring, Blazor, Django, Express.js, Node.js, Flask, Symfony & Laravel starter kits, admin themes, web design, figma, web development, free templates, free admin themes, bootstrap theme, bootstrap template, bootstrap dashboard, bootstrap dak mode, bootstrap button, bootstrap datepicker, bootstrap timepicker, fullcalendar, datatables, flaticon"" />
            <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
            <meta property=""og:locale"" content=""en_US"" />
            <meta property=""og:type"" content=""article"" />
            <meta property=""og:title"" content=""Metronic - Bootstrap Admin Template, HTML, VueJS, React, Angular. Laravel, Asp.Net Core, Ruby on Rails, Spring Boot, Blazor, Django, Express.js, Node.js, Flask Admin Dashboard Theme & Template"" />
            <meta property=""og:url"" content=""https://keenthemes.com/metronic"" />
            <meta property=""og:site_name"" content=""Keenthemes | Metronic"" />
            <link rel=""canonical"" href=""https://preview.keenthemes.com/metronic8"" />
            <link rel=""shortcut icon"" href=""assets/media/logos/favicon.ico"" />
            <!--begin::Fonts(mandatory for all pages)-->
            <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700"" />
            <!--end::Fonts-->
            <!--begin::Global Stylesheets Bundle(mandatory for all pages)-->
            <link href=""assets/plugins/global/plugins.bundle.css"" rel=""stylesheet"" type=""text/css"" />
            <link href=""assets/css/style.bundle.css"" rel=""stylesheet"" type=""text/css"" />
            <!--end::Global Stylesheets Bundle-->
        </head>
        <!--end::Head-->
        <!--begin::Body-->
        <body id=""kt_body"" class=""app-blank bgi-size-cover bgi-position-center bgi-no-repeat"">
            <!--begin::Theme mode setup on page load-->
            <script>var defaultThemeMode = ""light""; var themeMode; if ( document.documentElement ) { if ( document.documentElement.hasAttribute(""data-bs-theme-mode"")) { themeMode = document.documentElement.getAttribute(""data-bs-theme-mode""); } else { if ( localStorage.getItem(""data-bs-theme"") !== null ) { themeMode = localStorage.getItem(""data-bs-theme""); } else { themeMode = defaultThemeMode; } } if (themeMode === ""system"") { themeMode = window.matchMedia(""(prefers-color-scheme: dark)"").matches ? ""dark"" : ""light""; } document.documentElement.setAttribute(""data-bs-theme"", themeMode); }</script>
            <!--end::Theme mode setup on page load-->
            <!--begin::Root-->
            <div class=""d-flex flex-column flex-root"" id=""kt_app_root"">
                <!--begin::Page bg image-->
                <style>body { background: url('/Image/back.jpg'); } [data-bs-theme=""dark""] body { background-image: url('/Image/back.jpg'); }</style>
                <!--end::Page bg image-->
                <!--begin::Authentication - Signup Welcome Message -->
                <div class=""d-flex flex-column flex-center flex-column-fluid"">
                    <!--begin::Content-->
                    <div class=""d-flex flex-column flex-center text-center p-10"">
                        <!--begin::Wrapper-->
                        <div class=""card card-flush w-lg-650px py-5"">
                            <div class=""card-body py-15 py-lg-20"">
                                <!--begin::Title-->
                                <h1 class=""fw-bolder fs-2hx text-gray-900 mb-4"">Oops!</h1>
                                <!--end::Title-->
                                <!--begin::Text-->
                                <div class=""fw-semibold fs-6 text-gray-500 mb-7"">" + response.msg.Substring(2)+@" please enter right requst</div>
                                <!--end::Text-->
                                <!--begin::Illustration-->
                                <div class=""mb-3"">
                                    <img src=""/Image/400.png"" class=""mw-100 mh-300px"" alt="""" />
                                </div>
                                <!--end::Illustration-->
                            </div>
                        </div>
                        <!--end::Wrapper-->
                    </div>
                    <!--end::Content-->
                </div>
                <!--end::Authentication - Signup Welcome Message-->
            </div>
            <!--end::Root-->
            <!--begin::Javascript-->
            <script>var hostUrl = ""assets/"";</script>
            <!--begin::Global Javascript Bundle(mandatory for all pages)-->
            <script src=""assets/plugins/global/plugins.bundle.js""></script>
            <script src=""assets/js/scripts.bundle.js""></script>
            <!--end::Global Javascript Bundle-->
            <!--end::Javascript-->
        </body>
        <!--end::Body-->
        </html>
        ";
        }




    }
}

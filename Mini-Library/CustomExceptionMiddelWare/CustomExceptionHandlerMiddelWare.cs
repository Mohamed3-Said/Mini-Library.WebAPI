using DomainLayer.Exceptions;
using DomainLayer.Exceptions.EmployeeExceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net.Http;

namespace Mini_Library.CustomExceptionMiddelWare
{
    public class CustomExceptionHandlerMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddelWare> _logger;
        //1-ctor
        public CustomExceptionHandlerMiddelWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddelWare> logger)
        {
            _next = Next;
            _logger = logger;
        }
        //2-invoke
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);
                await HandelNotFoundEndPoint(httpcontext);
            }
            catch(Exception ex)
            {
                //Log the Exception in Server 
                _logger.LogError("Something Went Wrong !!");
                await HandleExceptionAsync(httpcontext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            //3-Return object in The Response Body:
            int stausecode;
            ErrorToReturn Responseobj;
            switch(ex)
            {
                case NotfoundException:
                    stausecode = StatusCodes.Status404NotFound;
                    Responseobj = new ErrorToReturn()
                                            {
                        StatusCode = stausecode,
                        ErrorMessage = ex.Message
                    };
                    break;
                case EmpoyeeBadRequestException empoyeeBadRequest:
                    stausecode = StatusCodes.Status400BadRequest;
                    Responseobj = new ErrorToReturn()
                    {
                        StatusCode = stausecode,
                        ErrorMessage = empoyeeBadRequest.Message,
                        Errors = empoyeeBadRequest.Errors
                    };
                    break;
                    default:
                    stausecode = StatusCodes.Status500InternalServerError;
                    Responseobj = new ErrorToReturn
                    {
                        StatusCode = stausecode,
                        ErrorMessage = ex.Message
                    };
                    break;

            }

            httpcontext.Response.StatusCode = stausecode;
            httpcontext.Response.ContentType = "application/json";
            await httpcontext.Response.WriteAsJsonAsync(Responseobj);
        }

        private async Task HandelNotFoundEndPoint(HttpContext httpcontext)
        {
            if (httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpcontext.Request.Path} is Not Found"
                };
                await httpcontext.Response.WriteAsJsonAsync(response);
            }
        }
    }

}

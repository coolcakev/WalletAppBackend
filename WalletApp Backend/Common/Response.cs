using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Models
{
    public class EmptyValue{}
    public class Response<T>
    {
        public bool IsValid => Message is null || Message.Length == 0;
        public string[] Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Value { get; set; }

        public Response(T value, HttpStatusCode statusCode)
        {
            Value = value;
            StatusCode = statusCode;

        }

        public Response(HttpStatusCode statusCode, params string[] message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public Response<TType> ConvertToNewType<TType>(TType value)
        {
            var response = new Response<TType>(value,StatusCode);
            return response;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new {Message,StatusCode});
        }
    }

    public static class SuccessResponses
    {
        public static Response<T> Created<T>(T value)
        {
            return new Response<T>(value, HttpStatusCode.Created);
        }

        public static Response<T> Ok<T>(T value)
        {
            return new Response<T>(value, HttpStatusCode.OK);
        }

        public static Response<EmptyValue> Ok()
        {
            return new Response<EmptyValue>(HttpStatusCode.OK);
        }

        public static Response<EmptyValue> NoContent()
        {
            return new Response<EmptyValue>(HttpStatusCode.NoContent);
        }
    }

    public static class FailureResponses
    {
        public static Response<T> NotFound<T>(string message)
        {
            return new Response<T>(HttpStatusCode.NotFound, message);
        }
        public static Response<EmptyValue> NotFound(string message)
        {
            return NotFound<EmptyValue>(message);
        }
        public static Response<T> Forbidden<T>(string message)
        {
            return new Response<T>(HttpStatusCode.Forbidden, message);
        }
        public static Response<EmptyValue> Forbidden(string message)
        {
            return Forbidden<EmptyValue>(message);
        }
        public static Response<T> BadRequest<T>(params string[] message)
        {
            return new Response<T>(HttpStatusCode.BadRequest, message);
        }
        public static Response<EmptyValue> BadRequest(params string[] message)
        {
            return BadRequest<EmptyValue>(message);
        }

        public static Response<EmptyValue> InternalError(params string[] message)
        {
            return new Response<EmptyValue>(HttpStatusCode.InternalServerError, message);
        }
        public static Response<T> UnAuthorized<T>()
        {
            return new Response<T>(HttpStatusCode.Unauthorized, "User are not authorized");
        }
        public static Response<EmptyValue> Conflict(string message)
        {
            return new Response<EmptyValue>(HttpStatusCode.Conflict, message);
        }

    }

}
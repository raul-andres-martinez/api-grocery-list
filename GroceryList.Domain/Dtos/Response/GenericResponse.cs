namespace GroceryList.Domain.Dtos.Response
{
    public class GenericResponse
    {
        public GenericResponse(string message, object data)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public GenericResponse(string message)
        {
            Success = false;
            Message = message;
            Data = null;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}
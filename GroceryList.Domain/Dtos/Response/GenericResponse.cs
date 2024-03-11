namespace GroceryList.Domain.Dtos.Response
{
    public class GenericResponse<T>
    {
        public GenericResponse(string message, T data)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public GenericResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
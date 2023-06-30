namespace Microservices.Services.ProductAPI.Models.Dto
{
    public class ResponseDto<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public static ResponseDto<T> Success(T result)
        {
            return new ResponseDto<T>
            {
                Result = result,
                IsSuccess = true
            };
        }

        public static ResponseDto<T> Failure(string message)
        {
            return new ResponseDto<T>
            {
                Message = message,
                IsSuccess = false
            };
        }
    }
}

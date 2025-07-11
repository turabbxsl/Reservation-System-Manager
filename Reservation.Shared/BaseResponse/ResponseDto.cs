namespace Reservation.Shared.BaseResponse
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ResponseDto()
        {
            Errors = new List<string>();
        }
    }

    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ResponseDto()
        {
            Errors = new List<string>();
        }
        public static ResponseDto<T> SuccessResponse(T data, int statusCode = 200)
        {
            return new ResponseDto<T> { IsSuccess = true, Data = data, StatusCode = statusCode };
        }
        public static ResponseDto<T> SuccessResponse(T data, List<string> errors, int statusCode = 200)
        {
            return new ResponseDto<T> { IsSuccess = true, Data = data, Errors = errors, StatusCode = statusCode };
        }
        public static ResponseDto<T> SuccessResponse(string message, int statusCode = 200)
        {
            return new ResponseDto<T> { IsSuccess = true, Data = default, StatusCode = statusCode, Message = message };
        }

        public static ResponseDto<T> SuccessResponse(T data, string message, int statusCode = 200)
        {
            return new ResponseDto<T> { IsSuccess = true, Data = data, StatusCode = statusCode, Message = message };
        }
        public static ResponseDto<T> ErrorResponse(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { IsSuccess = false, Errors = errors, StatusCode = statusCode };
        }
        public static ResponseDto<T> ErrorResponse(string errors, int statusCode)
        {
            return new ResponseDto<T> { IsSuccess = false, Errors = new List<string>() { errors }, StatusCode = statusCode };
        }

        public static ResponseDto<T> ErrorResponse(List<string> errors, string error, int statusCode)
        {
            return new ResponseDto<T> { IsSuccess = false, Errors = errors, StatusCode = statusCode, Message = error };
        }
    }

}

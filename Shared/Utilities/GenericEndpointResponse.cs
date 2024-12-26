
namespace Shared.Utilities
{
    public class GenericEndpointResponse<T>
    {
        public required string Status {set; get;}
        public T? Content {set; get;}

        public required string Msg {set; get;}

        public required string Log {set; get;}

        public static GenericEndpointResponse<T> Success(T _content, string _msg, string _log){
            return new GenericEndpointResponse<T> {
                Status = "Success",
                Content = _content,
                Msg = _msg,
                Log = _log 
            };
        }

        public static GenericEndpointResponse<T> Error(T _content, string _msg, string _log){
            return new GenericEndpointResponse<T> {
                Status = "Error",
                Content = _content,
                Msg = _msg,
                Log = _log 
            };
        }
    }
}
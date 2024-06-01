using RestSharp;

namespace CookBookAdminApp.Helpers
{
    public class ResponseValue<T>
    {
        private ResponseStatus _statuse;
        public ResponseStatus Status { get { return _statuse; } set { _statuse = value; } }

        private T _value;
        public T Value { get => _value; set => _value = value; }

        private string _exception;
        public string Exception { get => _exception; set => _exception = value; }

        public ResponseValue()
        {
            Exception = null;
            Status = ResponseStatus.Error;
        }
    }
}

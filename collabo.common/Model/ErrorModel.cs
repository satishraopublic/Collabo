namespace collabo.Common
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString(){
            return $"Code={StatusCode}, Message={ErrorMessage}";
        }
    }
}
namespace Codelux.Common.Responses
{
    public class ServiceResponse
    {
        public ServiceResponse() { }

        public ServiceResponse(string service, int code)
        {
            Code = code;
            Service = service;
        }

        public int Code { get; }
        public string Service { get; }

        public override bool Equals(object obj)
        {
            ServiceResponse response = obj as ServiceResponse;
            if (response == null) return false;

            return response.Code == Code && response.Service == Service;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Code.GetHashCode();
                hash = hash * 23 + Service.GetHashCode();
                return hash;
            }
        }
    }
}

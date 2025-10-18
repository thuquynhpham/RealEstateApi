namespace RealEstate.Api.Validation
{
    public class RealEstateValidationError(string name, string message, int? key = null)
    {
        public string Name { get; set; } = name;
        public string Message { get; set; } = message;
        public int? Key { get; set; } = key;

    }
}

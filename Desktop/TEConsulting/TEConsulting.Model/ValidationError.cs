using TEConsulting.Types;

namespace TEConsulting.Model
{
    public class ValidationError
    {
        public ValidationError(string desc, ErrorType errorType)
        {
            Description = desc;
            ErrorType = errorType;
        }

        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }
    }

}

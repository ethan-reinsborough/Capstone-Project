using System.Collections.Generic;

namespace TEConsulting.Model
{
    public abstract class BaseEntity
    {
        public List<ValidationError> Errors = new List<ValidationError>();

        public void AddError(ValidationError error)
        {
            Errors.Add(error);
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace EmployeeModels.Interfaces
{
    public interface IGenericPropertyBag: 
        IDictionary<string, object>, 
        ICollection<KeyValuePair<string, object>>, 
        IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        T GetPropertyValue<T>(string key, bool throwOnInvalidCast);
    }
}
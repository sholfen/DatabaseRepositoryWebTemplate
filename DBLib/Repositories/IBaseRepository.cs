using System.Collections.Generic;

namespace DBLib.Repositories
{
    public interface IBaseRepository<T>
    {
        int Insert(T parameter);
        List<T> Query();
        List<T> QueryBy(Dictionary<string, object> parameters);
    }
}

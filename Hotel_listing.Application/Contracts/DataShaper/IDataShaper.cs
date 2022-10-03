using System.Dynamic;

namespace Hotel_listing.Application.Contracts.DataShaper;

public interface IDataShaper<T>
{
    IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string
        fieldsString);
    ExpandoObject ShapeData(T entity, string fieldsString);

}
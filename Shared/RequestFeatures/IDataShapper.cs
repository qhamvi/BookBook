using System.Dynamic;

namespace Shared;

public interface IDataShapper<T>
{
    IEnumerable<Entity> ShapeData(IEnumerable<T> entities, string fieldsString);
    Entity ShapeData(T entity, string fieldsString);
}

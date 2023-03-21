using CodeMvvm.Entities;

namespace CodeMvvm.Data.Repositories;

public interface IConditionRepository
{
    List<Condition> Get();
}
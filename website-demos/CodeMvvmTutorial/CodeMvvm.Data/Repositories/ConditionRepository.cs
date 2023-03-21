using CodeMvvm.Data.Models;
using CodeMvvm.Entities;

namespace CodeMvvm.Data.Repositories;

public class ConditionRepository : IConditionRepository
{
    private readonly CodeMvvmDbContext context;

    public ConditionRepository(CodeMvvmDbContext context)
    {
        this.context = context;
    }

    public List<Condition> Get()
    {
        return context.Conditions.ToList();
    }
}
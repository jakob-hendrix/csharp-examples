using System.ComponentModel;
using System.Runtime.CompilerServices;
using CodeMvvm.Data.Repositories;
using CodeMvvm.Entities;

namespace CodeMvvm.ViewModels;

public class ConditionViewModel : INotifyPropertyChanged
{
    private readonly IConditionRepository repository;
    private List<Condition> conditions;
    private bool isBusy;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ConditionViewModel(IConditionRepository repository)
    {
        this.repository = repository;
        if (conditions == null)
        {
            LoadConditions();
        }
    }

    public bool IsBusy
    {
        get => isBusy;
        set
        {
            if (value == isBusy) return;
            isBusy = value;
            OnPropertyChanged();
        }
    }

    public List<Condition> Conditions
    {
        get => conditions;
        set
        {
            if (Equals(value, conditions)) return;
            conditions = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// This is called by controller/page to fetch data
    /// </summary>
    public void HandleRequest()
    {
        LoadConditions();
    }

    public void LoadConditions()
    {
        if (repository == null)
        {
            throw new ApplicationException("Repository should be set up in DI container");
        }

        Conditions = repository.Get().OrderBy(c => c.Name).ToList();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
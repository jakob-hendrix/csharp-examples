using CodeMvvm.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeMvvm.Data.DefaultData;

public class DbInitializer
{
    private readonly ModelBuilder modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        modelBuilder.Entity<Condition>().HasData(
            new Condition
            {
                Id = 1,
                Name = "Prone",
                Description = "The character is lying on the ground. A prone attacker has a –4 penalty on melee attack rolls and cannot use a ranged weapon (except for a crossbow). A prone defender gains a +4 bonus to Armor Class against ranged attacks, but takes a –4 penalty to AC against melee attacks.\r\nStanding up is a move-equivalent action that provokes an attack of opportunity.",
                URL = "https://aonprd.com/Rules.aspx?Name=Conditions&Category=Combat"
            },
            new Condition
            {
                Id = 2,
                Name = "Haste",
                Description = "",
                URL = ""
            }
            );
    }
}
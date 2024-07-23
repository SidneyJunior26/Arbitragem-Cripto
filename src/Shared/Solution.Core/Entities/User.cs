using Solution.Core.Enum;

namespace Solution.Core.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Level Level { get; private set; }
    public bool Trial { get; private set; }
    public DateTime? TrialExpiration { get; private set; }

    public User(string name, string email, string password, bool trial)
    {
        Id = Guid.NewGuid();

        Name = name;
        Email = email;
        Password = password;
        Level = Level.USER;
        Trial = trial;
        
        if (Trial)
            TrialExpiration = DateTime.Now.AddDays(3);

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;

        UpdateDate = DateTime.Now;
    }

    public void UpdatePassword(string password)
    {
        Password = password;

        UpdateDate = DateTime.Now;
    }

    public void ChangeTrial()
    {
        Trial = !Trial;

        UpdateDate = DateTime.Now;
    }
}
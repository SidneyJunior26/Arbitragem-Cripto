using Solution.Core.Enum;

namespace Solution.Core.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Level Level { get; private set; }
}
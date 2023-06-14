namespace Test101;

public class Basic
{
    [Fact]
    public void Add_CreateUser_ReturnsSuccess()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.Add(new("Mohamad", "Lawand"));

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);
        Assert.NotNull(savedUser);
        Assert.Equal("Mohamad", savedUser.FirstName);
        Assert.Equal("Lawand", savedUser.LastName);
        Assert.NotEmpty(savedUser.Phone);
        Assert.False(savedUser.VerifiedEmail);
    }

    [Fact]
    public void Verify_VerifyEmailAddress_ReturnsSuccess()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.Add(new("Mohamad", "Lawand"));

        var firstUser = userManagement.AllUsers.ToList().First();
        userManagement.VerifyEmail(firstUser.Id);

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);
        Assert.True(savedUser.VerifiedEmail);
    }

    [Fact]
    public void Update_UpdateMobileNumber_ReturnsSuccess()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.Add(new("Mohamad", "Lawand"));

        var firstUser = userManagement.AllUsers.ToList().First();
        firstUser.Phone = "+4409090909090";
        userManagement.UpdatePhone(firstUser);

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);
        Assert.Equal("+4409090909090", savedUser.Phone);
    }
}

public record User(string FirstName, string LastName)
{
    public int Id { get; init; }
    public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
    public string Phone { get; set; } = "+44 ";
    public bool VerifiedEmail { get; set; } = false;
}
public class UserManagement
{
    private readonly List<User> _users = new();
    private int idCounter = 1;
    public IEnumerable<User> AllUsers => _users;

    public void Add(User user)
    {
        _users.Add(user with { Id = idCounter++ });
    }
    public void UpdatePhone(User user)
    {
        var dbUser = _users.First(x => x.Id == user.Id);

        dbUser.Phone = user.Phone;
    }
    public void VerifyEmail(int id)
    {
        var dbUser = _users.First(x => x.Id == id);
        dbUser.VerifiedEmail = true;
    }
}


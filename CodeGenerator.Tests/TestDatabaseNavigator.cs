namespace CodeGenerator.Tests;

[TestFixture]
public class TestDatabaseNavigator
{
    private Server server;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Server.ConnectionString = @"Server=.;Database=master;Integrated Security=SSPI;TrustServerCertificate=True";
        Server.ProviderType = DataSource.SqlServer;
        server = new Server();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        server?.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void TestConstructor()
    {
        Assert.That(server, Is.Not.Null);
    }

    [Test]
    public void TestDatabaseCollection()
    {
        Assert.That(server.Databases, Is.Not.Empty);
    }

    [Test]
    public void TestDatabaseCollectionRemoveAndReload()
    {
        int count = server.Databases.Count;
        Console.Out.WriteLine(count);
        server.Databases.Remove(server.Databases.First());

        int countAfterRemove = count - 1;
        Assert.That(server.Databases, Has.Count.EqualTo(countAfterRemove));
        server.Reload();
        Assert.That(server.Databases, Has.Count.EqualTo(count));
    }

    [Test]
    public void TestDatabaseTablesCollection()
    {
        Assert.That(server.Databases.First().Tables.IsNullOrEmpty(), Is.False);
    }

    [Test]
    public void TestDatabaseTableColumnsCollection()
    {
        var table = server.Databases.First().Tables.First();
        Assert.That(table.Columns, Is.Not.Empty);
    }

    [Test]
    public void TestDatabaseKeys()
    {
        var table = server.Databases.First().Tables.First();
        Assert.That(table.Keys, Is.Not.Empty);
    }

    [Test]
    public void TestPrimaryKeyExists()
    {
        var table = server.Databases.First().Tables.First();
        bool exists = false;
        foreach (Column column in table.Columns)
        {
            if (column.IsPrimaryKey)
            {
                Console.Write(column.Name);
                exists = true; break;
            }
        }
        Assert.That(exists, Is.True);
    }
}
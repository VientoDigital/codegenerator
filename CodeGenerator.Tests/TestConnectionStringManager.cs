namespace CodeGenerator.Tests;

[TestFixture]
public class TestConnectionStringManager
{
    [Test]
    public void TestGetConnectionStrings()
    {
        Assert.That(ConfigFile.Instance.ConnectionStrings, Has.Count.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void TestAddConnection()
    {
        int numConn = ConfigFile.Instance.ConnectionStrings.Count;
        ConfigFile.Instance.ConnectionStrings.Add("Server=.;Database=master;Integrated Security=SSPI;TrustServerCertificate=True");
        ConfigFile.Instance.Save();
        Assert.That(numConn, Is.EqualTo((ConfigFile.Instance.ConnectionStrings.Count - 1)));
    }
}
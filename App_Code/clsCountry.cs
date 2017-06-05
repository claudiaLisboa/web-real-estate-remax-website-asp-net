public class Country
{
    private int _countryId;
    private string _name;

    // Default constructor
    public Country()
    {
        _countryId = default(int);
        _name = string.Empty;
    }

    public int CountryId
    {
        get { return _countryId; }
        set { _countryId = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}

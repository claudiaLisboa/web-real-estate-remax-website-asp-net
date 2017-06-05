public class City
{
    private int _cityId;
    private string _name;

    // Default constructor
    public City()
    {
        _cityId = default(int);
        _name = string.Empty;
    }

    public int CityId
    {
        get { return _cityId; }
        set { _cityId = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}

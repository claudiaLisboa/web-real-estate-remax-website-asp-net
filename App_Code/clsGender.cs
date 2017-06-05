public class Gender
{
    private int _genderId;
    private string _description;

    // Default constructor
    public Gender()
    {
        _genderId = default(int);
        _description = string.Empty;
    }

    public int GenderId
    {
        get { return _genderId; }
        set { _genderId = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}

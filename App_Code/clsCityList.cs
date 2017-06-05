using System.Collections.Generic;
using System.Data;
using System.Linq;

public class CityList
{
    private Dictionary<int, City> _list;
    private DataSet _ds;

    // Default constructor
    public CityList()
    {
        LoadCities();
    }

    private void LoadCities()
    {
        _list = new Dictionary<int, City>();
        _ds = DataSource.GetCities();

        // Any error occurred?
        if (DataSource.ErrorCode() != 0)
        {
            //string msg = $"{DataSource.ErrorCode()} - {DataSource.ErrorMessage()}";
            //throw new System.ApplicationException(msg);

            return;
        }

        if ((_ds == null) || (_ds.Tables[0].Rows.Count == 0))
        {
            // There are no Cities.
            return;
        }

        foreach (DataRow row in _ds.Tables[0].Rows)
        {
            City city = new City();
            city.CityId = (int)row["CityId"];
            city.Name = row["CityName"].ToString();

            _list.Add(city.CityId, city);
        }
    }

    public DataSet AsDataSet()
    {
        return _ds;
    }

    public Dictionary<int, City>.ValueCollection Elements
    {
        get
        {
            return _list.Values;
        }
    }

    public Dictionary<int, City>.KeyCollection Keys
    {
        get
        {
            return _list.Keys;
        }
    }

    public int Quantity
    {
        get
        {
            return _list.Values.Count();
        }
    }

    public bool Exists(int cityId)
    {
        return _list.ContainsKey(cityId);
    }

    public City FindByCityId(int cityId)
    {
        if (Exists(cityId) == true)
        {
            return _list[cityId];
        }
        else
        {
            return null;
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Linq;

public class CountryList
{
    private Dictionary<int, Country> _list;
    private DataSet _ds;

    // Default constructor
    public CountryList()
    {
        LoadCountries();
    }

    private void LoadCountries()
    {
        _list = new Dictionary<int, Country>();
        _ds = DataSource.GetCountries();

        // Any error occurred?
        if (DataSource.ErrorCode() != 0)
        {
            //string msg = $"{DataSource.ErrorCode()} - {DataSource.ErrorMessage()}";
            //throw new System.ApplicationException(msg);

            return;
        }

        if ((_ds == null) || (_ds.Tables[0].Rows.Count == 0))
        {
            // There are no Countries.
            return;
        }

        foreach (DataRow row in _ds.Tables[0].Rows)
        {
            Country country = new Country();
            country.CountryId = (int)row["CountryId"];
            country.Name = row["CountryName"].ToString();

            _list.Add(country.CountryId, country);
        }
    }

    public DataSet AsDataSet()
    {
        return _ds;
    }

    public Dictionary<int, Country>.ValueCollection Elements
    {
        get
        {
            return _list.Values;
        }
    }

    public Dictionary<int, Country>.KeyCollection Keys
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

    public bool Exists(int countryId)
    {
        return _list.ContainsKey(countryId);
    }

    public Country FindByCountryId(int countryId)
    {
        if (Exists(countryId) == true)
        {
            return _list[countryId];
        }
        else
        {
            return null;
        }
    }
}

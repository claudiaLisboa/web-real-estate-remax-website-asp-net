using System.Collections.Generic;
using System.Data;
using System.Linq;

public class GenderList
{
    private Dictionary<int, Gender> _list;
    private DataSet _ds;

    // Default constructor
    public GenderList()
    {
        LoadGenders();
    }

    private void LoadGenders()
    {
        _list = new Dictionary<int, Gender>();
        _ds = DataSource.GetGenders();

        // Any error occurred?
        if (DataSource.ErrorCode() != 0)
        {
            //string msg = $"{DataSource.ErrorCode()} - {DataSource.ErrorMessage()}";
            //throw new System.ApplicationException(msg);

            return;
        }

        if ((_ds == null) || (_ds.Tables[0].Rows.Count == 0))
        {
            // There are no Genders.
            return;
        }

        foreach (DataRow row in _ds.Tables[0].Rows)
        {
            Gender gender = new Gender();
            gender.GenderId = (int)row["GenderId"];
            gender.Description = row["Description"].ToString();

            _list.Add(gender.GenderId, gender);
        }
    }

    public DataSet AsDataSet()
    {
        return _ds;
    }

    public Dictionary<int, Gender>.ValueCollection Elements
    {
        get
        {
            return _list.Values;
        }
    }

    public Dictionary<int, Gender>.KeyCollection Keys
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
}

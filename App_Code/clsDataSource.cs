using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Data;
using System.Data.OleDb;
using System.IO;

public static class DataSource
{
    private static OleDbConnection _dbConnection;

    private static int _errorCode = 0;
    private static string _errorMessage = string.Empty;

    public static bool Init()
    {
        ClearError();

        string dbPath = HostingEnvironment.MapPath("~/App_Data/dbRemax.mdb");

        if (!File.Exists(dbPath))
        {
            _errorCode = -1;
            _errorMessage = "The application database could not be found at the location below:\n\n" + dbPath;
            return false;
        }

        _dbConnection = null;
        _dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath);
        try
        {
            _dbConnection.Open();
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return false;
        }

        return true;
    }

    public static int ErrorCode()
    {
        return _errorCode;
    }

    public static string ErrorMessage()
    {
        return _errorMessage;
    }

    private static void ClearError()
    {
        _errorCode = 0;
        _errorMessage = string.Empty;
    }

    #region Client
    public static DataSet GetClients()
    {
        ClearError();

        string cmdText = "select ClientId, FirstName, LastName, BirthDate, Email, Phone, ClientTypeId, AgentId ";
        cmdText += "from Clients ";
        cmdText += "order by ClientId ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }

    public static int AddClient(string firstName, string lastName, DateTime birthDate,
        string email, string phone, int clientTypeId, int agentId)
    {
        ClearError();

        string cmdString = "insert into Clients (FirstName, LastName, BirthDate, Email, Phone, ClientTypeId, AgentId) ";
        cmdString += "values (@FirstName, @LastName, @BirthDate, @Email, @Phone, @ClientTypeId, @AgentId) ";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@BirthDate", birthDate);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@ClientTypeId", Convert.ToInt32(clientTypeId));
            cmd.Parameters.AddWithValue("@AgentId", Convert.ToInt32(agentId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }

    public static int DeleteClient(int clientId)
    {
        ClearError();

        string cmdString = "delete from Clients where ClientId = @ClientId";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@ClientId", Convert.ToInt32(clientId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }

    public static int UpdateClient(int clientId, string firstName, string lastName, DateTime birthDate,
        string email, string phone, int clientTypeId, int agentId)
    {
        ClearError();

        string cmdString = "update Clients set FirstName=@FirstName, LastName=@LastName, BirthDate=@BirthDate, ";
        cmdString += "Email=@Email, Phone=@Phone, ClientTypeId=@ClientTypeId, AgentId=@AgentId ";
        cmdString += "where ClientId=@ClientId ";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@BirthDate", birthDate);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@ClientTypeId", Convert.ToInt32(clientTypeId));
            cmd.Parameters.AddWithValue("@AgentId", Convert.ToInt32(agentId));
            cmd.Parameters.AddWithValue("@ClientId", Convert.ToInt32(clientId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }
    #endregion

    #region ClientType
    public static DataSet GetClientTypes()
    {
        ClearError();

        string cmdText = "select ClientTypeId, Description ";
        cmdText += "from ClientTypes ";
        cmdText += "order by ClientTypeId ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }
    #endregion

    #region Employee
    public static DataSet GetEmployees(int employeeId = -1, string firstName = "", string lastName = "",
        DateTime birthDate = default(DateTime), string email = "", int genderId = -1, string phone = "",
        int cityId = -1, int positionId = -1, double salary = -1, string username = "")
    {
        ClearError();

        try
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = _dbConnection;

            string cmdText = "select Employees.EmployeeId, Employees.FirstName, Employees.LastName, Employees.BirthDate, ";
            cmdText += "Employees.Email, Employees.GenderId, Genders.Description, Employees.Phone, Employees.CityId, Cities.CityName, ";
            cmdText += "Employees.PositionId, Positions.Description, Employees.Salary, Employees.Username, Employees.UserPassword, ";
            cmdText += "PositionPermissions.ManageEmployees, ";
            cmdText += "PositionPermissions.ManageAllClients, PositionPermissions.ManageOwnClients, ";
            cmdText += "PositionPermissions.ManageAllHouses, PositionPermissions.ManageOwnHouses, ";
            cmdText += "PositionPermissions.ManageAllSales, PositionPermissions.ManageOwnSales ";
            cmdText += "from Genders inner join ";
            cmdText += "((Positions inner join ";
            cmdText += "(Cities inner join Employees on Cities.CityId = Employees.CityId) ";
            cmdText += "on Positions.PositionId = Employees.PositionId) ";
            cmdText += "inner join PositionPermissions ";
            cmdText += "on Positions.PositionId = PositionPermissions.PositionId) ";
            cmdText += "on Genders.GenderId = Employees.GenderId ";
            cmdText += "where 1 = 1 ";

            // Employee Id
            if (employeeId != -1)
            {
                cmdText += "and Employees.EmployeeId=@EmployeeId ";
                cmd.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32(employeeId));
            }
            // First Name
            if (firstName != string.Empty)
            {
                cmdText += "and Employees.FirstName like '*' + @FirstName + '*' ";
                cmd.Parameters.AddWithValue("@FirstName", firstName);
            }
            // Last Name
            if (lastName != string.Empty)
            {
                cmdText += "and Employees.LastName like '*' + @lastName + '*' ";
                cmd.Parameters.AddWithValue("@LastName", lastName);
            }
            // Email
            if (email != string.Empty)
            {
                cmdText += "and Employees.Email like '*' + @Email + '*' ";
                cmd.Parameters.AddWithValue("@Email", email);
            }
            // Gender Id
            if (genderId != -1)
            {
                cmdText += "and Employees.GenderId=@GenderId ";
                cmd.Parameters.AddWithValue("@GenderId", Convert.ToInt32(genderId));
            }
            // Phone
            if (phone != string.Empty)
            {
                cmdText += "and Employees.Phone like '*' + @Phone + '*' ";
                cmd.Parameters.AddWithValue("@Phone", phone);
            }
            // City Id
            if (cityId != -1)
            {
                cmdText += "and Employees.CityId=@CityId ";
                cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityId));
            }
            // Position Id
            if (positionId != -1)
            {
                cmdText += "and Employees.PositionId=@PositionId ";
                cmd.Parameters.AddWithValue("@PositionId", Convert.ToInt32(positionId));
            }
            // Salary
            if (salary != -1)
            {
                cmdText += "and Employees.Salary=@Salary ";
                cmd.Parameters.AddWithValue("@Salary", Convert.ToDouble(salary));
            }
            // Username
            if (username != string.Empty)
            {
                cmdText += "and Employees.Username like '*' + @Username + '*' ";
                cmd.Parameters.AddWithValue("@Username", username);
            }
            cmdText += "order by Employees.EmployeeId ";

            cmd.CommandText = cmdText;
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }

    public static int AddEmployee(string firstName, string lastName, DateTime birthDate,
        string email, int genderId , string phone, int cityId, int positionId, double salary, string username, string userPassword)
    {
        ClearError();

        string cmdString = "insert into Employees (FirstName, LastName, BirthDate, Email, GenderId, Phone, CityId, PositionId, Salary, Username, UserPassword) ";
        cmdString += "values (@FirstName, @LastName, @BirthDate, @Email, @GenderId, @Phone, @CityId, @PositionId, @Salary, @Username, @UserPassword) ";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@BirthDate", birthDate);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@GenderId", Convert.ToInt32(genderId));
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityId));
            cmd.Parameters.AddWithValue("@PositionId", Convert.ToInt32(positionId));
            cmd.Parameters.AddWithValue("@Salary", Convert.ToDouble(salary));
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@UserPassword", userPassword);

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }

    public static int DeleteEmployee(int employeeId)
    {
        ClearError();

        string cmdString = "delete from Employees where EmployeeId = @EmployeeId";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32(employeeId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }

    public static int UpdateEmployee(int employeeId, string firstName, string lastName, DateTime birthDate,
        string email, int genderId, string phone, int cityId, int positionId, double salary)
    {
        ClearError();

        string cmdString = "update Employees set FirstName=@FirstName, LastName=@LastName, BirthDate=@BirthDate, ";
        cmdString += "Email=@Email, GenderId=@GenderId, Phone=@Phone, CityId=@CityId, PositionId=@PositionId, Salary=@Salary ";
        cmdString += "where EmployeeId=@EmployeeId";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@BirthDate", birthDate);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@GenderId", Convert.ToInt32(genderId));
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityId));
            cmd.Parameters.AddWithValue("@PositionId", Convert.ToInt32(positionId));
            cmd.Parameters.AddWithValue("@Salary", Convert.ToDouble(salary));
            cmd.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32(employeeId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }
    #endregion

    #region Position
    public static DataSet GetPositions()
    {
        ClearError();

        string cmdText = "select PositionId, Description ";
        cmdText += "from Positions ";
        cmdText += "order by PositionId ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }
    #endregion

    #region House
    public static DataSet GetHouses()
    {
        ClearError();

        string cmdText = "select Houses.HouseId, Houses.BuildingTypeId, BuildingTypes.Description as BuildingType, ";
        cmdText += "Houses.Street, Houses.ApartmentNo, Houses.PostalCode, Houses.CityId, Cities.CityName, ";
        cmdText += "Houses.CountryId, Countries.CountryName, Houses.MakingYear, Houses.Bathrooms, Houses.Bedrooms, ";
        cmdText += "Houses.Area, Houses.Description, Houses.Price, Houses.Tax, ";
        cmdText += "Houses.SellerId, Clients.FirstName + ' ' +  Clients.LastName as FullName";
        cmdText += "from Clients inner join ";
        cmdText += "(Countries inner join ";
        cmdText += "(Cities inner join ";
        cmdText += "(BuildingTypes inner join Houses on BuildingTypes.BuildingTypeId = Houses.BuildingTypeId) ";
        cmdText += "on Cities.CityId = Houses.CityId) ";
        cmdText += "on Countries.CountryId = Houses.CountryId) ";
        cmdText += "on Clients.ClientId = Houses.SellerId ";
        cmdText += "order by HouseId ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }

    public static DataSet GetHouses(int buildingTypeId = -1, string street = "", string apartmentNo = "", string postalCode = "",
        int cityId = -1, int countryId = -1, string makingYear = "", int bathrooms = -1, int bedrooms = -1, double area = -1,
        string description = "", double priceMin = -1, double priceMax = -1, int sellerId = -1)
    {
        ClearError();

        try
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = _dbConnection;

            string cmdText = "select Houses.HouseId, Houses.BuildingTypeId, BuildingTypes.Description as BuildingType, ";
            cmdText += "Houses.Street, Houses.ApartmentNo, Houses.PostalCode, Houses.CityId, Cities.CityName, ";
            cmdText += "Houses.CountryId, Countries.CountryName, Houses.MakingYear, Houses.Bathrooms, Houses.Bedrooms, ";
            cmdText += "Houses.Area, Houses.Description, Houses.Price, Houses.Tax, ";
            cmdText += "Houses.SellerId, Clients.FirstName + ' ' +  Clients.LastName as FullName ";
            cmdText += "from Clients inner join ";
            cmdText += "(Countries inner join ";
            cmdText += "(Cities inner join ";
            cmdText += "(BuildingTypes inner join Houses on BuildingTypes.BuildingTypeId = Houses.BuildingTypeId) ";
            cmdText += "on Cities.CityId = Houses.CityId) ";
            cmdText += "on Countries.CountryId = Houses.CountryId) ";
            cmdText += "on Clients.ClientId = Houses.SellerId ";
            cmdText += "where 1 = 1 ";

            // Building Type
            if (buildingTypeId != -1)
            {
                cmdText += "and Houses.BuildingTypeId=@BuildingTypeId ";
                cmd.Parameters.AddWithValue("@BuildingTypeId", Convert.ToInt32(buildingTypeId));
            }
            // Street
            if (street != string.Empty)
            {
                cmdText += "and Houses.Street like '*' + @Street + '*' ";
                cmd.Parameters.AddWithValue("@Street", street);
            }
            // Apartment No
            if (apartmentNo != string.Empty)
            {
                cmdText += "and Houses.ApartmentNo like '*' + @ApartmentNo + '*' ";
                cmd.Parameters.AddWithValue("@ApartmentNo", apartmentNo);
            }
            // Postal Code
            if (postalCode != string.Empty)
            {
                cmdText += "and Houses.PostalCode like '*' + @PostalCode + '*' ";
                cmd.Parameters.AddWithValue("@PostalCode", postalCode);
            }
            // City
            if (cityId != -1)
            {
                cmdText += "and Houses.CityId=@CityId ";
                cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityId));
            }
            // Country
            if (countryId != -1)
            {
                cmdText += "and Houses.CountryId=@CountryId ";
                cmd.Parameters.AddWithValue("@CountryId", Convert.ToInt32(countryId));
            }
            // Making Year
            if (makingYear != string.Empty)
            {
                cmdText += "and Houses.MakingYear like '*' + @MakingYear + '*' ";
                cmd.Parameters.AddWithValue("@MakingYear", makingYear);
            }
            // Bathrooms
            if (bathrooms != -1)
            {
                cmdText += "and Houses.Bathrooms=@Bathrooms ";
                cmd.Parameters.AddWithValue("@Bathrooms", Convert.ToInt32(bathrooms));
            }
            // Bedrooms
            if (bedrooms != -1)
            {
                cmdText += "and Houses.Bedrooms=@Bedrooms ";
                cmd.Parameters.AddWithValue("@Bedrooms", Convert.ToInt32(bedrooms));
            }
            // Area
            if (area != -1)
            {
                cmdText += "and Houses.Area=@Area ";
                cmd.Parameters.AddWithValue("@Area", Convert.ToDouble(area));
            }
            // Description
            if (description != string.Empty)
            {
                cmdText += "and Houses.Description like '*' + @Description + '*' ";
                cmd.Parameters.AddWithValue("@Description", description);
            }
            // Seller Id
            if (sellerId != -1)
            {
                cmdText += "and Houses.SellerId=@SellerId ";
                cmd.Parameters.AddWithValue("@SellerId", Convert.ToInt32(sellerId));
            }
            cmdText += "order by Houses.HouseId ";

            cmd.CommandText = cmdText;
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }

    public static int AddHouse(int buildingTypeId, string street, string apartmentNo, string postalCode, int cityId, int countryId,
        string makingYear, int bathrooms, int bedrooms, double area, string description, double price, double tax, int sellerId)
    {
        ClearError();

        string cmdString = "insert into Houses (BuildingTypeId, Street, ApartmentNo, PostalCode, CityId, ";
        cmdString += "CountryId, MakingYear, Bathrooms, Bedrooms, Area, Description, Price, Tax, SellerId) ";
        cmdString += "values (@BuildingTypeId, @Street, @ApartmentNo, @PostalCode, @CityId, ";
        cmdString += "@CountryId, @MakingYear, @Bathrooms, @Bedrooms, @Area, @Description, @Price, @Tax, @SellerId) ";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@BuildingTypeId", Convert.ToInt32(buildingTypeId));
            cmd.Parameters.AddWithValue("@Street", street);
            cmd.Parameters.AddWithValue("@ApartmentNo", apartmentNo);
            cmd.Parameters.AddWithValue("@PostalCode", postalCode);
            cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityId));
            cmd.Parameters.AddWithValue("@CountryId", Convert.ToInt32(countryId));
            cmd.Parameters.AddWithValue("@MakingYear", makingYear);
            cmd.Parameters.AddWithValue("@Bathrooms", Convert.ToInt32(bathrooms));
            cmd.Parameters.AddWithValue("@Bedrooms", Convert.ToInt32(bedrooms));
            cmd.Parameters.AddWithValue("@Area", area);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Tax", tax);
            cmd.Parameters.AddWithValue("@SellerId", Convert.ToInt32(sellerId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }

    public static int DeleteHouse(int houseId)
    {
        ClearError();

        string cmdString = "delete from Houses where HouseId = @HouseId";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@HouseId", Convert.ToInt32(houseId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }

    public static int UpdateHouse(int houseId, int buildingTypeId, string street, string apartmentNo, string postalCode,
        int cityId, int countryId, string makingYear, int bathrooms, int bedrooms, double area, string description,
        double price, double tax, int sellerId)
    {
        ClearError();

        string cmdString = "update Houses set BuildingTypeId=@BuildingTypeId, Street=@Street, ApartmentNo=@ApartmentNo, ";
        cmdString += "PostalCode=@PostalCode, CityId=@CityId, Country=@CountryId, MakingYear=@MakingYear, Bathrooms=@Bathrooms, ";
        cmdString += "Bedrooms=@Bedrooms, Area=@Area, Description=@Description, Price=@Price, Tax=@Tax, SellerId=@SellerId ";
        cmdString += "where HouseId=@HouseId";

        try
        {
            OleDbCommand cmd = new OleDbCommand(cmdString, _dbConnection);
            cmd.Parameters.AddWithValue("@BuildingTypeId", Convert.ToInt32(buildingTypeId));
            cmd.Parameters.AddWithValue("@Street", street);
            cmd.Parameters.AddWithValue("@ApartmentNo", apartmentNo);
            cmd.Parameters.AddWithValue("@PostalCode", postalCode);
            cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityId));
            cmd.Parameters.AddWithValue("@CountryId", Convert.ToInt32(countryId));
            cmd.Parameters.AddWithValue("@MakingYear", makingYear);
            cmd.Parameters.AddWithValue("@Bathrooms", Convert.ToInt32(bathrooms));
            cmd.Parameters.AddWithValue("@Bedrooms", Convert.ToInt32(bedrooms));
            cmd.Parameters.AddWithValue("@Area", area);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Tax", tax);
            cmd.Parameters.AddWithValue("@SellerId", Convert.ToInt32(sellerId));
            cmd.Parameters.AddWithValue("@HouseId", Convert.ToInt32(houseId));

            // Returning the number of affected rows.
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return 0;
        }
    }
    #endregion

    #region BuildingType
    public static DataSet GetBuildingTypes()
    {
        ClearError();

        string cmdText = "select BuildingTypeId, Description ";
        cmdText += "from BuildingTypes ";
        cmdText += "order by Description ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }
    #endregion

    #region City
    public static DataSet GetCities()
    {
        ClearError();

        string cmdText = "select CityId, CityName ";
        cmdText += "from Cities ";
        cmdText += "order by CityName ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }
    #endregion

    #region Country
    public static DataSet GetCountries()
    {
        ClearError();

        string cmdText = "select CountryId, CountryName ";
        cmdText += "from Countries ";
        cmdText += "order by CountryName ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }
    #endregion

    #region Gender
    public static DataSet GetGenders()
    {
        ClearError();

        string cmdText = "select GenderId, Description ";
        cmdText += "from Genders ";
        cmdText += "order by Description ";

        try
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(cmdText, _dbConnection);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            return ds;
        }
        catch (Exception err)
        {
            _errorCode = err.HResult;
            _errorMessage = err.Message;
            return null;
        }
    }
    #endregion
}

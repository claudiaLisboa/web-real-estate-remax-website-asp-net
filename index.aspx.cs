using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;

public partial class index : System.Web.UI.Page
{
    OleDbConnection myCon;
    OleDbCommand myCmd;
    public static DataSet mySet;
    public static OleDbDataAdapter adpHouses;
    static DataTable tbHouses;
    static string myQuery;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool initOK = DataSource.Init();

        if (!IsPostBack)
        {
            /*- SECTION :Main Search*/

            // ** Building Types
            DataSet dsBuildingTypes = (new HouseList()).GetBuildingTypes();
            cboType.DataValueField = "BuildingTypeId";
            cboType.DataTextField = "Description";
            cboType.DataSource = dsBuildingTypes;
            cboType.DataBind();
            // From: http://stackoverflow.com/questions/20700473/how-to-add-a-default-select-option-to-this-asp-net-dropdownlist-control
            cboType.Items.Insert(0, new ListItem("-- Choose --", ""));

            // ** Cities
            DataSet dsCities = (new CityList()).AsDataSet();
            cboHouseCity.DataValueField = "CityId";
            cboHouseCity.DataTextField = "CityName";
            cboHouseCity.DataSource = dsCities;
            cboHouseCity.DataBind();
            cboHouseCity.Items.Insert(0, new ListItem("-- Choose --", ""));

            cboAgentCity.DataValueField = "CityId";
            cboAgentCity.DataTextField = "CityName";
            cboAgentCity.DataSource = dsCities;
            cboAgentCity.DataBind();
            cboAgentCity.Items.Insert(0, new ListItem("-- Choose --", ""));

            // ** Gender
            DataSet dsGenders = (new GenderList()).AsDataSet();
            cboGender.DataValueField = "GenderId";
            cboGender.DataTextField = "Description";
            cboGender.DataSource = dsGenders;
            cboGender.DataBind();
            cboGender.Items.Insert(0, new ListItem("-- Choose --", ""));

            ///*- SECTION :Search house by Agent*/
            //myCmd = new OleDbCommand("SELECT FirstName + ' ' + LastName AS FullName, EmployeeId FROM (Positions INNER JOIN Employees ON Positions.PositionId = Employees.PositionId) WHERE Positions.Description = 'Agent'", myCon);
            //OleDbDataReader rdAgent = myCmd.ExecuteReader();
            //ListBoxAgent.DataTextField = "FullName";
            //ListBoxAgent.DataValueField = "EmployeeId";
            //ListBoxAgent.DataSource = rdAgent;
            //ListBoxAgent.DataBind();
        }
    }

    private int getIntValue(string stringValue, int defaultValue)
    {
        int returnValue = defaultValue;

        if (stringValue != string.Empty)
        {
            int.TryParse(stringValue, out returnValue);
        }

        return returnValue;
    }

    protected void btnSearchHouse_Click(object sender, EventArgs e)
    {
        // Currently available search fields.
        int buildingTypeId = getIntValue(cboType.SelectedValue, -1);
        string postalCode = txtPostalCode.Text;
        int cityId = getIntValue(cboHouseCity.SelectedValue, -1);
        int bedrooms = getIntValue(cboBedrooms.SelectedValue, -1);
        double priceMin = -1;
        double priceMax = -1;
        // Currently non-available search fields.
        string street = string.Empty;
        string apartmentNo = string.Empty;
        int countryId = -1;
        string makingYear = string.Empty;
        int bathrooms = -1;
        double area = -1;
        string description = string.Empty;
        int sellerId = -1;

        HouseList houseList = new HouseList(buildingTypeId, street, apartmentNo, postalCode, cityId, countryId,
            makingYear, bathrooms, bedrooms, area, description, priceMin, priceMax, sellerId);

        displayHouses(houseList.AsDataSet());

        return;
    }

    private void displayHouses(DataSet ds)
    {
        //Building an HTML string.
        StringBuilder html = new StringBuilder();

        if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
        {
            //Building the Data rows.
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                html.Append("<div class='col-md-12' style='background-color: lightgrey; border-radius: 25px;'>");
                html.Append("<table >");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("CODE: " + row["HouseId"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                //html.Append("<td>");
                //html.Append("<img src='" + row["Picture"].ToString() + "' height='150' width='150'>");
                //html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("$" + row["Price"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(row["Description"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(row["CityName"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("ROOMS: " + row["Bedrooms"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                string houseCode = row["HouseId"].ToString();
                html.Append("<td>");
                html.Append("<a href='house.aspx?MyCode=" + houseCode + "'>See Details</a>");
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("</table>");
                html.Append("</div>");
            }
        }
        else
        {
            string msgNoResult = "<div class=\"alert alert-info alert-dismissable\">";
            msgNoResult += "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>";
            msgNoResult += "Sorry, no results for your search.</div>";
            html.Append(msgNoResult);
        }

        LiteralResult.Text = html.ToString();
    }

    protected void ListBoxAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        myQuery = "SELECT Houses.Code AS Code, Houses.MainPicture AS Picture, Houses.Price AS Price, Houses.Description AS Description, Houses.NbOfRooms AS Rooms, City.CityName AS City, Houses.Owner, ClientsSellers.Agent FROM (((Agent INNER JOIN ClientsSellers ON Agent.AgentId = ClientsSellers.Agent) INNER JOIN Houses ON ClientsSellers.sellerId = Houses.Owner) INNER JOIN City ON Houses.City = City.CityId) WHERE (ClientsSellers.Agent = @myAG)";
        myCmd = new OleDbCommand(myQuery, myCon);
        //SEARCH parameters
        myCmd.Parameters.AddWithValue("myAG", Convert.ToInt32(ListBoxAgent.SelectedValue));
        myCmd.Parameters["myAG"].OleDbType = OleDbType.Integer;

        //displaySelectHouses(SelectHouses(myCmd));
    }

    protected void btnSearchAgent_Click(object sender, EventArgs e)
    {
        // Currently available search fields.
        int genderId = getIntValue(cboGender.SelectedValue, -1);
        int cityId = getIntValue(cboAgentCity.SelectedValue, -1);
        // Currently non-available search fields.
        int employeeId = -1;
        string firstName = string.Empty;
        string lastName = string.Empty;
        DateTime birthDate = default(DateTime);
        string email = string.Empty;
        string phone = string.Empty;
        double salary = -1;
        string username = string.Empty;

        // The positionId: parameter is set to (int)Position.Agent in order to filter for Agents only.
        EmployeeList agentList = new EmployeeList(employeeId, firstName, lastName, birthDate, email, genderId, phone, cityId,
            (int)Position.Agent, salary, username);

        DataSet dsAgents = agentList.AsDataSet();
        if ((dsAgents != null) && (dsAgents.Tables[0].Rows.Count > 0))
        {
            GridResult.DataSource = dsAgents;
            GridResult.DataBind();

            LiteralResult.Text = "";
        }
        else
        {
            GridResult.DataSource = "";

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            string msgNoResult = "<div class=\"alert alert-info alert-dismissable\">";
            msgNoResult += "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>";
            msgNoResult += "Sorry, no results for your search.</div>";

            html.Append(msgNoResult);

            LiteralResult.Text = html.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;

public partial class house : System.Web.UI.Page
{

    OleDbConnection myCon;
    OleDbCommand myCmd;
    public static DataSet mySet2;
    public static OleDbDataAdapter adpHouses2;
    static DataTable tbHouses2;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    
        String houseCode = Request.QueryString["MyCode"];
        myCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("./App_Data/dbRemax.mdb"));
        myCon.Open();

        if (!IsPostBack)
        {
            myCmd = new OleDbCommand("SELECT Houses.Code AS Code, Houses.Price AS Price, Houses.Description AS Description, Houses.NbOfRooms AS Rooms, Houses.MainPicture AS Picture, City.CityName AS City FROM (City INNER JOIN Houses ON City.CityId = Houses.City) WHERE (Code = @myCode)", myCon);

            myCmd.Parameters.AddWithValue("myCode", houseCode);
            myCmd.Parameters["myCode"].OleDbType = OleDbType.BSTR;
            mySet2 = new DataSet();
            adpHouses2 = new OleDbDataAdapter(myCmd);
            adpHouses2.Fill(mySet2, "Houses");
            tbHouses2 = mySet2.Tables["Houses"];
            displaySelectHouses(tbHouses2);
        }
    }
    private void displaySelectHouses(DataTable myTable)
    {
        //Building an HTML string.
        StringBuilder html = new StringBuilder();

        int curr = 0;
        if (myTable.Rows.Count > 0)
        {
            //Building the Data rows.
            foreach (DataRow row in myTable.Rows)
            {

                html.Append("<div class='col-md-2' style='background-color: lightgrey; border-radius: 25px;'>");
                html.Append("<table >");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("CODE: " + myTable.Rows[curr]["Code"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("<img src='" + myTable.Rows[curr]["Picture"].ToString() + "' height='300' width='300'>");
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("$" + myTable.Rows[curr]["Price"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(myTable.Rows[curr]["Description"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(myTable.Rows[curr]["City"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("ROOMS: " + myTable.Rows[curr]["Rooms"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("</table>");
                html.Append("</div>");
                curr++;
            }
        }
        else
        {
            html.Append("Sorry, no results for your search");
        }


        myLabel.Text = html.ToString();
    }

}
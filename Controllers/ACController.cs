using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ACProject.Models;
using System.Data;
using Microsoft.Data.SqlClient;

public class ACController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=dffafdafebcfacbdcbaeaacbbeecfcbdfe-0;Database=ACEMP;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<AC> acs = new List<AC>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM AC";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AC ac = new AC();

                    ac.AC_Id = Convert.ToInt32(reader["AC_Id"]);
                    ac.Brand = reader["Brand"].ToString();
                    ac.Type = reader["Type"].ToString();
                    ac.Price = reader["Price"].ToString();

                    acs.Add(ac);
                }

                reader.Close();
            }
        }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
        return View(acs);

    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(AC ac)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO AC (Brand, Type, Price) VALUES (@Brand, @Type, @Price)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // command.Parameters.AddWithValue("@AC_Id", Book.AC_Id);
                command.Parameters.AddWithValue("@Brand", ac.Brand);
                command.Parameters.AddWithValue("@Type", ac.Type);
                command.Parameters.AddWithValue("@Price", ac.Price);


                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }
}

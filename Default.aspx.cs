using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Data;


namespace PreventivoAuto
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAuto();
                GetOptional();
            }
        }

        protected void ddlAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idAuto = int.Parse(ddlAuto.SelectedValue);
            var auto = GetAutoById(idAuto);

            if (auto != null)
            {
                imgAuto.ImageUrl = auto.Immagine;
                lblPrezzoBase.Text = $"Prezzo base: {auto.PrezzoBase:C2}";
            }
        }


        protected void btnCalcola_Click(object sender, EventArgs e)
        {
            int idAuto = int.Parse(ddlAuto.SelectedValue);
            var auto = GetAutoById(idAuto);
            decimal totaleOptional = 0;
            foreach (ListItem item in chkOptional.Items)
            {
                if (item.Selected)
                {
                    totaleOptional += GetOptionalPrezzoById(int.Parse(item.Value));
                }
            }
            int numeroAnniGaranzia = int.Parse(txtNumeroAnniGaranzia.Text);
            decimal totaleGaranzia = numeroAnniGaranzia * 120;
            decimal totale = auto.PrezzoBase + totaleOptional + totaleGaranzia;
            h3Totale.Text = $"Totale: {totale:C2}";
        }

        private Auto GetAutoById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Auto WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Auto
                    {
                        Id = (int)reader["Id"],
                        Modello = (string)reader["Modello"],
                        PrezzoBase = (decimal)reader["PrezzoBase"],
                        Immagine = (string)reader["Immagine"]
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        private decimal GetOptionalPrezzoById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Prezzo FROM Optional WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                return (decimal)command.ExecuteScalar();
            }
        }

        private string ConnectionString
        {
            get { return "Server=GABRIELE-PORTAT\\SQLEXPRESS;Initial Catalog=PreventivoAuto;Integrated Security=True;"; }
        }

        public class Auto
        {
            public int Id { get; set; }
            public string Modello { get; set; }
            public decimal PrezzoBase { get; set; }
            public string Immagine { get; set; }
        }

        public List<Auto> GetAuto()
        {
            List<Auto> autoList = new List<Auto>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Auto", connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                foreach (DataRow row in dataTable.Rows)
                {
                    Auto auto = new Auto
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Modello = Convert.ToString(row["Modello"]),
                        PrezzoBase = Convert.ToDecimal(row["PrezzoBase"]),
                        Immagine = Convert.ToString(row["Immagine"])
                    };

                    autoList.Add(auto);
                }
            }

            return autoList;
        }

        public class Optional
        {
            public int Id { get; set; }
            public string Descrizione { get; set; }
            public decimal Prezzo { get; set; }         
        }

        public List<Optional> GetOptional()
        {
            List<Optional> optionalList = new List<Optional>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Optional", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Optional optional = new Optional
                    {
                        Id = (int)reader["Id"],
                        Descrizione = (string)reader["Descrizione"],
                        Prezzo = (decimal)reader["Prezzo"],
                    };

                    optionalList.Add(optional);
                }
            }

            return optionalList;
        }

    }
}
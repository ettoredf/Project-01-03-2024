using Project_01_03_2024.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Project_01_03_2024.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("RegistraUser");
        }

        public ActionResult RegistraUtente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistraUser(User user)
        {
            if (ModelState.IsValid)
            {
                User userEsiste = CheckUser(user);

                if (userEsiste == null)
                {
                    using (SqlConnection conn = Connection.GetConn())
                    {
                        conn.Open();

                        string query = "INSERT INTO ANAGRAFICA (Cognome, Nome,  Indirizzo, Città, CAP, CF) VALUES (@Cognome,@Nome,  @Indirizzo, @Citta, @CAP, @CodiceFiscale)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nome", user.Nome);
                            cmd.Parameters.AddWithValue("@Cognome", user.Cognome);
                            cmd.Parameters.AddWithValue("@Indirizzo", user.Indirizzo);
                            cmd.Parameters.AddWithValue("@Citta", user.Citta);
                            cmd.Parameters.AddWithValue("@CAP", user.CAP);
                            cmd.Parameters.AddWithValue("@CodiceFiscale", user.CodiceFiscale);

                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Utente già registrato";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        private User CheckUser(User user)
        {
            User nuovoUser = new User();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA WHERE CF = @CodiceFiscale";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", user.CodiceFiscale);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            nuovoUser.IdUtente = reader.GetInt32(0);
                            nuovoUser.Cognome = reader.GetString(1);
                            nuovoUser.Nome = reader.GetString(2);
                            nuovoUser.Indirizzo = reader.GetString(3);
                            nuovoUser.Citta = reader.GetString(4);
                            nuovoUser.CAP = reader.GetString(5);
                            nuovoUser.CodiceFiscale = reader.GetString(6);

                            return nuovoUser;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }
    }
}
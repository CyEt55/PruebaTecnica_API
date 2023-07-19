using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace PruebaTecnica_API.Controllers
{
    [ApiController]
    [Route("api/contenedores")]
    public class ContenedorController : ControllerBase
    {
        public SqlConnection Connection = new SqlConnection("Data Source=.\\SQLSERVER;Initial Catalog=prueba_tecnica;Integrated Security=True;Encrypt=False");

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contenedor>>> GetContedores()
        {
            using (Connection)
            {
                List<Contenedor> contenedores = new List<Contenedor>();
                try
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("List_All", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Contenedor cont = new Contenedor();

                        cont.Id = Convert.ToInt32(reader[0].ToString());
                        cont.Codigo_Propietario = reader[1].ToString();
                        cont.Numero = reader[2].ToString();
                        cont.Tipo = reader[3].ToString();
                        cont.Tamanio = reader[4].ToString();
                        cont.Peso = reader[5].ToString();
                        cont.Tara = reader[6].ToString();

                        contenedores.Add(cont);
                    }

                    Connection.Close();
                }
                catch (SqlException)
                {
                    return BadRequest();
                }

                return contenedores;
            }


        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contenedor>> GetContenedor(int Id)
        {
            using (Connection)
            {
                Contenedor cont = new Contenedor();

                try
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("GetContenedor", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Id;

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        cont.Id = Convert.ToInt32(reader[0].ToString());
                        cont.Codigo_Propietario = reader[1].ToString();
                        cont.Numero = reader[2].ToString();
                        cont.Tipo = reader[3].ToString();
                        cont.Tamanio = reader[4].ToString();
                        cont.Peso = reader[5].ToString();
                        cont.Tara = reader[6].ToString();
                    }

                    Connection.Close();
                }
                catch (SqlException)
                {
                    return BadRequest();
                }

                return cont;
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutContenedor(int Id, Contenedor contenedor)
        {
            int col = 0;
            await using (Connection)
            {
                try
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("PutContenedor", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@codigo_propietario", SqlDbType.Int).Value = contenedor.Codigo_Propietario;
                    cmd.Parameters.AddWithValue("@numero", SqlDbType.Char).Value = contenedor.Numero;
                    cmd.Parameters.AddWithValue("@tipo", SqlDbType.VarChar).Value = contenedor.Tipo;
                    cmd.Parameters.AddWithValue("@tamanio", SqlDbType.Char).Value = contenedor.Tamanio;
                    cmd.Parameters.AddWithValue("@peso", SqlDbType.VarChar).Value = contenedor.Peso;
                    cmd.Parameters.AddWithValue("@tara", SqlDbType.VarChar).Value = contenedor.Tara;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Id;

                    col = cmd.ExecuteNonQuery();

                    Connection.Close();
                }
                catch (SqlException)
                {
                    return BadRequest();
                }

                return col;
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<int>> PostContenedor(Contenedor contenedor)
        {
            int col = 0;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("PostContenedor", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@codigo_propietario", SqlDbType.Int).Value = contenedor.Codigo_Propietario;
                    cmd.Parameters.AddWithValue("@numero", SqlDbType.Char).Value = contenedor.Numero;
                    cmd.Parameters.AddWithValue("@tipo", SqlDbType.VarChar).Value = contenedor.Tipo;
                    cmd.Parameters.AddWithValue("@tamanio", SqlDbType.Char).Value = contenedor.Tamanio;
                    cmd.Parameters.AddWithValue("@peso", SqlDbType.VarChar).Value = contenedor.Peso;
                    cmd.Parameters.AddWithValue("@tara", SqlDbType.VarChar).Value = contenedor.Tara;

                    col = cmd.ExecuteNonQuery();

                    Connection.Close();
                }
                catch (SqlException)
                {
                    return BadRequest();
                }

                return col;
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteContenedor(int Id)
        {
            int col = 0;
            await using (Connection)
            {
                try
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteContenedor", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Id;

                    col = cmd.ExecuteNonQuery();

                    Connection.Close();
                }
                catch (SqlException)
                {
                    return BadRequest();
                }

                return col;
            }
        }
    }
}
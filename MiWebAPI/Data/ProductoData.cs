using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {

        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ProductoDetalle>> Lista()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_LISTAR", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductoDetalle
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            nombre_producto = reader["NOMBRE_PRODUCTO"].ToString(),
                            precio = Convert.ToDecimal(reader["Precio"]),
                            cantidad = Convert.ToInt32(reader["Cantidad"]),
                            total = Convert.ToDecimal(reader["Total"]),
                            descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Empleado> Obtener(int Id)
        {
            Empleado objeto = new Empleado();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("dbo.CREAR_PRODUCTO", con);
                cmd.Parameters.AddWithValue("@nombre_producto", objeto.nombre_producto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio", objeto.precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.descripcion ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(long id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("ELIMINIAR_PRODUCTO", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public async Task<bool> Editar(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.CREAR_PRODUCTO", con);
                cmd.Parameters.AddWithValue("@nombre_producto", objeto.nombre_producto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio", objeto.precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.descripcion ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = true;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }
    }
}
using Dapper;
using Lavanderia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class BoletaRepository
{
    private readonly string connectionString;

    public BoletaRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void RegistrarBoletaConDetalles(Boleta boleta)
    {
        using (var conexion = new SqlConnection(connectionString))
        {
            conexion.Open();

            using (var command = new SqlCommand("RegistrarBoletaConDetalles", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros simples
                command.Parameters.AddWithValue("@ClienteID", boleta.ClienteID);
                command.Parameters.AddWithValue("@EmpleadoID", boleta.EmpleadoID);
                command.Parameters.AddWithValue("@SucursalID", boleta.SucursalID);
                command.Parameters.AddWithValue("@FechaEmision", boleta.FechaEmision);
                command.Parameters.AddWithValue("@FechaEntrega", boleta.FechaEntrega);

                // Param OUTPUT
                var boletaIdParam = new SqlParameter("@BoletaID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(boletaIdParam);

                // Crear DataTable para @Detalles
                var table = new DataTable();
                table.Columns.Add("PrendaNombre", typeof(string));
                table.Columns.Add("TipoCobro", typeof(string));
                table.Columns.Add("Peso", typeof(decimal));
                table.Columns.Add("Precio", typeof(decimal));

                foreach (var detalle in boleta.Detalles)
                {
                    table.Rows.Add(detalle.PrendaNombre, detalle.TipoCobro, detalle.Peso, detalle.Precio);
                }

                // Agregar tipo definido por usuario
                var detallesParam = command.Parameters.AddWithValue("@Detalles", table);
                detallesParam.SqlDbType = SqlDbType.Structured;
                detallesParam.TypeName = "TipoDetalleBoleta";

                command.ExecuteNonQuery();

                // Asignar el ID generado
                boleta.BoletaID = (int)boletaIdParam.Value;
            }
        }
    }
}

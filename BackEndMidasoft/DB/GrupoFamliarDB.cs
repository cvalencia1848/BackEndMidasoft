using BackEndMidasoft.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndMidasoft.DB
{
    public class GrupoFamliarDB
    {
        private string cadenaConexion { get; set; }
        public GrupoFamliarDB(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public async Task<List<GrupoFamiliar>> GetGrupoFamiliars(string Parametro, string Usuario)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_CRUD_GrupoFamiliar", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Parametro", Parametro));
                        cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                        cmd.Parameters.Add(new SqlParameter("@Cedula", ""));
                        cmd.Parameters.Add(new SqlParameter("@Nombres", ""));
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", ""));
                        cmd.Parameters.Add(new SqlParameter("@Genero",""));
                        cmd.Parameters.Add(new SqlParameter("@Parentesco", ""));
                        cmd.Parameters.Add(new SqlParameter("@Edad", ""));
                        cmd.Parameters.Add(new SqlParameter("@MenorEdad", ""));
                        cmd.Parameters.Add(new SqlParameter("@FechaNacimiento",""));

                        await sql.OpenAsync();

                        var respuesta = new List<GrupoFamiliar>();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                respuesta.Add(new GrupoFamiliar {
                                    Usuario = (string)reader["Usuario"],
                                    Cedula = (int)reader["Cedula"],
                                    Nombres = (string)reader["Nombres"],
                                    Apellidos = (string)reader["Apellidos"],
                                    Genero = (string)reader["Genero"],
                                    Parentesco = (string)reader["Parentesco"],
                                    Edad = (int)reader["Edad"],
                                    MenorEdad = Convert.ToString((int)reader["MenorEdad"]),
                                    FechaNacimiento = (DateTime)reader["FechaNacimiento"]
                            });
                                //respuesta = (string)reader["ESTADO"];
                            }
                        }
                        return respuesta;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> GrupoFamiliar(string Parametro, GrupoFamiliar grupoFamiliar)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection sql = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_CRUD_GrupoFamiliar", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Parametro", Parametro));
                        cmd.Parameters.Add(new SqlParameter("@Usuario", grupoFamiliar.Usuario));
                        cmd.Parameters.Add(new SqlParameter("@Cedula", grupoFamiliar.Cedula));
                        cmd.Parameters.Add(new SqlParameter("@Nombres", grupoFamiliar.Nombres));
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", grupoFamiliar.Apellidos));
                        cmd.Parameters.Add(new SqlParameter("@Genero", grupoFamiliar.Genero));
                        cmd.Parameters.Add(new SqlParameter("@Parentesco", grupoFamiliar.Parentesco));
                        cmd.Parameters.Add(new SqlParameter("@Edad", grupoFamiliar.Edad));
                        cmd.Parameters.Add(new SqlParameter("@MenorEdad", grupoFamiliar.MenorEdad));
                        cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", grupoFamiliar.FechaNacimiento));

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                respuesta = (string)reader["ESTADO"];
                            }
                        }
                        return respuesta;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

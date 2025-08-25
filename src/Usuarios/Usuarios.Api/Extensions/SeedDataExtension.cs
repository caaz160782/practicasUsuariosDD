using Bogus;
using Dapper;
using Usuarios.Application.Abstractions.Data;

namespace Usuarios.Api.Extensions;

public static class SeedDataExtension
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using var connection = sqlConnectionFactory.CreateConnection();

        List<object> roles;

        const string sqlCountRoles = "SELECT COUNT(*) FROM roles";

        if (connection.ExecuteScalar<int>(sqlCountRoles) <= 0)
        {
            roles = [
                new { Id = Guid.NewGuid() , NombreRol = "Docente", Descripcion = "Docente de aula" },
                new { Id = Guid.NewGuid() , NombreRol = "Estudiante", Descripcion = "Estudiante" }
            ];

            const string sqlInsertRoles = "INSERT INTO roles (id,nombre,descripcion) VALUES (@Id,@NombreRol,@Descripcion)";

            connection.Execute(sqlInsertRoles, roles);

            List<object> usuarios = new();

            for (int i = 0; i < 100; i++)
            {
                var fake = new Faker();
                int indexRol = fake.Random.Number(0, roles.Count - 1);
                var maxFN = fake.Date.Past(50);

                var usuario = new
                {
                    id = Guid.NewGuid(),
                    nombre_persona = fake.Person.FullName,
                    apellido_paterno = fake.Person.FirstName,
                    apellido_materno = fake.Person.FirstName,
                    password = fake.Person.UserName,
                    nombre_usuario = fake.Person.UserName,
                    fecha_ultimo_cambio = DateTime.UtcNow,
                    fecha_nacimiento = fake.Date.Between(maxFN, DateTime.UtcNow),
                    correo_electronico = fake.Person.Email,
                    direccion_pais = fake.Address.Country(),
                    direccion_departamento = fake.Address.State(),
                    direccion_provincia = fake.Address.City(),
                    direccion_distrito = fake.Address.Direction(),
                    direccion_calle = fake.Address.StreetName(),
                    estado = fake.PickRandom("Activo", "Inactivo"),
                    rol_id = (Guid)((dynamic)roles[indexRol]).Id
                };

                usuarios.Add(usuario);
            }

            const string sqlInsertarUsuarios = """
              INSERT INTO usuarios 
                (id, 
                nombre_persona, 
                apellido_paterno, 
                apellido_materno, 
                password, 
                nombre_usuario, 
                fecha_nacimiento, 
                correo_electronico, 
                direccion_pais, 
                direccion_departamento, 
                direccion_provincia, 
                direccion_distrito, 
                direccion_calle, 
                estado, 
                fecha_ultimo_cambio, 
                rol_id) 
            VALUES 
                (@id, 
                @nombre_persona, 
                @apellido_paterno, 
                @apellido_materno, 
                @password, 
                @nombre_usuario,
                @fecha_nacimiento, 
                @correo_electronico, 
                @direccion_pais, 
                @direccion_departamento, 
                @direccion_provincia, 
                @direccion_distrito, 
                @direccion_calle, 
                @estado, 
                @fecha_ultimo_cambio, 
                @rol_id);
            """;

            connection.Execute(sqlInsertarUsuarios, usuarios);

        }
    }
}
using System.Data.SqlClient;

namespace Z5.Animals
{
    public interface IAnimalRepository
    {
        public IEnumerable<Animal> FetchAllAnimals(string orderBy);
        public bool CreateAnimal(string name, string category, string area, string? description = null);
        public bool EditAnimal(int id, string name, string category, string area, string? description = null);
        public bool DeleteAnimal(int id);
    }


    public class AnimalRepository : IAnimalRepository
    {
        private readonly IConfiguration _animalRepository;

        public AnimalRepository(IConfiguration animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public IEnumerable<Animal> FetchAllAnimals(string orderBy)
        {
            using var connection = new SqlConnection(_animalRepository["ConnectionStrings:DefaultConnection"]);
            connection.Open();

            //Nwm czy to Id tu trzeba wpisywac
            var safeOrderBy = new string[] { "Id", "Name", "Description", "Category", "Area" }.Contains(orderBy) ? orderBy : "Name";
            using var command = new SqlCommand($"SELECT Id, Email FROM Students ORDER BY {safeOrderBy}", connection);
            using var reader = command.ExecuteReader();

            var animals = new List<Animal>();

            while (reader.Read())
            {
                var animal = new Animal()
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!,
                    Description = reader["Description"].ToString()!,
                    Category = reader["Category"].ToString()!,
                    Area = reader["Area"].ToString()!
                };

                animals.Add(animal);
            }
            return animals;
        }

        public bool CreateAnimal(string name, string category, string area, string? description = null)
        {
            using var connection = new SqlConnection(_animalRepository["ConnectionStrings:DefaultConnection"]);
            connection.Open();

            // Uwzględnij pola Description, Category i Area w poleceniu SQL
            using var command = new SqlCommand("INSERT INTO Animal (Name, Description, CATEGORY, AREA) VALUES (@name, @description, @category, @area)", connection);

            // Dodaj parametry komendy SQL
            command.Parameters.AddWithValue("@name", name);
            if (description != null)
                command.Parameters.AddWithValue("@description", description); 
            else
                command.Parameters.AddWithValue("@description", DBNull.Value); 
            command.Parameters.AddWithValue("@category", category); 
            command.Parameters.AddWithValue("@area", area); 

            var affectedRows = command.ExecuteNonQuery();
            return affectedRows == 1;
        }





        public bool EditAnimal(int id, string name, string category, string area, string? description = null)
        {
            using var connection = new SqlConnection(_animalRepository["ConnectionStrings:DefaultConnection"]);
            connection.Open();

            using var command = new SqlCommand("UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE Id = @id", connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            if (description != null)
                command.Parameters.AddWithValue("@description", description);
            else
                command.Parameters.AddWithValue("@description", DBNull.Value);
            command.Parameters.AddWithValue("@category", category);
            command.Parameters.AddWithValue("@area", area);

            var affectedRows = command.ExecuteNonQuery();
            return affectedRows == 1;
        }

        public bool DeleteAnimal(int id)
        {
            using var connection = new SqlConnection(_animalRepository["ConnectionStrings:DefaultConnection"]);
            connection.Open();

            using var command = new SqlCommand("DELETE FROM Animal WHERE Id = @id", connection);

            command.Parameters.AddWithValue("@id", id);

            var affectedRows = command.ExecuteNonQuery();
            return affectedRows == 1;
        }
    }
}

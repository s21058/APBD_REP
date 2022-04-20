using System.Data;
using System.Data.SqlClient;

namespace WebApplication_Animals;

public class AnimalsDb
{
    private static string conString;
    public AnimalsDb(string conString)
    {
        AnimalsDb.conString = conString;
    }

    public static List<Animals> getBy(string orderBy)
    {
        List<Animals> list = new List<Animals>();
        using (SqlConnection connection = new SqlConnection(conString))
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ANIMAL ORDER BY " + orderBy + " ASC";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Animals
                {
                    idAnimal = (int) reader["idAnimal"],
                    name = reader["Name"].ToString(),
                    description = reader["Description"].ToString(),
                    category = reader["Category"].ToString(),
                    area = reader["Area"].ToString()
                });
            }
        }

        return list;
    }

    public static void Add(Animals animals)
    {
        using (SqlConnection connection = new SqlConnection(conString))
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Insert into ANIMAL VALUES (@Name,@Description,@Category,@Area)";
            //(idAnimal,Name,Description,Category,Area)
            command.Parameters.AddWithValue("@Name", animals.name);
            command.Parameters.AddWithValue("@Description", animals.description);
            command.Parameters.AddWithValue("@Category", animals.category);
            command.Parameters.AddWithValue("@Area", animals.area);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Update(Animals animals,int idAnimal)
    {
        using (SqlConnection connection = new SqlConnection(conString))
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText =
                "UPDATE ANIMAL SET Name=@name, Description=@description,Category=@category,Area=@area Where idAnimal="+idAnimal;
            command.Parameters.AddWithValue("@name", animals.name);
            command.Parameters.AddWithValue("@description", animals.description);
            command.Parameters.AddWithValue("@category", animals.category);
            command.Parameters.AddWithValue("@area", animals.area);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static void Delete(int idAnimal)
    {
        using (SqlConnection connection = new SqlConnection(conString))
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Delete from Animal where IdAnimal=" + idAnimal;
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
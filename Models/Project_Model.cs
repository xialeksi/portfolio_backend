using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace portfolio_backend
{
    public class Project
    {
        public int idproject { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        internal Database? Db { get; set; }

        public Project () {}
        public Project (int id, string n, string d) {
            idproject = id;
            name = n;
            description = d;
        }
        internal Project (Database db)
        {
            Db = db;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM project ;";
            var result=await ReturnAllAsync(await cmd.ExecuteReaderAsync());
           // Console.WriteLine(result);
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }
        //select one
        //create new
        //edit existing
        //delete one
        private async Task<List<Project>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Project>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Project(Db)
                    {
                        idproject = reader.GetInt32(0),
                        name = null,
                        description = null
                    };
                    if (!reader.IsDBNull(1))
                        post.name = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        post.description = reader.GetString(2);

                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
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
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }
        //select one
        public async Task<Project> FindOneAsync(int idproject)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM project WHERE idproject = @idproject";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idproject",
                DbType = DbType.Int32,
                Value = idproject,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            if(result.Count > 0){
                return result[0];
            }
            else {
                return null;
            }
            //return result.Count > 0 ? result[0] : null;
        }

        //create new
        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText=@"insert into project(idproject,name,description) 
            values(@idproject,@name,@description);";
            BindParams(cmd);
            BindId(cmd);
            try
            {
                int affectedRows=await cmd.ExecuteNonQueryAsync();
                return affectedRows;
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }
        //edit existing
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE project SET idproject = @idproject, name = @name, description = @description WHERE idproject = @idproject;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }
        //delete one
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM project WHERE idproject = @idproject;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

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
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idproject",
                DbType = DbType.String,
                Value = idproject,
            });
        }
        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.DateTime,
                Value = name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@description",
                DbType = DbType.DateTime,
                Value = description,
            });
        }
    }
}
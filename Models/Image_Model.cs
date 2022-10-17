using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace portfolio_backend
{
    public class Image
    {
        public int idimage { get; set; }
        public int idproject { get; set; }
        public string filename { get; set; }
        public string description { get; set; }
        internal Database? Db { get; set; }

        public Image () {}
        public Image (int idi, int idp, string n, string d) {
            idimage = idi;
            idproject = idp;
            filename = n;
            description = d;
        }
        internal Image (Database db)
        {
            Db = db;
        }

        public async Task<List<Image>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM image ;";
            var result=await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }
        //select one
        public async Task<Image> FindOneAsync(int idimage)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM image WHERE idimage = @idimage";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idimage",
                DbType = DbType.Int32,
                Value = idimage,
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
            cmd.CommandText=@"insert into image(idimage,idproject,filename,description) 
            values(@idimage,@idproject,@filename,@description);";
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
            cmd.CommandText = @"UPDATE image SET idproject = @idproject, filename = @filename, description = @description WHERE idimage = @idimage;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        //delete one
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM image WHERE idimage = @idimage;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }





        private async Task<List<Image>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Image>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Image(Db)
                    {
                        idimage = reader.GetInt32(0),
                        filename = null,
                        description = null
                    };
                    if (!reader.IsDBNull(1))
                        post.idproject = reader.GetInt32(1);

                    if (!reader.IsDBNull(2))
                        post.filename = reader.GetString(2);

                    if (!reader.IsDBNull(3))
                        post.description = reader.GetString(3);

                    posts.Add(post);
                }
            }
            return posts;
        }
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idimage",
                DbType = DbType.Int32,
                Value = idimage,
            });
        }
        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idproject",
                DbType = DbType.String,
                Value = idproject,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@filename",
                DbType = DbType.String,
                Value = filename,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@description",
                DbType = DbType.String,
                Value = description,
            });
        }
    }
}
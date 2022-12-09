using System.IO;
namespace fut5.Data
{
    public class dbPathService
    {
        public string databasePath;
        public dbPathService()
        {
            var db_file = Directory.GetFiles(".", "fut5.db", SearchOption.AllDirectories);
            if (db_file.GetLength(0) > 0)
            {
                this.databasePath = db_file[0];
            }
        }
    }
}
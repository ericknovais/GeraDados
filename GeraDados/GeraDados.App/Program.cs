using Geradados.DataAccess.DB;
using Microsoft.EntityFrameworkCore;

namespace GeraDados.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var op = new DbContextOptionsBuilder<ContextoDataBase>().UseSqlServer("Server=(LocalDB)\\mssqllocaldb;database=GeraDadosDB").Options;
            //using var db = new ContextoDataBase(op);
            //db.Database.EnsureCreated();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace CrudOperations.DAL
{
    public class DALBASE : IDisposable
    {

        public Database db = null;
        public DbCommand command = null;
        public DALBASE()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            db = factory.Create("ConStr");
        
        }


        public void errorLog(string MethodName, string Message)
        {

            using (command = db.GetStoredProcCommand("sp_tblErrorLog"))
            {
                db.AddInParameter(command, "@MethodName", DbType.String, MethodName);
                db.AddInParameter(command, "@ErrorMsg", DbType.String, Message);

                try
                {

                    IDataReader reader = db.ExecuteReader(command);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        private bool disposed = false;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.

                //Release Database object
                if (db != null)
                    db = null;
                disposed = true;
            }
        }



        // Use C# destructor syntax for finalization code.
        ~DALBASE()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }
}

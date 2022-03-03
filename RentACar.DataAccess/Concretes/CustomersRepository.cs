using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using RentACar.Commons.Abstractions.Concretes.Data;
using RentACar.Commons.Abstractions.Concretes.Helpers;
using RentACar.Commons.Abstractions.Concretes.Logger;
using RentACar.DataAccess.Abstractions;
using RentACar.Model.Concretes;
namespace RentACar.DataAccess.Concretes
{
    public class CustomersRepository : IRepository<Customers>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public CustomersRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }
        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("WHERE ");
                query.Append("[CustomerID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_Customer] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public bool Insert(Customers entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Customers] ");
                query.Append("( [CustomerName], [CustomerSurname], [CustomerPasskey], [IDNo], [DriverIDNo], [Cellphpne], [CustomerAddress] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CustomerName, @CustomerSurname, @CustomerPasskey, @IDNo, @DriverIDNo, @Cellphpne, @CustomerAddress ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@CustomerName", CsType.String, ParameterDirection.Input, entity.CustomerName);
                        DBHelper.AddParameter(dbCommand, "@CustomerSurname", CsType.String, ParameterDirection.Input, entity.CustomerSurname);
                        DBHelper.AddParameter(dbCommand, "@CustomerPasskey", CsType.String, ParameterDirection.Input, entity.CustomerPasskey);
                        DBHelper.AddParameter(dbCommand, "@IDNo", CsType.Int, ParameterDirection.Input, entity.IDNo);
                        DBHelper.AddParameter(dbCommand, "@DriverIDNo", CsType.Int, ParameterDirection.Input, entity.DriverIDNo);
                        DBHelper.AddParameter(dbCommand, "@Cellphpne", CsType.Int, ParameterDirection.Input, entity.Cellphpne);
                        DBHelper.AddParameter(dbCommand, "@CustomerAddress", CsType.String, ParameterDirection.Input, entity.CustomerAddress);
                        
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Customers] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Customers> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Customers> customers = new List<Customers>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CustomerID], [CustomerName], [CustomerSurname], [CustomerPasskey], [IDNo], [DriverIDNo], [Cellphpne], [CustomerAddress] ");
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Customers();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.CustomerName = reader.GetString(1);
                                    entity.CustomerSurname = reader.GetString(2);
                                    entity.CustomerPasskey = reader.GetString(3);
                                    entity.IDNo = reader.GetInt32(4);
                                    entity.DriverIDNo = reader.GetInt32(5);
                                    entity.Cellphpne = reader.GetInt32(6);
                                    entity.CustomerAddress = reader.GetString(7);
                                    customers.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting All Error for entity [tbl_Customers] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Customers SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Customers customer = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CustomerID], [CustomerName], [CustomerSurname], [CustomerPasskey], [IDNo], [DriverIDNo], [Cellphpne], [CustomerAddress] ");
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("WHERE ");
                query.Append("[CustomerID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Customers();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.CustomerName = reader.GetString(1);
                                    entity.CustomerSurname = reader.GetString(2);
                                    entity.CustomerPasskey = reader.GetString(3);
                                    entity.IDNo = reader.GetInt32(4);
                                    entity.DriverIDNo = reader.GetInt32(5);
                                    entity.Cellphpne = reader.GetInt32(6);
                                    entity.CustomerAddress = reader.GetString(7);
                                    customer = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting Error for entity [tbl_Customers] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Customers entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Customers] ");
                query.Append(" SET [CustomerName] = @CustomerName, [CustomerSurname] = @CustomerSurname, [CustomerPasskey] =  @CustomerPasskey, [IDNo] = @IDNo, [DriverIDNo] = @DriverIDNo, [Cellphpne] = @Cellphpne, [CustomerAddress] = @CustomerAddress ");
                query.Append(" WHERE ");
                query.Append(" [CustomerID] = @CustomerID ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@CustomerName", CsType.String, ParameterDirection.Input, entity.CustomerName);
                        DBHelper.AddParameter(dbCommand, "@CustomerSurname", CsType.String, ParameterDirection.Input, entity.CustomerSurname);
                        DBHelper.AddParameter(dbCommand, "@CustomerPasskey", CsType.String, ParameterDirection.Input, entity.CustomerPasskey);
                        DBHelper.AddParameter(dbCommand, "@IDNo", CsType.Int, ParameterDirection.Input, entity.IDNo);
                        DBHelper.AddParameter(dbCommand, "@DriverIDNo", CsType.Int, ParameterDirection.Input, entity.DriverIDNo);
                        DBHelper.AddParameter(dbCommand, "@Cellphpne", CsType.Int, ParameterDirection.Input, entity.Cellphpne);
                        DBHelper.AddParameter(dbCommand, "@CustomerAddress", CsType.String, ParameterDirection.Input, entity.CustomerAddress);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Customers] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Update:Error occured.", ex);
            }
        }
    }
}

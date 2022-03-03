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
    public class CarRentingRepository : IRepository<CarRenting>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;
        private bool disposedValue;

        public CarRentingRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }
        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_CarRent] ");
                query.Append("WHERE ");
                query.Append("[CarRentingID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_CarRent] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_CarRent] reported the Database ErrorCode: " +
                                _errorCode);
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

        

        public bool Insert(CarRenting entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_CarRent] ");
                query.Append("( [CarId], [CustomerID], [CarKMGiving], [CarKMGettinBack], [CarRenttingTotalPrice] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CarId, @CustomerID, @CarKMGiving, @CarKMGettinBack, @CarRenttingTotalPrice) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_CarRent] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CarId", CsType.String, ParameterDirection.Input, entity.CarID);
                        DBHelper.AddParameter(dbCommand, "@CustomerID", CsType.String, ParameterDirection.Input, entity.CustomerID);
                        DBHelper.AddParameter(dbCommand, "@CarKMGiving", CsType.String, ParameterDirection.Input, entity.CarKMGiving);
                        DBHelper.AddParameter(dbCommand, "@CarKMGettinBack", CsType.Decimal, ParameterDirection.Input, entity.CarKMGettinBack);
                        DBHelper.AddParameter(dbCommand, "@CarRenttingTotalPrice", CsType.Byte, ParameterDirection.Input, entity.CarRenttingTotalPrice);
                        
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_CarRent] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CarRentingRepository::Insert:Error occured.", ex);
            }
        }

        public IList<CarRenting> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<CarRenting> carrenting = new List<CarRenting>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarId], [CustomerID], [CarKMGiving], [CarKMGettinBack], [CarRenttingTotalPrice] ");
                query.Append("FROM [dbo].[tbl_CarRent] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_CarRent] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new CarRenting();

                                    entity.CarRentingID = reader.GetInt32(0);
                                    entity.CustomerID = reader.GetInt32(1);
                                    entity.CarID = reader.GetInt32(2);
                                    entity.CustomerID = reader.GetInt32(3);
                                    entity.CarKMGiving = reader.GetInt32(4);
                                    entity.CarKMGettinBack = reader.GetInt32(5);
                                    entity.CarRenttingTotalPrice = reader.GetByte(6);
                                    carrenting.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_CarRent] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return carrenting;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public CarRenting SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            CarRenting carrent = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CArRentingID], [CarId], [CustomerID], [CarKMGiving], [CarKMGettinBack], [CarRenttingTotalPrice] ");
                query.Append("FROM [dbo].[tbl_CarRent] ");
                query.Append("WHERE ");
                query.Append("[CarRentingID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_CarRenting] can't be null. ");

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
                                    var entity = new CarRenting();
                                    entity.CarRentingID = reader.GetInt32(0);
                                    entity.CustomerID = reader.GetInt32(1);
                                    entity.CarID = reader.GetInt32(2);
                                    entity.CustomerID = reader.GetInt32(3);
                                    entity.CarKMGiving = reader.GetInt32(4);
                                    entity.CarKMGettinBack = reader.GetInt32(5);
                                    entity.CarRenttingTotalPrice = reader.GetByte(6);
                                    carrent = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_CarRent] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                
                return carrent;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CarRentingRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(CarRenting entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_CarRenting] ");
                query.Append(" SET [CarId] = @CarID, [CustomerID] = @CCustomerID, [CarKMGiving] =  @CarKMGiving, [CarKMGettinBack] = @CarKMGettinBack, [CarRenttingTotalPrice] = @CarRenttingTotalPrice ");
                query.Append(" WHERE ");
                query.Append(" [CarRentingID] = @CarRentingID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_CarRent] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@CarRentingID", CsType.Int, ParameterDirection.Input, entity.CustomerID);
                        DBHelper.AddParameter(dbCommand, "@CarId", CsType.Int, ParameterDirection.Input, entity.CarID);
                        DBHelper.AddParameter(dbCommand, "@CustomerID", CsType.Int, ParameterDirection.Input, entity.CustomerID);
                        DBHelper.AddParameter(dbCommand, "@CarKMGiving", CsType.Int, ParameterDirection.Input, entity.CarKMGiving);
                        DBHelper.AddParameter(dbCommand, "@CarKMGettinBack", CsType.Int, ParameterDirection.Input, entity.CarKMGettinBack);
                        DBHelper.AddParameter(dbCommand, "@CarRenttingTotalPrice", CsType.Int, ParameterDirection.Input, entity.CarRenttingTotalPrice);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_CarRent] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CarRentingRepository::Update:Error occured.", ex);
            }
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (!disposedValue)
            {
                if (bDisposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) atın
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve sonlandırıcıyı geçersiz kılın
                // TODO: büyük alanları null olarak ayarlayın
                disposedValue = true;
            }
        }

        // // TODO: sonlandırıcıyı yalnızca 'Dispose(bool disposing)' içinde yönetilmeyen kaynakları serbest bırakacak kod varsa geçersiz kılın
        // ~CarRentingRepository()
        // {
        //     // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
            Dispose(bDisposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

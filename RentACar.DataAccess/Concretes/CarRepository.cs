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
    public class CarRepository : IRepository<Car>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public CarRepository()
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
                query.Append("FROM [dbo].[tbl_Car] ");
                query.Append("WHERE ");
                query.Append("[CarID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Car] can't be null. ");

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
                                "Deleting Error for entity [tbl_Car] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CarRepository::Insert:Error occured.", ex);
            }
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
        public bool Insert(Car entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Car] ");
                query.Append("( [CarModel], [CarAirbag], [CarDriverLicenseType], [CarLuggage], [CarMaxKmDaily], [CarKm], [CarPrice], [CarSeats], [CarAddress], [TaxID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CarModel, @CarAirbag, @CarDriverLicenseType, @CarLuggage, @CarMaxKmDaily, @CarKm, @CarPrice, @CarSeats, @CarAddress, @TaxID ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Car] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.String, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@CarAirbag", CsType.String, ParameterDirection.Input, entity.CarAirbag);
                        DBHelper.AddParameter(dbCommand, "@CarDriverLicenseType", CsType.String, ParameterDirection.Input, entity.CarDriverLicenseType);
                        DBHelper.AddParameter(dbCommand, "@CarLuggage", CsType.Int, ParameterDirection.Input, entity.CarLuggage);
                        DBHelper.AddParameter(dbCommand, "@CarMaxKmDaily", CsType.Int, ParameterDirection.Input, entity.CarMaxKmDaily);
                        DBHelper.AddParameter(dbCommand, "@CarKm", CsType.Int, ParameterDirection.Input, entity.CarKm);
                        DBHelper.AddParameter(dbCommand, "@CarPrice", CsType.Int, ParameterDirection.Input, entity.CarPrice);
                        DBHelper.AddParameter(dbCommand, "@CarSeats", CsType.Int, ParameterDirection.Input, entity.CarSeats);
                        DBHelper.AddParameter(dbCommand, "@CarAddress", CsType.String, ParameterDirection.Input, entity.CarAddress);
                        DBHelper.AddParameter(dbCommand, "@TaxID", CsType.Int, ParameterDirection.Input, entity.TaxID);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Car] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CarRepository::Insert:Error occured.", ex);
            }        
        }

        public IList<Car> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Car> cars = new List<Car>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarID], [CarModel], [CarAirbag], [CarDriverLicenseType], [CarLuggage], [CarMaxKmDaily], [CarKm], [CarPrice], [CarSeats], [CarAddress], [TaxID] ");
                query.Append("FROM [dbo].[tbl_Car] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Car] can't be null. ");

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
                                    var entity = new Car();
                                    entity.CarId = reader.GetInt32(0);
                                    entity.CarModel = reader.GetString(1);
                                    entity.CarAirbag = reader.GetString(2);
                                    entity.CarDriverLicenseType = reader.GetString(3);
                                    entity.CarLuggage = reader.GetInt32(4);
                                    entity.CarMaxKmDaily = reader.GetInt32(5);
                                    entity.CarKm = reader.GetInt32(6);
                                    entity.CarPrice = reader.GetInt32(7);
                                    entity.CarSeats = reader.GetInt32(8);
                                    entity.CarAddress = reader.GetString(9);
                                    entity.TaxID = reader.GetInt32(10);
                                    cars.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Car] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return cars;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Car SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Car car = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarID], [CarModel], [CarAirbag], [CarDriverLicenseType], [CarLuggage], [CarMaxKmDaily], [CarKm], [CarPrice], [CarSeats], [CarAddress], [TaxID] ");
                query.Append("FROM [dbo].[tbl_Car] ");
                query.Append("WHERE ");
                query.Append("[CarID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Car] can't be null. ");

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
                                    var entity = new Car();
                                    entity.CarId = reader.GetInt32(0);
                                    entity.CarModel = reader.GetString(1);
                                    entity.CarAirbag = reader.GetString(2);
                                    entity.CarDriverLicenseType = reader.GetString(3);
                                    entity.CarLuggage = reader.GetInt32(4);
                                    entity.CarMaxKmDaily = reader.GetInt32(5);
                                    entity.CarKm = reader.GetInt32(6);
                                    entity.CarPrice = reader.GetInt32(7);
                                    entity.CarSeats = reader.GetInt32(8);
                                    entity.CarAddress = reader.GetString(9);
                                    entity.TaxID = reader.GetInt32(10);
                                    car = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Car] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                
                return car;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Car entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Car] ");
                query.Append(" SET [CarModel] = @CarModel, [CarAirbag] = @CarAirbag, [CarDriverLicenseType] =  @CarDriverLicenseType, [CarLuggage] = @CarLuggage, [CarMaxKmDaily] = @CarMaxKmDaily, [CarKm] = @CarKm, [CarPrice] = @CarPrice, [CarSeats] = @CarSeats, [CarAddress] = @CarAddress, [TaxID] = @TaxID ");
                query.Append(" WHERE ");
                query.Append(" [CarID] = @CarID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Car] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.String, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@CarAirbag", CsType.String, ParameterDirection.Input, entity.CarAirbag);
                        DBHelper.AddParameter(dbCommand, "@CarDriverLicenseType", CsType.String, ParameterDirection.Input, entity.CarDriverLicenseType);
                        DBHelper.AddParameter(dbCommand, "@CarLuggage", CsType.Int, ParameterDirection.Input, entity.CarLuggage);
                        DBHelper.AddParameter(dbCommand, "@CarMaxKmDaily", CsType.Int, ParameterDirection.Input, entity.CarMaxKmDaily);
                        DBHelper.AddParameter(dbCommand, "@CarKm", CsType.Int, ParameterDirection.Input, entity.CarKm);
                        DBHelper.AddParameter(dbCommand, "@CarPrice", CsType.Int, ParameterDirection.Input, entity.CarPrice);
                        DBHelper.AddParameter(dbCommand, "@CarSeats", CsType.Int, ParameterDirection.Input, entity.CarSeats);
                        DBHelper.AddParameter(dbCommand, "@CarAddress", CsType.String, ParameterDirection.Input, entity.CarAddress);
                        DBHelper.AddParameter(dbCommand, "@TaxID", CsType.Int, ParameterDirection.Input, entity.TaxID);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Car] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
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

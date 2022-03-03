using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Commons.Abstractions.Concretes.Helpers;
using RentACar.Commons.Abstractions.Concretes.Logger;
using RentACar.Model.Concretes;
using RentACar.DataAccess.Concretes;

namespace RentACar.BusinessLogic.Concretes
{
    public class CarBusiness:IDisposable
    {
        public bool InsertCar(Car entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarBusiness::InsertCar::Error occured.", ex);
            }
        }

        public bool UpdateCar(Car entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarBusiness::UpdateCar::Error occured.", ex);
            }
        }

        public bool DeleteCarById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarBusiness::DeleteCar::Error occured.", ex);
            }
        }

        public Car SelectCarById(int carId)
        {
            try
            {
                Car responseEntitiy;
                using (var repo = new CarRepository())
                {
                    responseEntitiy = repo.SelectedById(carId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Car doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:carBusiness::SelectcarById::Error occured.", ex);
            }
        }

        public List<Car> SelectAllCar()
        {
            var responseEntities = new List<Car>();

            try
            {
                using (var repo = new CarRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarBusiness::SelectAllCar::Error occured.", ex);
            }
        }

        public CarBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}

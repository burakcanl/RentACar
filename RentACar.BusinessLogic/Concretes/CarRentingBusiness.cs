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
    public class CarRentingBusiness:IDisposable
    {
        public bool InsertCarRenting(CarRenting entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRentingRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarRentingBusiness::InsertCarRenting::Error occured.", ex);
            }
        }

        public bool UpdateCarRenting(CarRenting entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRentingRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarRentingBusiness::UpdateCarRenting::Error occured.", ex);
            }
        }

        public bool DeleteCarById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRentingRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarRentingBusiness::DeleteCarRenting::Error occured.", ex);
            }
        }

        public CarRenting SelectCarById(int carrentingId)
        {
            try
            {
                CarRenting responseEntitiy;
                using (var repo = new CarRentingRepository())
                {
                    responseEntitiy = repo.SelectedById(carrentingId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("CarRenting doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CarRentingBusiness::SelectCarRentingId::Error occured.", ex);
            }
        }

        public List<CarRenting> SelectAllCar()
        {
            var responseEntities = new List<CarRenting>();

            try
            {
                using (var repo = new CarRentingRepository())
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
                throw new Exception("BusinessLogic:CarRentingBusiness::SelectAllCarRenting::Error occured.", ex);
            }
        }

        public CarRentingBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}

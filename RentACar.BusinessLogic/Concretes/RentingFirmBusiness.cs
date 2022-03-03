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
    public class RentingFirmBusiness:IDisposable
    {
        public bool InsertRentingFirm(RentingFirm entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentingFirmRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:RentingFirmBusiness::InsertRentingFirm::Error occured.", ex);
            }
        }

        public bool UpdateRentingFirm(RentingFirm entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentingFirmRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:RentingFirmBusiness::UpdateRentingFirm::Error occured.", ex);
            }
        }

        public bool DeleteRentingFirmById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentingFirmRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:RentingFirmBusiness::DeleteRentingFirm::Error occured.", ex);
            }
        }

        public RentingFirm SelectRentingFirmById(int taxId)
        {
            try
            {
                RentingFirm responseEntitiy;
                using (var repo = new RentingFirmRepository())
                {
                    responseEntitiy = repo.SelectedById(taxId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Firm doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:RentingFirmBusiness::SelectRentingFirmById::Error occured.", ex);
            }
        }

        public List<RentingFirm> SelectAllRentingFirm()
        {
            var responseEntities = new List<RentingFirm>();

            try
            {
                using (var repo = new RentingFirmRepository())
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
                throw new Exception("BusinessLogic:RentingFirmBusiness::SelectAllRentingFirm::Error occured.", ex);
            }
        }

        public RentingFirmBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}

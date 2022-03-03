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
    public class UserBusiness:IDisposable
    {
        public bool InsertUser(User entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new UserRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:UserBusiness::InsertUser::Error occured.", ex);
            }
        }

        public bool UpdateUser(User entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new UserRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:UserBusiness::UpdateUser::Error occured.", ex);
            }
        }

        public bool DeleteUserById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new UserRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:UserBusiness::DeleteUser::Error occured.", ex);
            }
        }

        public User SelectUserById(int userId)
        {
            try
            {
                User responseEntitiy;
                using (var repo = new UserRepository())
                {
                    responseEntitiy = repo.SelectedById(userId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("User doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:UserBusiness::SelectUserId::Error occured.", ex);
            }
        }

        public List<User> SelectAllCar()
        {
            var responseEntities = new List<User>();

            try
            {
                using (var repo = new UserRepository())
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
                throw new Exception("BusinessLogic:UserBusiness::SelectAllUser::Error occured.", ex);
            }
        }

        public UserBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}

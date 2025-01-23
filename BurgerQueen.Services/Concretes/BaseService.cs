using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _baseRepository = _unitOfWork.Repository<T>();
        }

        public virtual async Task AddDTOAsync(T entityDTO)
        {
            try
            {
                var entity = (T)Activator.CreateInstance(typeof(T));
                // Manually map properties from DTO to Entity
                foreach (var property in typeof(T).GetProperties())
                {
                    if (property.CanWrite && property.Name != "Id" && typeof(T).GetProperty(property.Name) != null && entityDTO.GetType().GetProperty(property.Name) != null)
                    {
                        property.SetValue(entity, entityDTO.GetType().GetProperty(property.Name).GetValue(entityDTO));
                    }
                }
                await _baseRepository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task UpdateDTOAsync(T entityDTO)
        {
            try
            {
                var entity = await _baseRepository.GetById(e => e.Id == entityDTO.Id);
                if (entity != null)
                {
                    // Manually map properties from DTO to Entity
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (property.CanWrite && property.Name != "Id" && typeof(T).GetProperty(property.Name) != null && entityDTO.GetType().GetProperty(property.Name) != null)
                        {
                            property.SetValue(entity, entityDTO.GetType().GetProperty(property.Name).GetValue(entityDTO));
                        }
                    }
                    await _baseRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Entity with id {entityDTO.Id} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task DeleteDTOAsync(int id)
        {
            try
            {
                var entity = await _baseRepository.GetById(e => e.Id == id);
                if (entity != null)
                {
                    await _baseRepository.DeleteAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Entity with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<List<T>> GetAllDTO()
        {
            return await _baseRepository.GetAllActive();
        }

        public virtual async Task<T> GetByIdDTO(Expression<Func<T, bool>> exp)
        {
            return await _baseRepository.GetById(exp);
        }

        public virtual async Task<List<T>> GetByDTO(Expression<Func<T, bool>> exp)
        {
            return await _baseRepository.GetBy(exp);
        }
    }
}

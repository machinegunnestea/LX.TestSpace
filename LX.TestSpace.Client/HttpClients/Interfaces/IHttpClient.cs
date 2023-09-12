using LX.TestSpace.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LX.TestSpace.Client.HttpRepository.Interfaces
{
    public interface IHttpClient<T> where T : class
    {
        Task<T> Create(T item);
        Task<bool> Delete(int id);
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<bool> Update(T item);
    }
}

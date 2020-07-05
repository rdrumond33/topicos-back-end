using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projeto.DAO
{
    public class IncludeAttribute : Attribute
    {}
    public static class BaseIncludeDAO
    {

        public static IQueryable<T> LoadRelated<T>(this IQueryable<T> originalQuery) where T : BaseEntity, new()
        {
            Func<IQueryable<T>, IQueryable<T>> includeFunc = f => f;
            foreach (var prop in typeof(T).GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(IncludeAttribute))))
            {
                Func<IQueryable<T>, IQueryable<T>> chainedIncludeFunc = f => f.Include(prop.Name);
                includeFunc = Compose(includeFunc, chainedIncludeFunc);
            }

            return includeFunc(originalQuery);
        }
        private static Func<T, T> Compose<T>(Func<T, T> innerFunc, Func<T, T> outerFunc)
        {
            return arg => outerFunc(innerFunc(arg));
        }

    }
}
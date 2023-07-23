using System.Collections.Generic;
using Students.Common.Models;

namespace Students.Common.Contracts
{
    public interface IJsonManager<T> where T : Entity
    {
        IList<T> GetContent(string filePath);
    }
}
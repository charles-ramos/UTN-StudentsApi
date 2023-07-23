using Newtonsoft.Json;
using System.Collections.Generic;
using Students.Common.Contracts;
using Students.Common.Models;


namespace Students.Common.Implementations
{
    public class JsonManager<T> : IJsonManager<T> where T : Entity
    {
        public IList<T> GetContent(string filePath)
        {
            var jsonContent = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<IList<T>>(jsonContent)!;
        }
    }
}
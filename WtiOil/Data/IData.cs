using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет коллекцию данных для хранения данных.  
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// Полный возможный набо данных для данного экземпляра. 
        /// </summary>
        List<ItemWTI> FullData { get; }

        /// <summary>
        /// Набор данных  с которыми производятся операции.
        /// </summary>
        List<ItemWTI> Data { get; set; }
    }
}

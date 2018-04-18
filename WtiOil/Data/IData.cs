using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    public interface IData
    {
        List<ItemWTI> FullData { get; }
        List<ItemWTI> Data { get; set; }
    }
}

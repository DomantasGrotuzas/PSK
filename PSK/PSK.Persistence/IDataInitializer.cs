using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSK.Persistence
{
    public interface IDataInitializer
    {
        void InitializeDatabase();
    }
}

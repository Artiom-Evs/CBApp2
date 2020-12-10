using System;
using System.Collections.Generic;
using System.Text;

namespace CBApp2.Services
{
    public interface IConnectionInfo
    {
        bool IsConnected { get; }
    }
}

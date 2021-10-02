using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IFixable
{
    void StartFix();

    bool IsFixed {
        get;
        set;
    }
    bool IsFixing {
        get;
        set;
    }
    bool IsBugged {
        get;
        set;
    }

    float ScanTime {
        get;
    }
}
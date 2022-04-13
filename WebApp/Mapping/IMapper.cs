using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapping
{
    public interface IMapper<TLeftObject, TRightObject>
    {
        TLeftObject Map(TRightObject inObject);
        TRightObject Map(TLeftObject inObject);
    }
}

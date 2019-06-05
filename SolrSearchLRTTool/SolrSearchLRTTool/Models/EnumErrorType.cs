using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool.Models
{
    public enum EnumErrorType
    {
        querySuccess,

        queryError,

        SearchError,

        KeyListSearchError,

        CreateFileSuccess,

        CreateFileError,

        SearchNoData

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.WebServices
{
    public class Constant
    {
        /// <summary>
        /// WebAPI認証用：項目名
        /// </summary>
        public const string HTTP_ENDPOINT = "ApiEndpoint";

        /// <summary>
        /// WebAPI認証用：項目名
        /// </summary>
        public const string HTTP_HEADER_APITOKEN = "APIToken";

        /// <summary>
        /// WebAPI認証用：項目名
        /// </summary>
        public const string LOG_MAXDAYCOUNT = "MaxArchiveDayCount";

        /// <summary>
        /// 成功
        /// </summary>
        public const int APIRESULT_SUCCESS = 1;

        /// <summary>
        /// データ無し
        /// </summary>
        public const int APIRESULT_NONEDATA = 0;

        /// <summary>
        /// エラー
        /// </summary>
        public const int APIRESULT_ERROR = -1;


        /// <summary>データステータス：初期値</summary>
        public const int DATASTATUS_NONE = 0;

        /// <summary>データステータス：追加</summary>
        public const int DATASTATUS_ADD = 1;

        /// <summary>データステータス：変更</summary>
        public const int DATASTATUS_UPDATE = 2;

        /// <summary>データステータス：削除</summary>
        public const int DATASTATUS_DELETE = 3;
    }
}

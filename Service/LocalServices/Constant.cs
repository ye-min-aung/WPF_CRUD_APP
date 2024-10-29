using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LocalServices
{
    public class Constant
    {
       
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

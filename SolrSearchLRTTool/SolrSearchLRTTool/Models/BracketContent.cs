namespace SolrSearchLRTTool
{
    /// <summary>
    /// 括号
    /// </summary>
    public class Brackets
    {
        /// <summary>
        /// 是否是左括号
        /// </summary>
        public bool bIsLeft;
        /// <summary>
        /// 在字符串中的位置
        /// </summary>
        public int iIndexOfStr;

        public int IindexBracketNum;

        public Brackets(bool bIsLeft, int iIndexOfStr, int IindexBracketNum)
        {

            this.bIsLeft = bIsLeft;
            this.iIndexOfStr = iIndexOfStr;
            this.IindexBracketNum = IindexBracketNum;
        }

        public Brackets(bool bIsLeft, int iIndexOfStr)
        {

            this.bIsLeft = bIsLeft;
            this.iIndexOfStr = iIndexOfStr;
        }
    }
     
    /// <summary>
    /// 括号中内容
    /// </summary>
    public class BracketContent
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int BCID;
        /// <summary>
        /// 关键字
        /// </summary>
        public string BCKey;
        /// <summary>
        /// 括号内容
        /// </summary>
        public string BCContents;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErreStr;

        public BracketContent(int BCID, string BCKey, string BCContents)
        {
            this.BCID = BCID;
            this.BCKey = BCKey;
            this.BCContents = BCContents;
        }

    }
}

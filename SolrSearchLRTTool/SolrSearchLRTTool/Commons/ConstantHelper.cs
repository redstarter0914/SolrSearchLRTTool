namespace SolrSearchLRTTool
{
    /// <summary>
    /// 常量帮助类
    /// </summary>
    public class ConstantHelper
    {  
        /// <summary>
        /// 替换日期格式
        /// </summary>
        public const string DateStrFormat = "@datetime@";
        /// <summary>
        /// 日期格式
        /// </summary>
        public const string DateFormatStr = "yyyy-MM-dd'T'HH:mm:ss.sssZ";
        /// <summary>
        /// 日期正则表达式
        /// </summary>
        public const string DateRegexStr = @"((?<!\d)((\d{2,4}(\.|年|\/|\-))((((0?[13578]|1[02])(\.|月|\/|\-))((3[01])|([12][0-9])|(0?[1-9])))|(0?2(\.|月|\/|\-)((2[0-8])|(1[0-9])|(0?[1-9])))|(((0?[469]|11)(\.|月|\/|\-))((30)|([12][0-9])|(0?[1-9]))))|((([0-9]{2})((0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))(\.|年|\/|\-))0?2(\.|月|\/|\-)29))日?(?!\d))";

        /// <summary>
        /// 替换Range格式   range(@Range@1,@Range@2,to='',from='')
        /// </summary>
        public const string RangeFormat = "@Range@";
        /// <summary>
        /// Range正则表达式
        /// </summary>
        public const string RangeRegex = @"range\(([^()]|\(([^()])*\))*\)";
        /// <summary>
        /// Range字符串 range
        /// </summary>
        public const string RangeStr = "range";
        /// <summary>
        /// Range括号  range(
        /// </summary>
        public const string RangeBrackets = "range(";

        /// <summary>
        /// and字符串  and
        /// </summary>
        public const string AndStr = "and";
        /// <summary>
        /// and括号   and(
        /// </summary>
        public const string AndBrackets = "and(";
        /// <summary>
        /// and方法替换值   and(@andfuns@1,@andfuns@2)
        /// </summary>
        public const string AndFunEx = "@andfuns@";
        /// <summary>
        /// andand方法替换值    and(and(@andandfuns@1，@andandfuns@2),....)
        /// </summary>
        public const string AndAndFunEx = "@andandfuns@";
        /// <summary>
        ///  andor方法替换值    and(or(@andorfuns@1，@andorfuns@2),....)
        /// </summary>
        public const string AndOrFunEx = "@andorfuns@";
        /// <summary>
        /// or 字符串  or
        /// </summary>
        public const string OrStr = "or";
        /// <summary>
        /// or括号    or(
        /// </summary>
        public const string OrBrackets = "or(";
        /// <summary>
        /// or方法替换    or(@orfuns@1,@orfuns@2)
        /// </summary>
        public const string OrFunEx = "@orfuns@";
        /// <summary>
        /// orand方法替换  or(and(@orandfuns@1,@orandfuns@2),...)
        /// </summary>
        public const string OrAndFunEx = "@orandfuns@";
        /// <summary>
        /// oror方法替换   or(or(@ororfuns@1,@ororfuns@2),....)
        /// </summary>
        public const string OrOrFunEx = "@ororfuns@";
        /// <summary>
        /// AndNot字符串
        /// </summary>
        public const string AndNotStr = "andnot";
        /// <summary>
        /// equals字符串
        /// </summary>
        public const string EqualsStr = "equals";
        /// <summary>
        /// not字符串
        /// </summary>
        public const string NotStr = "not";
        /// <summary>
        /// near字符串
        /// </summary>
        public const string NearStr = "near";
        /// <summary>
        /// Brackets字符串  (
        /// </summary>
        public const string FBracketsStr = "(";
        /// <summary>
        /// Brackets字符串  )
        /// </summary>
        public const string EBracketsStr = "(";


    }
}
